using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace spiral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Matrix rotateMatrix = Matrix.Identity;

        public MainWindow()
        {
            InitializeComponent();
            rotateMatrix.Rotate(90);
        }

        private void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Choosing start settings
            int startLength = 500;
            int gapWidth = 25;

            //Choosing between recursive and loop
            //DrawSpiralWithLoop(startLength, gapWidth);
            DrawSpiralRecursively(new Vector(0, 0), new Vector(1, 0), 3, startLength, gapWidth);
        }

        private void DrawSpiralWithLoop(int startLength, int gapWidth)
        {
            //Initialize variables
            Vector position = new Vector(0, 0);
            Vector direction = new Vector(1, 0);
            int countToChangeLength = 3;
            int currentLength = startLength;

            //If currentLength was smaller than gapWidth, then the next line would not follow the gap
            while (currentLength >= gapWidth)
            {
                //Drawing line, moving position and rotating direction
                CreateLine(position, position + direction * currentLength);
                position += direction * currentLength;
                direction = rotateMatrix.Transform(direction);

                //Check if length should be decreased
                countToChangeLength--;
                if (countToChangeLength == 0)
                {
                    currentLength -= gapWidth + 1;
                    countToChangeLength = 2;

                    //Drawing The last line
                    if (currentLength < gapWidth)
                        CreateLine(position, position + direction * currentLength);
                }
            }
        }

        private void DrawSpiralRecursively(Vector position, Vector direction, int countToChangeLength, int currentLength, int gapWidth)
        {
            //If currentLength was smaller than gapWidth, then the next line would not follow the gap
            if (currentLength < gapWidth)
                return;

            //Drawing line, moving position and rotating direction
            CreateLine(position, position + direction * currentLength);
            position += direction * currentLength;
            direction = rotateMatrix.Transform(direction);

            //Check if length should be decreased
            countToChangeLength--;
            if (countToChangeLength == 0)
            {
                currentLength -= gapWidth + 1;
                countToChangeLength = 2;

                //Drawing The last line
                if (currentLength < gapWidth)
                    CreateLine(position, position + direction * currentLength);
            }

            DrawSpiralRecursively(position, direction, countToChangeLength, currentLength, gapWidth);
        }

        //Function will add new line to the mainGrid
        private void CreateLine(Vector from, Vector to)
        {
            Line line = new Line();
            line.X1 = from.X + 0.5f;
            line.Y1 = from.Y + 0.5f;
            line.X2 = to.X + 0.5f;
            line.Y2 = to.Y + 0.5f;
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;
            mainGrid.Children.Add(line);
        }
    }
}
