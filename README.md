<p align="center"><img src="assets/logo.png" width="160" alt="Episode Title Fixer logo"></p>

<h1 align="center">Episode Title Fixer for Jellyfin</h1>

🇬🇧 [English](#english) · 🇩🇰 [Dansk](#dansk)

---

## English

When TMDb or TheTVDB has no episode title in your metadata language, Jellyfin often ends up with a placeholder like **"Afsnit 11"** or **"Episode 11"**, while the real title sits in *Original title*.

This plugin swaps the placeholder for the original title:

- **Automatically**, right after an episode is added or its metadata is refreshed.
- **For the whole library** with the scheduled task *Fix placeholder episode titles* (runs every 24 hours, or start it by hand).
- Only the **title field is locked** afterwards, so a later refresh does not bring the placeholder back. Everything else keeps updating as usual.
- If the library saves NFO files, the new title is written to the NFO as well.

Episodes without an original title are left alone. Placeholders are recognised in many languages (Afsnit, Episode, Avsnitt, Jakso, Folge, Aflevering, Épisode, Episodio, …), and the pattern can be changed in the settings.

### Requirements

- Jellyfin **12.2** or newer

### Installation

**From the plugin repository (recommended)**

1. In Jellyfin go to **Dashboard → Plugins → Repositories** (or *Catalog → ⚙️*) and add:
   ```
   https://raw.githubusercontent.com/Tntdruid/jellyfin-plugin-episode-title-fixer/main/manifest.json
   ```
2. Open the **Catalog**, find **Episode Title Fixer** and install it.
3. Restart Jellyfin.
4. Run **Dashboard → Scheduled Tasks → Fix placeholder episode titles** once to fix the existing library.

**Manual**

1. Download the latest `.zip` from [Releases](https://github.com/Tntdruid/jellyfin-plugin-episode-title-fixer/releases).
2. Unzip it into a folder named `Episode Title Fixer_<version>` inside Jellyfin's `plugins` folder.
3. Restart Jellyfin.

### Settings

| Setting | Default | |
|---|---|---|
| Fix titles automatically | On | Fix episodes as they are added or refreshed |
| Lock the title after fixing | On | Stop a later refresh from bringing the placeholder back |
| Placeholder pattern | many languages | Regular expression, not case-sensitive |

### Building

```
dotnet publish Jellyfin.Plugin.EpisodeTitleFixer -c Release -o out
```

---

## Dansk

Når TMDb eller TheTVDB ikke har en afsnitstitel på dit metadatasprog, ender Jellyfin ofte med en pladsholder som **"Afsnit 11"**, mens den rigtige titel står i *Original titel*.

Dette plugin erstatter pladsholderen med den originale titel:

- **Automatisk**, så snart et afsnit bliver tilføjet, eller dets metadata bliver opdateret.
- **For hele biblioteket** med den planlagte opgave *Fix placeholder episode titles* (kører hver 24. time, eller start den selv).
- Bagefter bliver **kun titelfeltet låst**, så en senere opdatering ikke henter pladsholderen tilbage. Resten af metadataene opdaterer som normalt.
- Hvis biblioteket gemmer NFO-filer, bliver den nye titel også skrevet i NFO'en.

Afsnit uden originaltitel bliver ikke rørt. Pladsholdere genkendes på mange sprog (Afsnit, Episode, Avsnitt, Jakso, Folge, Aflevering, Épisode, Episodio, …), og mønsteret kan ændres i indstillingerne.

### Krav

- Jellyfin **12.2** eller nyere

### Installation

**Fra plugin-repository (anbefalet)**

1. Gå til **Dashboard → Plugins → Repositories** (eller *Katalog → ⚙️*) i Jellyfin, og tilføj:
   ```
   https://raw.githubusercontent.com/Tntdruid/jellyfin-plugin-episode-title-fixer/main/manifest.json
   ```
2. Åbn **Kataloget**, find **Episode Title Fixer**, og installér det.
3. Genstart Jellyfin.
4. Kør **Dashboard → Planlagte opgaver → Fix placeholder episode titles** én gang for at rette det eksisterende bibliotek.

**Manuelt**

1. Hent den nyeste `.zip` under [Releases](https://github.com/Tntdruid/jellyfin-plugin-episode-title-fixer/releases).
2. Pak den ud i en mappe med navnet `Episode Title Fixer_<version>` i Jellyfins `plugins`-mappe.
3. Genstart Jellyfin.

### Indstillinger

| Indstilling | Standard | |
|---|---|---|
| Ret titler automatisk | Til | Retter afsnit, når de bliver tilføjet eller opdateret |
| Lås titlen efter rettelse | Til | Forhindrer, at en senere opdatering henter pladsholderen tilbage |
| Mønster for pladsholdere | mange sprog | Regulært udtryk, store og små bogstaver er ligegyldige |

### Byg selv

```
dotnet publish Jellyfin.Plugin.EpisodeTitleFixer -c Release -o out
```

---

<!-- jellyfin-projects:start -->
## More Jellyfin projects / Flere Jellyfin-projekter

| | 🇬🇧 | 🇩🇰 |
|---|---|---|
| [Jellyfin Music Stats](https://github.com/Tntdruid/jellyfin-music-stats) | Home Assistant add-on with music listening stats: top lists, genres, moods, Wrapped | Home Assistant-add-on med lyttestatistik for musik: top-lister, genrer, humør, Wrapped |
| [Jellyfin Video Stats](https://github.com/Tntdruid/jellyfin-video-stats) | Home Assistant add-on with viewing stats for movies, series and YouTube videos: progress, binges, transcoding, Wrapped | Home Assistant-add-on med seerstatistik for film, serier og YouTube-videoer: fremdrift, binge, transcoding, Wrapped |
| [Auto Artist Playlists](https://github.com/Tntdruid/jellyfin-plugin-auto-artist-playlists) | Jellyfin plugin that hearts the songs you listen to and adds them to a playlist per artist | Jellyfin-plugin, der giver hjerte til de sange, du hører, og samler dem i en playliste pr. kunstner |
| [Tonefin](https://github.com/Tntdruid/tonefin) | Windows music player for Jellyfin that always reports what you play, so stats and plugins see every listen | Musikafspiller til Windows til Jellyfin, der altid rapporterer, hvad du spiller, så statistik og plugins ser hver lytning |
| [TvFin](https://github.com/Tntdruid/TvFin) | Minimal Jellyfin player for Samsung Tizen TVs: Quick Connect, resume where you left off, chapters, subtitles, auto-play next episode | Minimal Jellyfin-afspiller til Samsung Tizen-TV: Quick Connect, fortsæt hvor du slap, kapitler, undertekster, automatisk næste afsnit |
<!-- jellyfin-projects:end -->

---

## License / Licens

[GPL-3.0](LICENSE)
