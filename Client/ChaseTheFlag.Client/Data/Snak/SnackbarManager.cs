using MudBlazor;

namespace ChaseTheFlag.Client.Data.Snak
{
    /// <summary>
    /// Manager class for displaying snackbars in the ChaseTheFlag client application.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SnackbarManager"/> class.
    /// </remarks>
    /// <param name="snackbar">The injected ISnackbar instance.</param>
    public class SnackbarManager(ISnackbar snackbar)
    {
        private readonly ISnackbar _snackbar = snackbar;

        /// <summary>
        /// Displays a snackbar with the specified text and severity.
        /// </summary>
        /// <param name="text">The text to display in the snackbar.</param>
        /// <param name="severity">The severity level of the snackbar (default is Severity.Warning).</param>
        public void Show(string text, Severity severity = Severity.Warning)
        {
            // Configure snackbar position and appearance
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
            _snackbar.Configuration.SnackbarVariant = Variant.Filled;

            // Add the snackbar with the specified text and severity
            _snackbar.Add(text, severity);
        }
    }
}
