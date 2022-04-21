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
using CameraViewer.Types;

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

        /// <summary>
        /// Add a new camera when Add is clicked.
        /// </summary>
        /// <param name="sender">The object that sent the button click event signal.</param>
        /// <param name="e">The event arguments.</param>
        private void AddClicked(object sender, RoutedEventArgs e)
        {
            var connString = $"rtsp://{NameTextBox.Text}:{PassBox.Password}@{IpTextBox.Address}:{PortTextBox.Text}{ParametersTextBox.Text}";

            MessageBox.Show(connString);

            var newCamera = new Camera(CamNameTextBox.Text, connString);

            ((MainWindow)Application.Current.MainWindow).AddCamera(newCamera);

            this.Close();
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
    }
}
