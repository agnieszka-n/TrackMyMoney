using System;
using TrackMyMoney.Services.Contracts.Dialogs;

namespace TrackMyMoney.Services.Dialogs
{
    /// <summary>
    /// A dialog wrapper which will create a new dialog window every time it needs to be shown.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly Func<IOkCancelDialogWindow> createDialog;

        public DialogService(Func<IOkCancelDialogWindow> createDialogCallback)
        {
            this.createDialog = createDialogCallback;
        }

        public bool ShowDialog(string message)
        {
            // Create a new dialog window each time because an already closed one can't be shown again.
            IOkCancelDialogWindow okCancelDialog = createDialog();
            return okCancelDialog.ShowDialog(message);
        }
    }
}
