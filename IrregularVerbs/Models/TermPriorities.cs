using System;
using System.Collections.Generic;

namespace IrregularVerbs.Models;

[Serializable]
public class TermPriorities
{
    private readonly Dictionary<string, int> _priorities = new Dictionary<string, int>();
    [field:NonSerialized] public event Action OnChanged;

    public int GetPriority(string term)
    {
        if (_priorities.TryGetValue(term, out int priority))
        {
            return priority;
        }

        return 0;
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