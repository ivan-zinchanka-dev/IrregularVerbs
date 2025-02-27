using IrregularVerbs.Domain.Extensions;
using IrregularVerbs.Domain.Factories;
using IrregularVerbs.Domain.Models.Answers;
using IrregularVerbs.Domain.Models.Cache;
using IrregularVerbs.Domain.Models.Components;
using IrregularVerbs.Domain.Models.Configs;
using IrregularVerbs.Domain.Models.Verbs;
using IrregularVerbs.Domain.Services.AppData;
using Microsoft.Extensions.Logging;

namespace IrregularVerbs.Domain.Services.Testing;

public class IrregularVerbsTeacher
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly IrregularVerbsStorage _storage;
    private readonly IrregularVerbsFactory _verbsFactory;
    private readonly ILogger<IrregularVerbsTeacher> _logger;
    private readonly CacheService _cacheService;

    private bool _usePriorities;
    private List<IrregularVerbAnswer> _cachedTask;
    
    public IrregularVerbsTeacher(
        ApplicationSettings appSettings, 
        IrregularVerbsStorage storage, 
        IrregularVerbsFactory verbsFactory,
        ILogger<IrregularVerbsTeacher> logger,
        CacheService cacheService)
    {
        _applicationSettings = appSettings;
        _storage = storage;
        _verbsFactory = verbsFactory;
        _logger = logger;
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
            _logger.LogError("The irregular verbs collection is null");
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
        
        _logger.LogInformation("The task was generated successfully");

        return _cachedTask;
    }

    public CheckingResult CheckTask()
    {
        if (_cachedTask == null)
        {
            _logger.LogError("There is no task to check");
            return new CheckingResult(0, 0);
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
        
        _logger.LogInformation("The task was checked successfully");
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