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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using CameraViewer.Types;

namespace CameraViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialize the Main Window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetupCameras();
        }

        /// <summary>
        /// Setup all the saved cameras.
        /// </summary>
        void SetupCameras()
        {
            CameraPanel.Orientation = Orientation.Horizontal;
        }

        /// <summary>
        /// Add a new camera.
        /// </summary>
        /// <param name="newCamera">The new camera.</param>
        internal void AddCamera(Camera newCamera)
        {
            if (newCamera != null)
            {
                CameraPanel.Children.Add(newCamera.VideoView());
                newCamera.Play();
            }
        }

        /// <summary>
        /// Opens the New Camera window for the user to add a new camera.
        /// </summary>
        /// <param name="sender">The object that sent the event signal.</param>
        /// <param name="e">The event arguments.</param>
        private void OpenNewCameraWindow(object sender, RoutedEventArgs e)
        {
            var newCamWindow = new NewCameraWindow();
            newCamWindow.Show();
        }
    }
}
