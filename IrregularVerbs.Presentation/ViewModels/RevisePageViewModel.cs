﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using IrregularVerbs.Domain.Models.Verbs;
using IrregularVerbs.Domain.Services.Testing;
using IrregularVerbs.Presentation.Services.Management;
using IrregularVerbs.Presentation.ViewModels.Base;
using IrregularVerbs.Presentation.Views;

namespace IrregularVerbs.Presentation.ViewModels;

internal class RevisePageViewModel : BaseViewModel
{
    private ObservableCollection<BaseIrregularVerb> _irregularVerbs = new ObservableCollection<BaseIrregularVerb>();
    private RelayCommand _backCommand;
    
    private readonly IrregularVerbsStorage _irregularVerbsStorage;
    private readonly PageManager _pageManager;
    
    public ObservableCollection<BaseIrregularVerb> IrregularVerbs
    {
        get => _irregularVerbs;

        set
        {
            _irregularVerbs = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand BackCommand
    {
        get
        {
            return _backCommand ??= new RelayCommand(obj =>
            {
                _pageManager.SwitchTo<StartPage>();
            });
        }
    }

    public RevisePageViewModel(IrregularVerbsStorage irregularVerbsStorage, PageManager pageManager)
    {
        _irregularVerbsStorage = irregularVerbsStorage;
        _pageManager = pageManager;
        
        IrregularVerbs = new ObservableCollection<BaseIrregularVerb>(_irregularVerbsStorage.IrregularVerbs);
    }
}