using System.Text.RegularExpressions;
using System.Windows.Input;
using System;

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

        /// <summary>
        /// Checks if an IP address is valid.
        /// </summary>
        /// <param name="ipAddress">The provided IP address.</param>
        /// <returns>True if the IP address is valid.</returns>
        static public bool VerifyIPAddress(string ipAddress)
        {
            if (ipAddress != null)
            {
                var ipPartsArray = ipAddress.Split('.');

                if (ipPartsArray.Length == 4)
                {
                    foreach (var ipPart in ipPartsArray)
                    {
                        var IsANumber = Int32.TryParse(ipPart, out int ipPartNum);

                        if (!IsANumber || ipPartNum > 254 || ipPartNum < 0)
                            return false;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
