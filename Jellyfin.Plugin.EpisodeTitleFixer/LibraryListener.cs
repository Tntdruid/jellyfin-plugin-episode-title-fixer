using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.EpisodeTitleFixer;

/// <summary>
/// Fixes titles as soon as an episode is added or its metadata is refreshed.
/// </summary>
public sealed class LibraryListener : IHostedService
{
    private readonly ILibraryManager _libraryManager;
    private readonly TitleFixer _fixer;
    private readonly ILogger<LibraryListener> _logger;
    private readonly ConcurrentDictionary<Guid, byte> _inProgress = new();

    public LibraryListener(ILibraryManager libraryManager, TitleFixer fixer, ILogger<LibraryListener> logger)
    {
        _libraryManager = libraryManager;
        _fixer = fixer;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _libraryManager.ItemAdded += OnItemChanged;
        _libraryManager.ItemUpdated += OnItemChanged;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _libraryManager.ItemAdded -= OnItemChanged;
        _libraryManager.ItemUpdated -= OnItemChanged;
        return Task.CompletedTask;
    }

    private void OnItemChanged(object? sender, ItemChangeEventArgs e)
    {
        if (!TitleFixer.Config.FixAutomatically || e.Item is not Episode episode || !_fixer.NeedsFix(episode))
        {
            return;
        }

        // Our own save raises ItemUpdated again; skip while this episode is being fixed.
        if (!_inProgress.TryAdd(episode.Id, 0))
        {
            return;
        }

        _ = FixAsync(episode);
    }

    private async Task FixAsync(Episode episode)
    {
        try
        {
            await _fixer.FixAsync(episode, CancellationToken.None).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Episode Title Fixer: failed to fix {Name} ({Id})", episode.Name, episode.Id);
        }
        finally
        {
            _inProgress.TryRemove(episode.Id, out _);
        }
    }
}
