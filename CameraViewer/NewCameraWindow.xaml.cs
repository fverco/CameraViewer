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

        /// <summary>
        /// Uses all the provided connection information to add a new camera.
        /// </summary>
        private void AddNewCamera()
        {
            if (NameTextBox.Text.Length > 0 &&
                PassBox.Password.Length > 0 &&
                IpTextBox.Address != null &&
                IpTextBox.Address.Length > 6 &&
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
                MessageBox.Show("Please fill in all the fields before continuing.", "Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
