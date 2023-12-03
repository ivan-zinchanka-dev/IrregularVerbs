using System;
using System.Collections.Generic;
using System.Linq;
using IrregularVerbs.Extensions;
using IrregularVerbs.Factories;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Services;

public class IrregularVerbsTeacher
{
    private readonly IrregularVerbsStorage _storage;
    private readonly int _questionsCount;

    private List<IrregularVerbAnswer> _cachedTask;
    
    public IrregularVerbsTeacher(IrregularVerbsStorage storage, int questionsCount)
    {
        _storage = storage;
        _questionsCount = questionsCount;
    }

    public IEnumerable<IrregularVerbAnswer> GenerateTask()
    {
        if (_storage.IrregularVerbs == null)
        {
            return new List<IrregularVerbAnswer>();
        }
        
        List<BaseIrregularVerb> verbs = _storage.IrregularVerbs.Disorder().Take(_questionsCount).ToList();
        _cachedTask = new List<IrregularVerbAnswer>(verbs.Count);

        foreach (BaseIrregularVerb verb in verbs)
        {
            _cachedTask.Add(new IrregularVerbAnswer(verb));
        }

        return _cachedTask;
    }


    public void CheckTask()
    {
        if (_cachedTask == null)
        {
            return;
        }

        foreach (IrregularVerbAnswer answer in _cachedTask)
        {
            CheckAnswer(answer);
        }
    }

    private void CheckAnswer(IrregularVerbAnswer answer)
    {
        try
        {
            BaseIrregularVerb original = answer.Original;
            BaseIrregularVerb input = IrregularVerbsFactory.FromAnswer(answer);

            if (original is VolatileIrregularVerb && input is FixedIrregularVerb)
            {
                input = new VolatileIrregularVerb(
                    input.Term,
                    new VolatileForm(input.Infinitive),
                    new VolatileForm(input.PastSimple),
                    new VolatileForm(input.PastParticiple));
                
            }
            
            answer.Result = ToAnswerResult(original.Inspect(input));
            
        }
        catch (Exception ex)
        {
            answer.Result = IrregularVerbAnswer.AnswerResult.Incorrect;
        }
    }

    private static IrregularVerbAnswer.AnswerResult ToAnswerResult(bool result)
    {
        return result ? IrregularVerbAnswer.AnswerResult.Correct : IrregularVerbAnswer.AnswerResult.Incorrect;
    }

}