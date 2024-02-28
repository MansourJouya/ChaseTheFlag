using ChaseTheFlag.Client.Pages.Addition;
using MudBlazor;

namespace ChaseTheFlag.Client.Data.Dialog
{
    /// <summary>
    /// Service class for managing dialogs in the ChaseTheFlag client application.
    /// </summary>
    public class MyDialog
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyDialog"/> class.
        /// </summary>
        /// <param name="dialogService">The injected dialog service.</param>
        public MyDialog(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Shows a dialog asynchronously.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="contentText">The text content of the dialog.</param>
        /// <param name="textButtonSubmit">The text for the submit button.</param>
        /// <param name="colorButtonSubmit">The color of the submit button.</param>
        /// <param name="dialogOptions">Additional options for the dialog.</param>
        /// <param name="textButtonCancel">The text for the cancel button.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean indicating whether the dialog was canceled.</returns>
        public async Task<bool> Show(string title, string contentText, string textButtonSubmit, Color colorButtonSubmit, DialogOptions dialogOptions, string textButtonCancel = "Cancel")
        {
            var parameters = new DialogParameters<MyDialogComponent>
            {
                { x => x.ContentText, contentText },
                { x => x.TextButtonSubmit, textButtonSubmit },
                { x => x.TextButtonCancel, textButtonCancel },
                { x => x.ColorButtonSubmit, colorButtonSubmit }
            };
            var options = dialogOptions;
            var dialog = await _dialogService.ShowAsync<MyDialogComponent>(title, parameters, options);
            var result = await dialog.Result;
            return result.Canceled;
        }
    }
}
