using MaterialDesignThemes.Wpf;

namespace IrregularVerbs.CodeBase.ThemeManagement;

public class ThemeManager
{
    private BaseTheme _currentBaseTheme;

    public ThemeManager()
    {
        PaletteHelper paletteHelper = new PaletteHelper();
        Theme theme = paletteHelper.GetTheme();
        _currentBaseTheme = theme.GetBaseTheme();
    }

    public bool SwitchBaseTheme(BaseTheme newBaseTheme)
    {
        if (_currentBaseTheme == newBaseTheme)
        {
            return false;
        }
        
        PaletteHelper paletteHelper = new PaletteHelper();
        Theme theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(newBaseTheme);
        paletteHelper.SetTheme(theme);

        _currentBaseTheme = newBaseTheme;
        
        return true;
    }
}