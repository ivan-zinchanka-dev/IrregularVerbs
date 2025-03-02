using MaterialDesignThemes.Wpf;

namespace IrregularVerbs.Presentation.Services.Management;

internal class ThemeManager
{
    public BaseTheme CurrentBaseTheme { get; private set; }

    public ThemeManager()
    {
        PaletteHelper paletteHelper = new PaletteHelper();
        Theme theme = paletteHelper.GetTheme();
        CurrentBaseTheme = theme.GetBaseTheme();
    }

    public bool SwitchBaseTheme(BaseTheme newBaseTheme)
    {
        if (CurrentBaseTheme == newBaseTheme)
        {
            return false;
        }
        
        PaletteHelper paletteHelper = new PaletteHelper();
        Theme theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(newBaseTheme);
        paletteHelper.SetTheme(theme);

        CurrentBaseTheme = newBaseTheme;
        
        return true;
    }
}