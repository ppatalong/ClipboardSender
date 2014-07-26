using System.Windows.Input;

namespace SCC
{
    public static class Commands
    {
        public static readonly ICommand PasteCommand = new RelayCommand(window => ((MainWindow)window).TrySimulatePasteText());
    }
}
