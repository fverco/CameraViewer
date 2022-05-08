using System.Windows.Controls;
using System.Windows.Input;

namespace CameraViewer.Controls
{
    /// <summary>
    /// Interaction logic for CameraView.xaml
    /// </summary>
    public partial class CameraView : UserControl
    {
        /// <summary>
        /// The context menu for the camera view.
        /// </summary>
        ContextMenu _CameraContextMenu;

        public CameraView()
        {
            InitializeComponent();

            _CameraContextMenu = new ContextMenu();
            _CameraContextMenu.Items.Add(new MenuItem()
            {
                Header = "Remove Camera"
            });

            this.ContextMenu = _CameraContextMenu;
        }

        /// <summary>
        /// Opens the context menu of the camera view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightMouseClicked(object sender, MouseButtonEventArgs e)
        {
            this.ContextMenu.IsOpen = true;
        }
    }
}
