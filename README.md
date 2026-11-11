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

Afsnit uden originaltitel bliver ikke rørt.

### Krav

- Jellyfin **12.2** eller nyere

### Installation

1. Gå til **Dashboard → Plugins → Repositories** (eller *Katalog → ⚙️*) i Jellyfin, og tilføj:
   ```
   https://raw.githubusercontent.com/Tntdruid/jellyfin-plugin-episode-title-fixer/main/manifest.json
   ```
2. Åbn **Kataloget**, find **Episode Title Fixer**, og installér det.
3. Genstart Jellyfin.
4. Kør **Dashboard → Planlagte opgaver → Fix placeholder episode titles** én gang for at rette det eksisterende bibliotek.

**Manuelt:** Hent den nyeste `.zip` under [Releases](https://github.com/Tntdruid/jellyfin-plugin-episode-title-fixer/releases), pak den ud i en mappe med navnet `Episode Title Fixer_<version>` i Jellyfins `plugins`-mappe, og genstart Jellyfin.
