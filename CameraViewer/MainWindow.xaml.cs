using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CameraViewer.Types;

namespace CameraViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A List of all the camera players and their connections.
        /// <para>Note: The cameras in this list must have their Dispose() methods called when they are removed.</para>
        /// </summary>
        List<Camera> _Cameras = new();

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

            if (XmlHandler.CameraFileExists())
            {
                // Read the camera info from the XML file.
                _Cameras = XmlHandler.ReadAllCamerasFromFile();

                // Add the cameras to the UI.
                foreach (var cam in _Cameras)
                    AddCamera(cam);
            }
        }

        /// <summary>
        /// Add a new camera.
        /// </summary>
        /// <param name="newCamera">The new camera.</param>
        internal void AddCamera(Camera newCamera)
        {
            if (newCamera != null)
            {
                // Add camera video view to the UI.
                CameraPanel.Children.Add(newCamera.VidView);

                newCamera.Play();
            }
        }

        /// <summary>
        /// Removes the cameras currently playing and reloads them from the camera file.
        /// </summary>
        internal void RefreshCameras()
        {
            // Remove all video views from the UI.
            CameraPanel.Children.Clear();

            // Stop the camera streams and dispose their players and views.
            foreach (var camera in _Cameras)
            {
                camera.Stop();
                camera.VlcPlayer.Dispose();
                camera.VidView.Dispose();
            }

            // Remove all cameras from memory.
            _Cameras.Clear();

            // Read the camera info from the XML file.
            _Cameras = XmlHandler.ReadAllCamerasFromFile();

            // Add the cameras to the UI.
            foreach (var cam in _Cameras)
                AddCamera(cam);
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

        /// <summary>
        /// Triggered when the refresh button is clicked. This will refresh the cameras from the camera file.
        /// </summary>
        /// <param name="sender">The object that sent the event signal.</param>
        /// <param name="e">The event arguments.</param>
        private void RefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            RefreshCameras();
        }
    }
}
