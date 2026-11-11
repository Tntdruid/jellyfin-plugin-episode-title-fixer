using System;
using System.Collections.Generic;
using System.Globalization;
using Jellyfin.Plugin.EpisodeTitleFixer.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.EpisodeTitleFixer;

/// <summary>
/// Replaces placeholder episode titles such as "Afsnit 11" with the original title of the episode.
/// </summary>
public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
{
    public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
        : base(applicationPaths, xmlSerializer)
    {
        Instance = this;
    }

    public static Plugin? Instance { get; private set; }

    public override string Name => "Episode Title Fixer";

    public override Guid Id => Guid.Parse("b6f1c2e4-3d8a-4f7b-9e21-5a0c7d4e8f13");

    public override string Description => "Replaces placeholder episode titles like \"Afsnit 11\" or \"Episode 11\" with the original title.";

    public IEnumerable<PluginPageInfo> GetPages()
    {
        return
        [
            new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html", GetType().Namespace)
            }
        ];
    }
}
