using System;
using System.Collections.Generic;
using System.Linq;
using IrregularVerbs.CodeBase.Extensions;
using IrregularVerbs.Factories;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Components;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Services;

public class IrregularVerbsTeacher
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly IrregularVerbsStorage _storage;
    private readonly IrregularVerbsFactory _verbsFactory;
    private readonly CacheService _cacheService;

    private bool _usePriorities;
    private List<IrregularVerbAnswer> _cachedTask;
    
    public IrregularVerbsTeacher(
        ApplicationSettings appSettings, 
        IrregularVerbsStorage storage, 
        IrregularVerbsFactory verbsFactory,
        CacheService cacheService)
    {
        _applicationSettings = appSettings;
        _storage = storage;
        _verbsFactory = verbsFactory;
        _cacheService = cacheService;
    }

    public IrregularVerbsTeacher UsePriorities()
    {
        _usePriorities = true;
        return this;
    }

    public IEnumerable<IrregularVerbAnswer> GenerateTask()
    {
        if (_storage.IrregularVerbs == null)
        {
            return new List<IrregularVerbAnswer>();
        }

        IEnumerable<BaseIrregularVerb> verbs = _storage.IrregularVerbs.Disorder();
        int questionsCount = _applicationSettings.VerbsCount; 
        
        OrderByPriority(ref verbs);
        UpdateNotShownTermPriorities(verbs.Skip(questionsCount));
        
        verbs = verbs.Take(questionsCount).ToList();

        if (_applicationSettings.AlphabeticalOrder)
        {
            verbs = verbs.OrderBy(verb=>verb.Infinitive).ToList();
        }
        
        _cachedTask = new List<IrregularVerbAnswer>(verbs.Count());

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

        CheckingResult checkingResult = new CheckingResult
        {
            AllAnswersCount = _cachedTask.Count
        };

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
            BaseIrregularVerb input = _verbsFactory.FromAnswer(answer);

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

    private void OrderByPriority(ref IEnumerable<BaseIrregularVerb> verbs)
    {
        if (!_usePriorities)
        {
            return;
        }

        verbs = verbs.OrderByDescending(
            verb => _cacheService.TermPriorities.GetPriority(verb.NativeWord.Term))
            .ToList();
    }

    private void UpdateNotShownTermPriorities(IEnumerable<BaseIrregularVerb> verbs)
    {
        if (!_usePriorities)
        {
            return;
        }

        TermPriorities termPriorities = _cacheService.TermPriorities;
        
        foreach (BaseIrregularVerb verb in verbs)
        {
            termPriorities.AppendPriority(verb.NativeWord.Term, 1);
        }
        
        termPriorities.Save();
    }

    private void UpdateShownTermPriorities(IEnumerable<IrregularVerbAnswer> answers)
    {
        if (!_usePriorities)
        {
            return;
        }

        TermPriorities termPriorities = _cacheService.TermPriorities;
        
        foreach (IrregularVerbAnswer answer in answers)
        {
            switch (answer.Result)
            {
                case AnswerResult.Correct:
                    termPriorities.SetPriority(answer.NativeWord.Term, 0);
                    break;
            
                case AnswerResult.Incorrect:
                    termPriorities.AppendPriority(answer.NativeWord.Term, 2);
                    break;
            }
        }
        
        termPriorities.Save();
    }

}