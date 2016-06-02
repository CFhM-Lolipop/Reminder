using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousCool
{
    public class Tips : Mvvm
    {
        private long _tipsid;
        public long TipsId
        {
            get { return _tipsid; }
            set { _tipsid = value; NotifyPropertyChanged(nameof(TipsId)); }
        }

        private long _uid;
        public long Uid
        {
            get { return _uid; }
            set { _uid = value; NotifyPropertyChanged(nameof(Uid)); }
        }

        private string _tiptext;
        public string TipText
        {
            get { return _tiptext; }
            set { _tiptext = value; NotifyPropertyChanged(nameof(TipText)); }
        }

        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; NotifyPropertyChanged(nameof(Time)); }
        }

        private bool _isread;
        public bool IsRead
        {
            get { return _isread; }
            set { _isread = value; NotifyPropertyChanged(nameof(IsRead)); }
        }

    }
}
