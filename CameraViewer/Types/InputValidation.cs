using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CameraViewer.Types
{
    /// <summary>
    /// Class used to validate input values.
    /// </summary>
    internal class InputValidation
    {
        /// <summary>
        /// Ensures that only numbers are entered.
        /// <para>Note: This does not filter out white spaces. Use <see cref="SpaceKeyPressedValidation(object, KeyEventArgs)"/> for this purpose.</para>
        /// </summary>
        /// <param name="sender">The component that triggered the event.</param>
        /// <param name="e">The arguments containing information about the key pressed.</param>
        static public void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Filters out white spaces when pressed.
        /// </summary>
        /// <param name="sender">The component that triggered the event.</param>
        /// <param name="e">The arguments containing information about the key pressed.</param>
        static public void SpaceKeyPressedValidation(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
