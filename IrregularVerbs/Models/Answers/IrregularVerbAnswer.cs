using System.ComponentModel;
using System.Runtime.CompilerServices;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Models.Answers;

public class IrregularVerbAnswer : INotifyPropertyChanged
{
    public BaseIrregularVerb Original { get; private set; }
    public LocalizedText NativeWord { get; private set; }
    public string Infinitive { get; set; }
    public string PastSimple { get; set; }
    public string PastParticiple { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    
    private AnswerResult _result;
    
    public AnswerResult Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }
    
    public IrregularVerbAnswer(BaseIrregularVerb original)
    {
        Original = original;
        NativeWord = original.NativeWord;
        Infinitive = string.Empty;
        PastSimple = string.Empty;
        PastParticiple = string.Empty;
        _result = AnswerResult.None;
    }

    public override string ToString()
    {
        return $"{Infinitive}|{PastSimple}|{PastParticiple}";
    }
    
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}