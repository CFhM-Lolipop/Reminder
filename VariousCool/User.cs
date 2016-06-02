using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousCool
{
    public class User : Mvvm
    {
        private long _uid;
        public long Uid
        {
            get { return _uid; }
            internal set { _uid = value; NotifyPropertyChanged(nameof(Uid)); }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged(nameof(UserName)); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private string _pwd;
        public string Password
        {
            get { return _pwd; }
            set { _pwd = value; NotifyPropertyChanged(nameof(Password)); }
        }
        private string _icon_address;
        public string IconAddress
        {
            get { return _icon_address; }
            set { _icon_address = value; NotifyPropertyChanged(nameof(IconAddress)); }
        }


    }
}
