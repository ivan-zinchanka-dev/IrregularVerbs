using System;
using System.Collections.Generic;
using System.Windows.Controls;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs.Services;

public class PageManager
{
    private readonly IAbstractFactory<StartPage> _startPageFactory;
    private readonly IAbstractFactory<RevisePage> _revisePageFactory;
    private readonly IAbstractFactory<CheckPage> _checkPageFactory;
    
    public event Action<Page> OnPageCreated;

    private readonly Dictionary<Type, Func<object>> _pageFactories;
    private Page _currentPage;
    
    public PageManager(
        IAbstractFactory<StartPage> startPageFactory, 
        IAbstractFactory<RevisePage> revisePageFactory, 
        IAbstractFactory<CheckPage> checkPageFactory)
    {
        _startPageFactory = startPageFactory;
        _revisePageFactory = revisePageFactory;
        _checkPageFactory = checkPageFactory;

        //_currentPage = _startPageFactory
        
        _pageFactories = new Dictionary<Type, Func<object>>()
        {
            { typeof(StartPage), _startPageFactory.Create },
            { typeof(RevisePage), _revisePageFactory.Create },
            { typeof(CheckPage), _checkPageFactory.Create }
        };
    }

    public bool SwitchTo<TPage>()
    {
        var pageType = typeof(TPage);
        
        if (_pageFactories.TryGetValue(pageType, out Func<object> creationMethod))
        {
            Page page = creationMethod() as Page;
            OnPageCreated?.Invoke(page);

            return true;
        }

        return false;
    }
}