namespace TrackMyMoney.Services.Contracts.Dialogs
{
    public interface IDialogService
    {
        /// <summary>
        /// Shows a dialog with the given message and returns its result.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ShowDialog(string message);
    }
}
