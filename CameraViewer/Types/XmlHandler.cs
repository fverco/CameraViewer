using System;
using System.Collections.Generic;
using System.Xml;

namespace CameraViewer.Types
{
    internal class XmlHandler
    {
        /// <summary>
        /// The file name of the XML file that contains camera connection info.
        /// </summary>
        private const string _XmlCameraFileName = "cam.xml";

        /// <summary>
        /// Write camera info to an XML file.
        /// </summary>
        /// <param name="camera">The camera object.</param>
        public static void WriteCameraToFile(Camera camera)
        {
            WriteCameraToFile(camera.Name, camera.ConnectionString);
        }

        /// <summary>
        /// Write camera info to an XML file.
        /// <para>Note: An ArgumentException will be thrown if the camera already exists in the file.</para>
        /// </summary>
        /// <param name="cameraName">The camera's name.</param>
        /// <param name="connectionString">The connection string for the camera.</param>
        public static void WriteCameraToFile(string cameraName, string connectionString)
        {
            if (!CameraExistsInFile(cameraName))
            {
                var xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("cameras");
                xmlDoc.AppendChild(rootNode);

                XmlNode camNode = xmlDoc.CreateElement("camera");
                XmlAttribute attribute = xmlDoc.CreateAttribute("conn");
                attribute.Value = connectionString;
                camNode.Attributes.Append(attribute);
                camNode.InnerText = cameraName;
                rootNode.AppendChild(camNode);

                xmlDoc.Save(_XmlCameraFileName);
            }
            else
                throw new ArgumentException("Camera name already exists: " + cameraName);
        }

        /// <summary>
        /// Reads all the stored camera info from the XML file.
        /// </summary>
        /// <returns>A list of Camera types.</returns>
        public static List<Camera> ReadAllCamerasFromFile()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_XmlCameraFileName);
            var cameras = new List<Camera>();

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
                cameras.Add(new Camera(xmlNode.InnerText, xmlNode.Attributes["conn"].Value));

            return cameras;
        }

        /// <summary>
        /// Reads the connection string of a specific camera.
        /// </summary>
        /// <param name="cameraName">The name of the camera.</param>
        /// <returns>A string with the encrypted connection string.</returns>
        public static string? ReadCameraConnectionString(string cameraName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_XmlCameraFileName);

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
            {
                if (xmlNode.InnerText == cameraName)
                    return xmlNode.Attributes["conn"].Value;
            }

            return null;
        }

        /// <summary>
        /// Reads all of the camera names from the XML file.
        /// </summary>
        /// <returns>A list of strings with the names.</returns>
        public static List<string> ReadAllCameraNamesFromFile()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_XmlCameraFileName);
            var cameraNames = new List<string>();

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
                cameraNames.Add(xmlNode.InnerText);

            return cameraNames;
        }

        /// <summary>
        /// Checks if the provided camera name already exists in the file.
        /// </summary>
        /// <param name="cameraName">The name of the camera.</param>
        /// <returns>True or false.</returns>
        public static bool CameraExistsInFile(string cameraName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_XmlCameraFileName);

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
            {
                if (xmlNode.InnerText == cameraName)
                    return true;
            }

            return false;
        }
    }
}
