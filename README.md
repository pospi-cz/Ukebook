# 🎸 Ukulele Zpěvník

WPF aplikace pro správu a zobrazení písní s akordy ve formátu **ChordPro**.

## Požadavky

- **.NET 8.0 SDK** nebo novější
- **Microsoft Edge WebView2 Runtime** (obvykle již nainstalovaný ve Windows 10/11)
- Visual Studio 2022 nebo VS Code s C# extension

## Instalace NuGet balíčků

```
dotnet restore
```

### Balíčky:
- `CommunityToolkit.Mvvm` 8.3.2 — MVVM pomocné třídy
- `Microsoft.Web.WebView2` 1.0.2739.15 — vestavěný prohlížeč pro HTML zobrazení
- `Newtonsoft.Json` 13.0.3 — ukládání dat

## Spuštění

```bash
cd Ukebook
dotnet run
```

nebo otevřete `Ukebook.csproj` ve Visual Studiu a stiskněte F5.

## Architektura projektu

```
Ukebook/
├── Models/
│   └── Song.cs               # Datový model písně
├── ViewModels/
│   └── MainViewModel.cs      # Hlavní ViewModel (MVVM)
├── Views/
│   ├── MainWindow.xaml       # Hlavní okno UI
│   └── MainWindow.xaml.cs    # Code-behind
├── Services/
│   ├── ChordProParser.cs     # Parser ChordPro → HTML
│   └── SongService.cs        # Načítání/ukládání písní
├── Themes/
│   └── MainTheme.xaml        # WPF styly a témata
└── App.xaml / App.xaml.cs
```

## Data

Písně jsou uloženy v:
```
%APPDATA%\Ukebook\
├── songs_index.json          # Index všech písní (metadata)
└── Songs\
    ├── {id}.cho              # ChordPro soubor každé písně
    └── ...
```

## ChordPro formát

### Základní zápis
```
[C]Text [G]písně [Am]s akordy
```

### Direktivy
```
{title: Název písně}
{artist: Interpret}
{key: C}
{tempo: 120}
{capo: 2}
{genre: Pop}
```

### Sekce
```
{start_of_verse: Sloka 1}
[C]Text sloky [G]zde
{end_of_verse}

{start_of_chorus}
[F]Text refrénu [C]zde
{end_of_chorus}

{start_of_bridge}
[Am]Bridge [G]text
{end_of_bridge}

{comment: Hrajte pomalu}
{chorus}   ← odkaz na refrén
```

## Klávesové zkratky

| Zkratka | Akce |
|---------|------|
| Ctrl+N  | Nová píseň |
| Ctrl+E  | Upravit vybranou píseň |
| Ctrl+S  | Uložit |
| Esc     | Zrušit úpravy |
| Ctrl+T  | Přepnout světlý/tmavý motiv |
| Ctrl++  | Zvětšit písmo |
| Ctrl+-  | Zmenšit písmo |
| F5      | Obnovit seznam |

## Funkce

- ✅ Zobrazení písní ve formátu ChordPro jako HTML
- ✅ Editor s živým náhledem (split view)
- ✅ Barevné sekce (sloka/refrén/bridge)
- ✅ Transpozice akordů (+/- půltóny)
- ✅ Světlý a tmavý motiv
- ✅ Nastavitelná velikost písma
- ✅ Vyhledávání v seznamu
- ✅ Filtrování podle žánru
- ✅ Ukládání na disk (JSON + .cho soubory)
- ✅ 3 ukázkové písně při prvním spuštění

## Plánovaná rozšíření

- 📋 Import/export .cho souborů
- 🖨️ Tisk písní
- 📱 Prezentační režim (celá obrazovka)
- 🎵 Diagramy ukulele akordů
- 🔄 Automatické scrollování
- 🌐 Import z URL / online databází
