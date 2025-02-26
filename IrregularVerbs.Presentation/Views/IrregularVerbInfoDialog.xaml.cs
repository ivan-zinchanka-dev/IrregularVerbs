using System.Windows;
using IrregularVerbs.Domain.Models.Verbs;

namespace IrregularVerbs.Presentation.Views;

public partial class IrregularVerbInfoDialog : Window
{
    private const string CorrectVerbsPattern = 
        "Native Word: {0}\nInfinitive: {1}\nPast Simple: {2}\nPast Participle: {3}";
    
    public IrregularVerbInfoDialog(BaseIrregularVerb irregularVerb)
    {
        InitializeComponent();
        
        _infoText.Text = string.Format(CorrectVerbsPattern, 
            irregularVerb.NativeWord, 
            irregularVerb.Infinitive, 
            irregularVerb.PastSimple, 
            irregularVerb.PastParticiple);
    }
    
    private void OnGotItClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}