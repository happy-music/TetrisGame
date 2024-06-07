using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TetrisGame.CustomControl
{
    internal class TetrisControl : UserControl
    {
        private Grid _grid;
        private Border[,]? borders;
        public int[,] Datas
        {
            get { return (int[,])GetValue(DatasProperty); }
            set { SetValue(DatasProperty, value); }
        }

        public static readonly DependencyProperty DatasProperty =
            DependencyProperty.Register("Datas", typeof(int[,]), typeof(TetrisControl), new FrameworkPropertyMetadata(DatasChangedCallbcak));

        private static void DatasChangedCallbcak(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TetrisControl control)
            {
                control.Reload();
            }
        }

        public void Reload()
        {
            if (Datas is not null)
            {
                Content = null;
                _grid = new Grid();
                AddChild(_grid);
                borders = new Border[Datas.GetLength(0), Datas.GetLength(1)];
                for (int i = 0; i < Datas.GetLength(0); i++)
                {
                    _grid.RowDefinitions.Add(new RowDefinition());
                    for (int j = 0; j < Datas.GetLength(1); j++)
                    {
                        if (i == 0)
                        {
                            _grid.ColumnDefinitions.Add(new ColumnDefinition());
                        }
                        var border = new Border();
                        border.SetValue(Grid.RowProperty, i);
                        border.SetValue(Grid.ColumnProperty, j);
                        border.Background = GetBrushes(Datas[i, j]);
                        border.BorderThickness = new Thickness(1);
                        border.BorderBrush = Brushes.White;

                        borders[i, j] = border;
                        _grid.Children.Add(border);
                    }
                }
            }
        }
        public void EliminateAnimation(List<int> ints)
        {
            foreach (var item in ints)
            {
                for (int i = 0; i < Datas.GetLength(1); i++)
                {
                    if (borders != null && borders.GetLength(0) > 0 && borders.GetLength(1) > i && borders[0, i] != null)
                    {
                        var brush = borders[item, i].Background as SolidColorBrush;
                        if (brush != null)
                        {
                            var animationBrush = new SolidColorBrush(brush.Color);
                            var animation = new ColorAnimation(Colors.White, Colors.DarkRed, TimeSpan.FromSeconds(1));
                            animation.RepeatBehavior = new RepeatBehavior(3);
                            animationBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                            borders[item, i].Background = animationBrush;
                        }
                    }
                }
            }
        }

        private SolidColorBrush GetBrushes(int number)
        {
            switch (number)
            {
                case 0: return Brushes.LightCyan;
                case 1: return Brushes.Blue;
                case 2: return Brushes.Red;
                case 3: return Brushes.Green;
                case 4: return Brushes.Orange;
                case 5: return Brushes.Purple;
                default: return Brushes.LightYellow;
            }
        }
    }
}
