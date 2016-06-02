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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VariousCool;

namespace reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double dsk_top;
        private double dsk_left;
        private double dsk_width;
        private double dsk_height;
        private double windowWidth;
        private double windowHeight;

        public Duration Timespan { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                dsk_top = SystemParameters.WorkArea.Top;
                dsk_left = SystemParameters.WorkArea.Left;
                dsk_width = SystemParameters.WorkArea.Width;
                dsk_height = SystemParameters.WorkArea.Height;
                
                windowWidth = dsk_width * 0.2;
                windowHeight = dsk_height * 1.0;

                Left = dsk_left + (dsk_width - windowWidth);
                Top = dsk_top;
                Height = windowHeight;
                Width = windowWidth;

                MouseEnter += MainWindow_MouseEnter;
                MouseLeave += MainWindow_MouseLeave;

                inputbox.PreviewKeyDown += Inputbox_PreviewKeyDown;
            };

            this.DataContext = new TextContext();
        }

        private void Inputbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled == false)
            {
                if (e.Key == Key.Enter)
                {
                    (this.DataContext as TextContext).TextboxText = inputbox.Text;
                    add.Command.Execute(this.DataContext);
                    e.Handled = true;
                }
            }
        }

        private void MainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            SetWindowLeft(dsk_left + dsk_width - 5);
        }

        private void MainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            SetWindowLeft(dsk_left + (dsk_width - windowWidth));
        }

        private void SetWindowLeft(double target_to_left)
        {
            DoubleAnimation change_left = new DoubleAnimation()
            {
                From = Left,
                To = target_to_left,
                Duration = TimeSpan.FromMilliseconds(250.0)
            };
            Storyboard sb = new Storyboard();
            sb.Children.Add(change_left);
            Storyboard.SetTarget(change_left, this);
            Storyboard.SetTargetProperty(change_left, new PropertyPath("Left"));
            sb.Begin();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
