using System.Windows.Controls;
using System.Windows;

namespace CameraViewer.Controls
{
    /// <summary>
    /// Interaction logic for CameraGrid.xaml
    /// </summary>
    public partial class CameraGrid : UserControl
    {
        public CameraGrid()
        {
            InitializeComponent();
            CamGrid.ShowGridLines = true;   // Add grid lines for debugging purposes.
        }

        /// <summary>
        /// Adds a camera view to the grid.
        /// </summary>
        /// <param name="camView"></param>
        public void AddCameraView(CameraView camView)
        {
            // Add the camera view to the grid.
            CamGrid.Children.Add(camView);

            // Check first if the amount spaces available are less than the amount of camera views.
            if ((CamGrid.RowDefinitions.Count * CamGrid.ColumnDefinitions.Count) < CamGrid.Children.Count)
            {
                // If there aren't enough spaces then add an equal amount of columns and rows where needed.
                if (CamGrid.ColumnDefinitions.Count <= CamGrid.RowDefinitions.Count)
                    CamGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                else
                    CamGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            RearangeCameraViews();
        }

        /// <summary>
        /// This will rearange every camera view in a left-to-right order in the existing row and column definitions.
        /// </summary>
        private void RearangeCameraViews()
        {
            int row = 0;
            int col = -1;

            foreach (UIElement view in CamGrid.Children)
            {
                col++;

                if ((col + 1) > CamGrid.ColumnDefinitions.Count)
                {
                    col = 0;
                    row++;
                }
                if ((row + 1) > CamGrid.RowDefinitions.Count)
                    break;

                view.SetValue(Grid.RowProperty, row);
                view.SetValue(Grid.ColumnProperty, col);
            }
        }

        /// <summary>
        /// Removes all camera views from the grid.
        /// <para>Note: This does not dispose the camera views.</para>
        /// </summary>
        public void ClearCameraViews()
        {
            CamGrid.Children.Clear();

            // Don't use this. This causes the cameras to be added on top of eachother.
            //CamGrid.RowDefinitions.Clear();
            //CamGrid.ColumnDefinitions.Clear();
        }
    }
}
