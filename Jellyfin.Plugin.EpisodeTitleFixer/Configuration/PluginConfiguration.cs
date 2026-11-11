using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.EpisodeTitleFixer.Configuration;

public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// The default pattern: a placeholder word in common languages followed by the episode number.
    /// </summary>
    public const string DefaultPattern = @"^\s*(Afsnit|Episode|Avsnitt|Episodi|Jakso|Þáttur|Folge|Aflevering|Épisode|Episodio|Odcinek|Epizoda|Epizód)\s*#?\d+\s*$";

    /// <summary>
    /// Gets or sets a value indicating whether titles are fixed automatically when episodes are added or updated.
    /// </summary>
    public bool FixAutomatically { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the title field is locked after fixing, so a metadata refresh does not bring the placeholder back.
    /// </summary>
    public bool LockTitle { get; set; } = true;

    /// <summary>
    /// Gets or sets the regular expression that matches a placeholder title (case-insensitive).
    /// </summary>
    public string PlaceholderPattern { get; set; } = DefaultPattern;
}
