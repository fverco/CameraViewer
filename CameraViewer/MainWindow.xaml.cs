using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CameraViewer.Types;
using LibVLCSharp.WPF;

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
        /// A List of all the video views used to display the cameras.
        /// <para>Note: The video views in this list must have their Dispose() methods called when they are removed.</para>
        /// </summary>
        List<VideoView> _VideoViews = new();

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
                // Create new video view.
                var newVidView = new VideoView()
                {
                    MediaPlayer = newCamera.VlcPlayer,
                    MinHeight = 200,
                    MinWidth = 200
                };

                // Add to the video views list.
                _VideoViews.Add(newVidView);

                // Add to the UI.
                CameraPanel.Children.Add(newVidView);

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

            // Dispose all the video views from memory.
            foreach (VideoView vidview in _VideoViews)
                vidview.Dispose();

            // Remove all the video views from the list.
            _VideoViews.Clear();

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
