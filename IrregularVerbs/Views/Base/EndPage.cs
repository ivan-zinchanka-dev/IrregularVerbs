﻿using System.Windows.Controls;
using System.Windows.Input;

namespace IrregularVerbs.Views.Base;

public abstract class EndPage : Page
{
    protected void RegisterBackCommand(ICommand backCommand)
    {
        CommandBindings.Add(new CommandBinding(
            NavigationCommands.BrowseBack,
            (sender, args) => backCommand.Execute(args.Parameter),
            (sender, args) => args.CanExecute = backCommand.CanExecute(args.Parameter)));
    }
}