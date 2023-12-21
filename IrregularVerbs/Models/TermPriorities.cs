using System;
using System.Collections.Generic;

namespace IrregularVerbs.Models;

public class TermPriorities
{
    private readonly Dictionary<string, int> _priorities;

    public event Action OnChanged;
    
    public TermPriorities()
    {
        _priorities = new Dictionary<string, int>();
    }

    public void SetPriority(string term, int priority)
    {
        _priorities[term] = priority;
    }
    
    public void AppendPriority(string term, int priority)
    {
        if (_priorities.ContainsKey(term))
        {
            _priorities[term] += priority;
        }
        else
        {
            _priorities.Add(term, priority);
        }
    }

}