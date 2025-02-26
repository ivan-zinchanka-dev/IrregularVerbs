using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IrregularVerbs.Presentation.Services.Management;

public class PageManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PageManager> _logger;
    private Page _currentPage;
    
    public event Action<Page> OnPageCreated;
    
    public PageManager(IServiceProvider serviceProvider, ILogger<PageManager> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public bool SwitchTo<TPage>() where TPage : Page
    {
        Type pageType = typeof(TPage);

        if (pageType == _currentPage?.GetType())
        {
            _logger.LogWarning("Attempt to switch to the same page of type '{0}'", pageType.ToString());
            return false;
        }

        if (_serviceProvider.GetService<TPage>() is Page page)
        {
            _currentPage = page;
            _logger.LogInformation("Switching to page of type'{0}'", pageType.ToString());
            OnPageCreated?.Invoke(_currentPage);
            return true;
        }
        else
        {
            _logger.LogError("Page of type '{0}' not found", pageType.ToString());
            return false;
        }
    }
}