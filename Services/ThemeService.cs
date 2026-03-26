namespace Ukebook.Services;

public static class ThemeService
{
    private const string LightUri = "Themes/LightTheme.xaml";
    private const string DarkUri  = "Themes/DarkTheme.xaml";

    public static bool IsDark { get; private set; } = false;

    public static void Apply(bool dark)
    {
        IsDark = dark;
        var uri  = new Uri(dark ? DarkUri : LightUri, UriKind.Relative);
        var dict = new ResourceDictionary { Source = uri };

        var appDicts = Application.Current.Resources.MergedDictionaries;

        // Odstraň přesně LightTheme nebo DarkTheme — ne MainTheme!
        var existing = appDicts.FirstOrDefault(d =>
            d.Source?.OriginalString is string s &&
            (s.EndsWith("LightTheme.xaml") || s.EndsWith("DarkTheme.xaml")));

        if (existing is not null) appDicts.Remove(existing);

        // Vložit na index 0 — musí být PŘED MainTheme.xaml,
        // jinak MainTheme přebije brushe dříve než je DynamicResource načte
        appDicts.Insert(0, dict);
    }

    public static void Toggle() => Apply(!IsDark);
}
