using System.Windows.Controls;
using System.Windows;

namespace CameraViewer.Controls
{
    /// <summary>
    /// Interaction logic for CameraGrid.xaml
    /// </summary>
    public partial class CameraGrid : UserControl
    {
        /// <summary>
        /// The maximum amount of camera views allowed per row.
        /// </summary>
        private short _MaxRowSize = 2;

        /// <summary>
        /// The amount of camera views in the grid.
        /// </summary>
        private short _CameraViewCount = 0;

        public CameraGrid()
        {
            InitializeComponent();
            CamGrid.ShowGridLines = true;
        }

        /// <summary>
        /// Adds a camera view to the grid.
        /// </summary>
        /// <param name="camView"></param>
        public void AddCameraView(CameraView camView)
        {
            CamGrid.Children.Add(camView);

            // Create a column if there aren't enough columns.
            if (_CameraViewCount < _MaxRowSize)
                CamGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Get the amount of available space in the last row.
            short lastRowCamSpace = (short)(_CameraViewCount % _MaxRowSize);

            // If there isn't any space in the last row, create a new row.
            if (lastRowCamSpace == 0)
                CamGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // Place the camera view in the last row, in the first available spot from the left.
            camView.SetValue(Grid.RowProperty, CamGrid.RowDefinitions.Count - 1);
            camView.SetValue(Grid.ColumnProperty, (int)lastRowCamSpace);

            _CameraViewCount++;
        }

        /// <summary>
        /// Removes all camera views from the grid.
        /// <para>Note: This does not dispose the camera views.</para>
        /// </summary>
        public void ClearCameraViews()
        {
            CamGrid.Children.Clear();
            CamGrid.RowDefinitions.Clear();
            CamGrid.ColumnDefinitions.Clear();
            _CameraViewCount = 0;
        }
    }
}
