using LibVLCSharp.Shared;

namespace CameraViewer.Types
{
    /// <summary>
    /// A class for accessing global variables in the application.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// The reference to the VLC library. This is needed for the VLC classes to work properly.
        /// <para>Important: There must only be one instance of this object at all times.</para>
        /// </summary>
        public static readonly LibVLC CameraLibVLC = new("--reset-plugins-cache");
    }
}
