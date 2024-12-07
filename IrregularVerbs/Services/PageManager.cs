using System;
using System.Collections.Generic;
using System.Windows.Controls;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.Views;

namespace IrregularVerbs.Services;

public class PageManager
{
    // TODO refactor this
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
        
        _pageFactories = new Dictionary<Type, Func<object>>()
        {
            { typeof(StartPage), _startPageFactory.Create },
            { typeof(RevisePage), _revisePageFactory.Create },
            { typeof(CheckPage), _checkPageFactory.Create }
        };
    }

    public bool SwitchTo<TPage>()
    {
        Type pageType = typeof(TPage);

        if (pageType == _currentPage?.GetType())
        {
            return false;
        }

        if (_pageFactories.TryGetValue(pageType, out Func<object> creationMethod))
        {
            if (creationMethod() is Page page)
            {
                _currentPage = page;
                OnPageCreated?.Invoke(_currentPage);
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public void SwitchForward()
    {
        
    }
}