using UkuleleSongbook.Services;

namespace UkuleleSongbook;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        SongService.EnsureDataDirectory();
        // Aplikuj výchozí světlé téma hned při startu
        ThemeService.Apply(dark: false);
    }
}
