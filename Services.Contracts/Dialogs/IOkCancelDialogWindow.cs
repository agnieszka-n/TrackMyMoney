using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.Services.Contracts.Dialogs
{
    public interface IOkCancelDialogWindow
    {
        /// <summary>
        /// Shows a dialog containing the given message. Returns a result based on the user's action.
        /// </summary>
        bool ShowDialog(string message);
    }
}
