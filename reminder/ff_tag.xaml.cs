using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VariousCool;

namespace reminder
{
    /// <summary>
    /// Interaction logic for tag.xaml
    /// </summary>
    public partial class ff_tag : UserControl
    {
        public static SolidColorBrush TransparentBrush { get; } = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        public SolidColorBrush TipBackground
        {
            get { return (SolidColorBrush)GetValue(TipBackgroundProperty); }
            set { SetValue(TipBackgroundProperty, value); }
        }
        public static readonly DependencyProperty TipBackgroundProperty = DependencyProperty.Register("TipBackground", typeof(SolidColorBrush), typeof(ff_tag), new PropertyMetadata(TransparentBrush, new PropertyChangedCallback(TipBackgroundPropertyChanged)));
        private static void TipBackgroundPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ff_tag sender = obj as ff_tag;
            sender.Dispatcher.BeginInvoke(new Action(() =>
            {
                sender.gbox.Background = sender.TipBackground;
            }));
        }


        public Tips BindedTip
        {
            get { return (Tips)GetValue(BindedTipProperty); }
            set { SetValue(BindedTipProperty, value); }
        }
        public static readonly DependencyProperty BindedTipProperty = DependencyProperty.Register("BindedTip", typeof(Tips), typeof(ff_tag), new PropertyMetadata(null, new PropertyChangedCallback(BindedTipPropertyChanged)));
        private static void BindedTipPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ff_tag sender = obj as ff_tag;
            Tips tip = sender.BindedTip;
            if(tip == null)
            {

                sender.DataContext = null;
            }
            else
            {
                sender.DataContext = tip;
            }
        }

        //private TagSet texts = null;
        public ff_tag()
        {
            InitializeComponent();
        }

        
    }

    //public class TagSet : INotifyPropertyChanged
    //{
    //    private string _header_text;
    //    private string _tagbox_text;

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public string HeaderText
    //    {
    //        get
    //        {
    //            return _header_text;
    //        }

    //        set
    //        {
    //            _header_text = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderText)));
    //        }
    //    }

    //    public string TagboxText
    //    {
    //        get
    //        {
    //            return _tagbox_text;
    //        }
    //        set
    //        {
    //            _tagbox_text = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagboxText)));
    //        }
    //    }
    //}
}
