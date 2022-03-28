using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public class Dialogue : Entity
    {
        private string _message;
        private int _topicCode;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    string s = CheckDialogue(value);
                    if (_message != s)
                    {
                        _message = s;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public int TopicCode
        {
            get { return _topicCode; }
            set
            {
                if (_topicCode != value)
                {
                    _topicCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private string CheckDialogue(string source)
        {
            string result = "", temp, s;
            while (source != "")
            {
                if (source.Contains("{"))
                {
                    temp = source.Substring(0, source.IndexOf('{') + 1).Replace("}", "");
                    source = source.Substring(source.IndexOf('{') + 1);
                    if (!source.Contains("}")) source += "}";
                    s = source.Substring(0, source.IndexOf('}')).Replace("{", "");
                    if (s.Length <= 2 || s.Substring(0, 2) != "4*") s = "4*" + s;
                    temp += s + "}";
                    source = source.Substring(source.IndexOf('}') + 1);
                    result += temp;
                }
                else
                {
                    result += source.Replace("}", "");
                    source = "";
                }
            }
            result = result.Replace("{4*}", "").Replace("}{4*", "");
            return result;
        }
    }
}
