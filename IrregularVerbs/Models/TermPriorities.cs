using System;
using System.Collections.Generic;

namespace IrregularVerbs.Models;

[Serializable]
public class TermPriorities
{
    private readonly Dictionary<string, int> _priorities;
    [field:NonSerialized] public event Action OnChanged;
    
    public TermPriorities()
    {
        _priorities = new Dictionary<string, int>();
    }

    public TermPriorities SetPriority(string term, int priority)
    {
        _priorities[term] = priority;
        return this;
    }
    
    public TermPriorities AppendPriority(string term, int priority)
    {
        if (_priorities.ContainsKey(term))
        {
            _priorities[term] += priority;
        }
        else
        {
            _priorities.Add(term, priority);
        }
        
        return this;
    }

    public void Save()
    {
        OnChanged?.Invoke();
    }

}