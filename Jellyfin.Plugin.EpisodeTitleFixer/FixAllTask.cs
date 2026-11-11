using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Data.Enums;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.EpisodeTitleFixer;

/// <summary>
/// Goes through every episode in the library and fixes placeholder titles.
/// </summary>
public class FixAllTask : IScheduledTask
{
    private readonly ILibraryManager _libraryManager;
    private readonly TitleFixer _fixer;
    private readonly ILogger<FixAllTask> _logger;

    public FixAllTask(ILibraryManager libraryManager, TitleFixer fixer, ILogger<FixAllTask> logger)
    {
        _libraryManager = libraryManager;
        _fixer = fixer;
        _logger = logger;
    }

    public string Name => "Fix placeholder episode titles";

    public string Key => "EpisodeTitleFixerFixAll";

    public string Description => "Replaces placeholder episode titles like \"Afsnit 11\" with the original title, for every episode in the library.";

    public string Category => "Episode Title Fixer";

    public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
    {
        return
        [
            new TaskTriggerInfo { Type = TaskTriggerInfoType.IntervalTrigger, IntervalTicks = TimeSpan.FromHours(24).Ticks }
        ];
    }

    public async Task ExecuteAsync(IProgress<double> progress, CancellationToken cancellationToken)
    {
        var episodes = _libraryManager.GetItemList(new InternalItemsQuery
        {
            IncludeItemTypes = [BaseItemKind.Episode],
            Recursive = true,
            IsVirtualItem = false
        }).OfType<Episode>().Where(_fixer.NeedsFix).ToList();

        _logger.LogInformation("Episode Title Fixer: {Count} episode(s) with a placeholder title", episodes.Count);

        var fixedCount = 0;
        for (var i = 0; i < episodes.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (await _fixer.FixAsync(episodes[i], cancellationToken).ConfigureAwait(false))
                {
                    fixedCount++;
                }
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Episode Title Fixer: failed to fix {Name} ({Id})", episodes[i].Name, episodes[i].Id);
            }

            progress.Report(100d * (i + 1) / episodes.Count);
        }

        _logger.LogInformation("Episode Title Fixer: fixed {Count} episode title(s)", fixedCount);
        progress.Report(100);
    }
}
