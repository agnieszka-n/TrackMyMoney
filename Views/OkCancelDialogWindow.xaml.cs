using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.CommandWpf;
using TrackMyMoney.Services.Contracts.Dialogs;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class OkCancelDialogWindow : Window, IOkCancelDialogWindow, IOkCancelDialogWindowViewModel
    {
        public string Message { get; private set; }
        public RelayCommand OkClickCommand { get; }
        public RelayCommand CancelClickCommand { get; }

        private bool result;

        public OkCancelDialogWindow(Window owner)
        {
            Owner = owner;
            InitializeComponent();
            DataContext = this;

            OkClickCommand = new RelayCommand(OkClick);
            CancelClickCommand = new RelayCommand(CancelClick);
        }

        public bool ShowDialog(string message)
        {
            Message = message;
            ShowDialog();
            return result;
        }

        private void OkClick()
        {
            result = true;
            Close();
        }

        private void CancelClick()
        {
            result = false;
            Close();
        }
    }
}
