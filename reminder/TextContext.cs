using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VariousCool;

namespace reminder
{
    public class TextContext : INotifyPropertyChanged
    {
        private string _textbox_text = "";
        private string _tagbox_text = "";

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Tips> _tips = new ObservableCollection<VariousCool.Tips>();
        public ObservableCollection<Tips> Tips
        {
            get { return _tips; }
            set { _tips = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((nameof(Tips)))); }
        }


        public string TextboxText
        {
            get
            {
                return _textbox_text;
            }
            set
            {
                _textbox_text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextboxText)));
            }
        }

        public string TagboxText
        {
            get
            {
                return _tagbox_text;
            }
            set
            {
                _tagbox_text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagboxText)));
            }
        }

        public ICommand AddTagCommand { get; } = new ButtonCommand<TextContext>((txt) =>
        {
            string tip = txt.TextboxText;
            Tips myTips = new Tips();
            myTips.TipText = tip;
            myTips.Time = DateTime.Now;
            myTips.Uid = 0;
            DatabaseHelper.InsertTips(myTips);
            txt.RefreshData();
            txt.TextboxText = "";
        });

        public ICommand RemoveSingleItemCommand { get; } = new ButtonCommand<object[]>((cmds) =>
        {
            Tips tip = cmds[0] as Tips;
            TextContext ctx = cmds[1] as TextContext;
        });

        private void ChangeData()
        {
            if(TextboxText != "")
            {
                TagboxText = TextboxText;
                TextboxText = "";
            }
        }

        public TextContext()
        {
            RefreshData();
        }

        private void RefreshData()
        {
            var tips = DatabaseHelper.GetAllTips();
            Tips.Clear();
            foreach (var item in tips)
            {
                Tips.Add(item);
            }
        }
    }

    public class ButtonCommand<T> : ICommand
    {
        private Action<T> action = null;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();
            action?.Invoke((T)parameter);
        }

        public ButtonCommand(Action<T> act)
        {
            action = act;
        }
    }
}
