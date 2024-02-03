using System.Windows;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.ViewPresenters;

public partial class IrregularVerbInfoWindow : Window
{
    public IrregularVerbInfoWindow(BaseIrregularVerb irregularVerb)
    {
        InitializeComponent();
        
        string correctData = $"Native Word: {irregularVerb.NativeWord}\n" +
                             $"Infinitive: {irregularVerb.Infinitive}\n" +
                             $"Past Simple: {irregularVerb.PastSimple}\n" +
                             $"Past Participle: {irregularVerb.PastParticiple}";
        
        _infoText.Text = correctData;
    }
    
    private void OnGotItClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}