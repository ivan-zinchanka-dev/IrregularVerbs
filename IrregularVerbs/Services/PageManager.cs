using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace IrregularVerbs.Services;

public class PageManager
{
    private readonly IServiceProvider _serviceProvider;
    private Page _currentPage;
    
    public event Action<Page> OnPageCreated;
    
    public PageManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool SwitchTo<TPage>() where TPage : Page
    {
        Type pageType = typeof(TPage);

        if (pageType == _currentPage?.GetType())
        {
            return false;
        }

        if (_serviceProvider.GetService<TPage>() is Page page)
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
}