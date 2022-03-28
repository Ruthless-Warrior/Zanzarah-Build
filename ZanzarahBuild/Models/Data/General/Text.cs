using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public class Text : Entity
    {
        private string _content = "";
        private int _type;
        private string _mark = "";
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Mark
        {
            get { return _mark; }
            set
            {
                if (_mark != value)
                {
                    _mark = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
