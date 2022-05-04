using System;
using System.Windows;
using System.Windows.Input;
using CameraViewer.Types;
using System.Xml;

namespace CameraViewer
{
    /// <summary>
    /// Interaction logic for NewCameraWindow.xaml
    /// </summary>
    public partial class NewCameraWindow : Window
    {
        /// <summary>
        /// Initialize the New Camera Window.
        /// </summary>
        public NewCameraWindow()
        {
            InitializeComponent();
        }

        private void KeyPressEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AddNewCamera();
        }

        /// <summary>
        /// Add a new camera when Add is clicked.
        /// </summary>
        /// <param name="sender">The object that sent the button click event signal.</param>
        /// <param name="e">The event arguments.</param>
        private void AddClicked(object sender, RoutedEventArgs e)
        {
            AddNewCamera();
        }

        /// <summary>
        /// Closes the window when Cancel is clicked.
        /// </summary>
        /// <param name="sender">The object that sent the button click event signal.</param>
        /// <param name="e">The event arguments.</param>
        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TestConnectionClicked(object sender, RoutedEventArgs e)
        {
            // Check if a valid IP address and port number is provided.
            if (InputValidation.VerifyIPAddress(IpTextBox.Address) &&
                PortTextBox.Text.Length > 0)
            {
                var portNumberIsValid = Int32.TryParse(PortTextBox.Text, out int portNumber);

                if (portNumberIsValid)
                {
                    // Test the connection.
                    var (ipSuccess, portSuccess) = Network.TestConnection(IpTextBox.Address, portNumber);

                    if (ipSuccess)
                    {
                        if (portSuccess)
                        {
                            MessageBox.Show("The connection was successful!", "Connection success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show($"The device's port {PortTextBox.Text} is not open.", "Port failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("The connection failed. Please make sure the IP address and port is correct and that the device is currently powered on.", "Connection failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show($"Please provide a valid port number.", "Invalid port number", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Please provide a valid IP address and port number before testing.", "Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Called whenever a character is entered in the port number field. This validates a character entered to make sure it is allowed as input.
        /// </summary>
        /// <param name="sender">The component that triggered the event.</param>
        /// <param name="e">The arguments containing information about the character entered.</param>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            InputValidation.NumberValidation(sender, e);
        }

        /// <summary>
        /// Called whenever a key is entered in the port number field. This will check if the space key is pressed and will ignore it as input.
        /// </summary>
        /// <param name="sender">The component that triggered the event.</param>
        /// <param name="e">The arguments containing information about the key pressed.</param>
        private void SpaceKeyPressedValidation(object sender, KeyEventArgs e)
        {
            InputValidation.SpaceKeyPressedValidation(sender, e);
        }

        /// <summary>
        /// Uses all the provided connection information to add a new camera.
        /// </summary>
        private void AddNewCamera()
        {
            if (NameTextBox.Text.Length > 0 &&
                PassBox.Password.Length > 0 &&
                InputValidation.VerifyIPAddress(IpTextBox.Address) &&
                PortTextBox.Text.Length > 0 &&
                ParametersTextBox.Text.Length > 0)
            {
                var connString = Crypto.Protect($"rtsp://{NameTextBox.Text}:{PassBox.Password}@{IpTextBox.Address}:{PortTextBox.Text}{ParametersTextBox.Text}");
                var newCamera = new Camera(CamNameTextBox.Text, connString);

                try
                {
                    XmlHandler.WriteCameraToFile(newCamera.Name, newCamera.ConnectionString);
                    ((MainWindow)Application.Current.MainWindow).AddCamera(newCamera);

                    this.Close();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Duplicate entry", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (XmlException)
                {
                    MessageBoxResult mbResult = MessageBox.Show("The camera file has corrupt data and cannot be written to.\nOverwrite it with new data? (Note: This will remove all your existing cameras.)", "Corrupt camera file", MessageBoxButton.YesNo, MessageBoxImage.Error);

                    if (mbResult == MessageBoxResult.Yes)
                    {
                        XmlHandler.DeleteCameraFile();
                        XmlHandler.WriteCameraToFile(newCamera.Name, newCamera.ConnectionString);
                        ((MainWindow)Application.Current.MainWindow).RefreshCameras();

                        this.Close();
                    }
                    else if (mbResult == MessageBoxResult.No)
                        MessageBox.Show("Cannot write to camera file.");
                }
            }
            else
                MessageBox.Show("Please fill in all the fields and ensure that they are correct.", "Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
