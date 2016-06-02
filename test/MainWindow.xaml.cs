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

namespace test
{
    //public class MyObject
    //{
    //    public string Data { get; set; }
    //    public int Digital { get; set; }
    //}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> data = new List<string>();
            for(int i = 0;i < 100;i++)
            {
                data.Add(i.ToString());
            }
            Random rand = new Random((int)DateTime.Now.Ticks);
            #region h
            //List<MyObject> items = new List<MyObject>();
            //for(int i = 0; i < data.Count; i++)
            //{
            //    MyObject obj = new MyObject();
            //    obj.Data = data[i];
            //    obj.Digital = rand.Next();
            //    items.Add(obj);
            //}
            #endregion
            var items = from val 
                        in data
                        select new
                        {
                            Data = val,
                            Digital = rand.Next()
                        };
            listview.ItemsSource = items;
        }
    }
}
