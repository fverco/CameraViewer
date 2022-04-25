using LibVLCSharp.Shared;
using LibVLCSharp.WPF;

namespace CameraViewer.Types
{
    /// <summary>
    /// A class for setting up cameras to view in the window.
    /// </summary>
    internal class Camera
    {
        /// <summary>
        /// The display name of the camera.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The entire connection string for connecting to the camera stream.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The media player used by VLC to play the stream.
        /// </summary>
        MediaPlayer VlcPlayer { get; }

        /// <summary>
        /// The view used to display the stream.
        /// </summary>
        VideoView VidView { get; }

        /// <summary>
        /// The Camera class constructor.
        /// </summary>
        /// <param name="name">The display name of the camera.</param>
        /// <param name="conString">The connection string for the stream.</param>
        public Camera(string name, string conString)
        {
            Name = name;
            ConnectionString = conString;
            VlcPlayer = new MediaPlayer(Globals.CameraLibVLC);
            VidView = new VideoView()
            {
                MediaPlayer = VlcPlayer,
                MinHeight = 200,
                MinWidth = 200
            };
        }

        /// <summary>
        /// The Camera class desctructor.
        /// <para>This will stop the stream if it is playing and dispose of all LibVLC objects.</para>
        /// </summary>
        ~Camera()
        {
            Stop();
            VlcPlayer.Dispose();
            VidView.Dispose();
        }

        /// <summary>
        /// Returns the view used to display the stream in the UI.
        /// </summary>
        /// <returns>A VideoView object.</returns>
        public VideoView VideoView()
        {
            return VidView;
        }

        /// <summary>
        /// Starts the stream.
        /// </summary>
        public void Play()
        {
            VlcPlayer.Play(new Media(Globals.CameraLibVLC, Crypto.Unprotect(ConnectionString), FromType.FromLocation));
        }

        /// <summary>
        /// Stops the stream.
        /// </summary>
        public void Stop()
        {
            VlcPlayer.Stop();
        }
    }
}
