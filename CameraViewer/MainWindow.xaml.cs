using System.Collections.Generic;
using System.Windows;
using CameraViewer.Types;
using CameraViewer.Controls;

namespace CameraViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A list of all the camera players and their connections.
        /// <para>Note: The cameras in this list must have their Dispose() methods called when they are removed.</para>
        /// </summary>
        List<Camera> _Cameras = new();

        /// <summary>
        /// A list of all the camera views in the UI.
        /// </summary>
        List<CameraView> _CameraViews = new();

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
                // Create a new camera view for the new camera.
                var camView = new CameraView();
                camView.VidView.MediaPlayer = newCamera.VlcPlayer;

                // Add camera video view to the UI.
                CamGrid.AddCameraView(camView);
                _CameraViews.Add(camView);

                newCamera.Play();
            }
        }

        /// <summary>
        /// Removes the cameras currently playing and reloads them from the camera file.
        /// </summary>
        internal void RefreshCameras()
        {
            // Remove all video views from the UI.
            CamGrid.ClearCameraViews();

            // Stop the camera streams and dispose their players.
            foreach (var camera in _Cameras)
            {
                camera.Stop();
                camera.VlcPlayer.Dispose();
            }

            // Dispose the camera views.
            foreach (var view in _CameraViews)
            {
                view.VidView.Dispose();
            }

            // Remove all the camera views from memory.
            _CameraViews.Clear();

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
