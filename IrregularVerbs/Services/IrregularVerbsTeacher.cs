using System;
using System.Collections.Generic;
using System.Linq;
using IrregularVerbs.Extensions;
using IrregularVerbs.Factories;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Components;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Services;

public class IrregularVerbsTeacher
{
    private readonly IrregularVerbsStorage _storage;
    private readonly int _questionsCount;
    private readonly bool _alphabeticalOrder;
    private TermPriorities _priorities;
    
    private List<IrregularVerbAnswer> _cachedTask;
    
    public IrregularVerbsTeacher(IrregularVerbsStorage storage, int questionsCount, bool alphabeticalOrder = true)
    {
        _storage = storage;
        _questionsCount = questionsCount;
        _alphabeticalOrder = alphabeticalOrder;
    }

    public IrregularVerbsTeacher UsePriorities(TermPriorities priorities)
    {
        _priorities = priorities;
        return this;
    }

    public IEnumerable<IrregularVerbAnswer> GenerateTask()
    {
        if (_storage.IrregularVerbs == null)
        {
            return new List<IrregularVerbAnswer>();
        }

        List<BaseIrregularVerb> verbs = _storage.IrregularVerbs.Disorder();
        UpdateNotShownTermPriorities(verbs.Skip(_questionsCount));
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

            if (answer.Result == AnswerResult.Correct)
            {
                checkingResult.CorrectAnswersCount++;
            }
        }
        
        UpdateShownTermPriorities(_cachedTask);

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
            answer.Result = AnswerResult.Incorrect;
        }
    }

    private static AnswerResult ToAnswerResult(bool result)
    {
        return result ? AnswerResult.Correct : AnswerResult.Incorrect;
    }
    
    private void UpdateNotShownTermPriorities(IEnumerable<BaseIrregularVerb> verbs)
    {
        if (_priorities == null)
        {
            return;
        }
        
        foreach (BaseIrregularVerb verb in verbs)
        {
            _priorities.AppendPriority(verb.NativeWord.Term, 1);
        }
        
        _priorities.Save();
    }

    private void UpdateShownTermPriorities(IEnumerable<IrregularVerbAnswer> answers)
    {
        if (_priorities == null)
        {
            return;
        }

        foreach (IrregularVerbAnswer answer in answers)
        {
            switch (answer.Result)
            {
                case AnswerResult.Correct:
                    _priorities.SetPriority(answer.NativeWord.Term, 0);
                    break;
            
                case AnswerResult.Incorrect:
                    _priorities.AppendPriority(answer.NativeWord.Term, 2);
                    break;
            
            }
        }
        
        _priorities.Save();
        
    }

}