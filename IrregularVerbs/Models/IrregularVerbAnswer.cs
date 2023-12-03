using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IrregularVerbs.Models;

public class IrregularVerbAnswer : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    public string Term { get; set; }
    public string Infinitive { get; set; }
    public string PastSimple { get; set; }
    public string PastParticiple { get; set; }

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
    
    public IrregularVerbAnswer()
    {
        Term = string.Empty;
        Infinitive = string.Empty;
        PastSimple = string.Empty;
        PastParticiple = string.Empty;
        _result = AnswerResult.None;
    }
    
    public enum AnswerResult : byte
    {
        None = 0,
        Correct = 1,
        Incorrect = 2,
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