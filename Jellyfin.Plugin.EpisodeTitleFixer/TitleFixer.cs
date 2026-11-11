using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Plugin.EpisodeTitleFixer.Configuration;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.EpisodeTitleFixer;

/// <summary>
/// Does the actual fix for one episode.
/// </summary>
public class TitleFixer
{
    private readonly ILibraryManager _libraryManager;
    private readonly ILogger<TitleFixer> _logger;
    private readonly object _regexLock = new();
    private string? _cachedPattern;
    private Regex? _cachedRegex;

    public TitleFixer(ILibraryManager libraryManager, ILogger<TitleFixer> logger)
    {
        _libraryManager = libraryManager;
        _logger = logger;
    }

    public static PluginConfiguration Config => Plugin.Instance?.Configuration ?? new PluginConfiguration();

    /// <summary>
    /// Returns true when the episode has a placeholder title and a real original title that can replace it.
    /// </summary>
    public bool NeedsFix(Episode episode)
    {
        var regex = GetRegex();
        if (regex is null || string.IsNullOrWhiteSpace(episode.Name) || !regex.IsMatch(episode.Name))
        {
            return false;
        }

        var original = episode.OriginalTitle;
        return !string.IsNullOrWhiteSpace(original)
            && !regex.IsMatch(original)
            && !string.Equals(original.Trim(), episode.Name.Trim(), StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the title to the original title and saves it (database, and NFO when NFO saving is enabled).
    /// </summary>
    public async Task<bool> FixAsync(Episode episode, CancellationToken cancellationToken)
    {
        if (!NeedsFix(episode))
        {
            return false;
        }

        var oldName = episode.Name;
        episode.Name = episode.OriginalTitle!.Trim();

        if (Config.LockTitle && Array.IndexOf(episode.LockedFields, MetadataField.Name) < 0)
        {
            episode.LockedFields = [.. episode.LockedFields, MetadataField.Name];
        }

        await _libraryManager.UpdateItemAsync(episode, episode.GetParent(), ItemUpdateType.MetadataEdit, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation(
            "Episode Title Fixer: {Series} S{Season}E{Episode} \"{Old}\" -> \"{New}\"",
            episode.SeriesName,
            episode.ParentIndexNumber ?? 0,
            episode.IndexNumber ?? 0,
            oldName,
            episode.Name);
        return true;
    }

    private Regex? GetRegex()
    {
        var pattern = string.IsNullOrWhiteSpace(Config.PlaceholderPattern) ? PluginConfiguration.DefaultPattern : Config.PlaceholderPattern;
        lock (_regexLock)
        {
            if (pattern == _cachedPattern)
            {
                return _cachedRegex;
            }

            try
            {
                _cachedRegex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, TimeSpan.FromSeconds(1));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Episode Title Fixer: invalid placeholder pattern {Pattern}", pattern);
                _cachedRegex = null;
            }

            _cachedPattern = pattern;
            return _cachedRegex;
        }
    }
}
