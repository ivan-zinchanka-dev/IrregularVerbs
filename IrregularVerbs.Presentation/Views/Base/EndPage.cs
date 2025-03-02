using System.Windows.Controls;
using System.Windows.Input;

namespace IrregularVerbs.Presentation.Views.Base;

internal abstract class EndPage : Page
{
    protected void RegisterBackCommand(ICommand backCommand)
    {
        CommandBindings.Add(new CommandBinding(
            NavigationCommands.BrowseBack,
            (sender, args) => backCommand.Execute(args.Parameter),
            (sender, args) => args.CanExecute = backCommand.CanExecute(args.Parameter)));
    }
}