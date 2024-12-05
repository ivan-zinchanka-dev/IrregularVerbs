using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IrregularVerbs.CodeBase;

public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    public virtual bool HasErrors => false;
    public virtual IEnumerable GetErrors(string propertyName) => Array.Empty<object>();
    
    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}