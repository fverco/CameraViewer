using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System.Windows;
using System;

namespace CameraViewer.Types
{
    /// <summary>
    /// A class for setting up cameras to view in the window.
    /// <para>Note: When done using a Camera object you must call the VlcPlayer's dispose method as well as the VidView's dispose method to avoid memory leaks.</para>
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
        /// <para>Note: When done using this player you must call its dispose method.</para>
        /// </summary>
        public MediaPlayer VlcPlayer { get; }

        /// <summary>
        /// The view used to display the stream.
        /// <para>Note: When done using this video view you must call its dispose method.</para>
        /// </summary>
        public VideoView VidView { get; }

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
        /// Starts the stream.
        /// </summary>
        /// <returns>True if the stream succeeds.</returns>
        public bool Play()
        {
            return VlcPlayer.Play(new Media(Globals.CameraLibVLC, Crypto.Unprotect(ConnectionString), FromType.FromLocation));                
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
