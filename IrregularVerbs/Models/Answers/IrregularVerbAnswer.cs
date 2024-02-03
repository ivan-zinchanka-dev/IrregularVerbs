using System.ComponentModel;
using System.Runtime.CompilerServices;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Services;

namespace IrregularVerbs.Models.Answers;

public class IrregularVerbAnswer : INotifyPropertyChanged
{
    public BaseIrregularVerb Original { get; private set; }
    public LocalizedText NativeWord { get; private set; }
    public string Infinitive { get; set; }
    public string PastSimple { get; set; }
    public string PastParticiple { get; set; }
    public int InstanceId { get; }
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
    
    private static int _instancesCount = 0;
    
    public IrregularVerbAnswer(BaseIrregularVerb original)
    {
        Original = original;
        NativeWord = original.NativeWord;
        Infinitive = string.Empty;
        PastSimple = string.Empty;
        PastParticiple = string.Empty;
        InstanceId = _instancesCount++;
        _result = AnswerResult.None;
    }
    
    public bool HasEmptyFields()
    {
        return Infinitive == string.Empty || PastSimple == string.Empty || PastParticiple == string.Empty;
    }

    public override string ToString()
    {
        return $"{Infinitive}|{PastSimple}|{PastParticiple}";
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}