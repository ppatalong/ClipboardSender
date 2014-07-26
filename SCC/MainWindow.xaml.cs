using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SCC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClipboardSender cliboardSender = new ClipboardSender();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void TrySimulatePasteText()
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(200);
                InvokeAsync(() => cliboardSender.TrySendClipboardText());
            });
        }
        
        private void InvokeAsync(Action actionToInvoke)
        {
            if (!this.Dispatcher.CheckAccess())
                this.Dispatcher.BeginInvoke(actionToInvoke);
            else
                actionToInvoke();
        }

    }
}
