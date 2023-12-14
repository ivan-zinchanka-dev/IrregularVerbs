using System.ComponentModel;
using System.Runtime.CompilerServices;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Services;

namespace IrregularVerbs.Models;

public class IrregularVerbAnswer : INotifyPropertyChanged
{
    public BaseIrregularVerb Original { get; private set; }
    private AnswerResult _result;
    public event PropertyChangedEventHandler PropertyChanged;
    
    public LocalizedText NativeWord { get; private set; }
    public string Infinitive { get; set; }
    public string PastSimple { get; set; }
    public string PastParticiple { get; set; }
    
    public AnswerResult Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }
    
    public enum AnswerResult : byte
    {
        None = 0,
        Correct = 1,
        Incorrect = 2,
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