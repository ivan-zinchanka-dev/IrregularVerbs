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
    private readonly bool _alphabeticalOrder;

    private List<IrregularVerbAnswer> _cachedTask;
    
    public IrregularVerbsTeacher(IrregularVerbsStorage storage, int questionsCount, bool alphabeticalOrder = true)
    {
        _storage = storage;
        _questionsCount = questionsCount;
        _alphabeticalOrder = alphabeticalOrder;
    }

    public IEnumerable<IrregularVerbAnswer> GenerateTask()
    {
        if (_storage.IrregularVerbs == null)
        {
            return new List<IrregularVerbAnswer>();
        }

        List<BaseIrregularVerb> verbs = _storage.IrregularVerbs.Disorder();
        verbs = verbs.Take(_questionsCount).ToList();

        if (_alphabeticalOrder)
        {
            verbs = verbs.OrderBy(verb=>verb.Infinitive).ToList();
        }
        
        _cachedTask = new List<IrregularVerbAnswer>(verbs.Count);

        foreach (BaseIrregularVerb verb in verbs)
        {
            _cachedTask.Add(new IrregularVerbAnswer(verb));
        }

        return _cachedTask;
    }


    public CheckingResult CheckTask()
    {
        if (_cachedTask == null)
        {
            throw new Exception("No tasks to check");
        }

        CheckingResult checkingResult = new CheckingResult();
        checkingResult.AllAnswersCount = _cachedTask.Count;
        
        foreach (IrregularVerbAnswer answer in _cachedTask)
        {
            CheckAnswer(answer);

            if (answer.Result == IrregularVerbAnswer.AnswerResult.Correct)
            {
                checkingResult.CorrectAnswersCount++;
            }
        }

        return checkingResult;
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
                    input.NativeWord,
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