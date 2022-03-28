using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace ZanzarahBuild.Converters
{
    public class DialogueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();
            FlowDocument doc = new FlowDocument();

            TextRange tr;
            Queue<string> strings = new Queue<string>();
            if (str[0] == '{') strings.Enqueue("");
            while (true)
            {
                if (str == "") break;
                strings.Enqueue(str.Substring(0, str.IndexOf('{')));
                str = str.Substring(str.IndexOf('{') + 3);
                if (str == "") break;
                strings.Enqueue(str.Substring(0, str.IndexOf('}')));
                str = str.Substring(str.IndexOf('}') + 1);
            }
            while (true)
            {
                if (strings.Count == 0) break;
                tr = new TextRange(doc.ContentEnd, doc.ContentEnd) { Text = strings.Dequeue() };
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, (LinearGradientBrush)Application.Current.Resources["DialogueBrush"]);
                if (strings.Count == 0) break;
                tr = new TextRange(doc.ContentEnd, doc.ContentEnd) { Text = strings.Dequeue() };
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, (LinearGradientBrush)Application.Current.Resources["TextBrush"]);
            }
            return doc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            var db = (LinearGradientBrush)Application.Current.Resources["DialogueBrush"];
            var tb = (LinearGradientBrush)Application.Current.Resources["TextBrush"];
            FlowDocument doc = value as FlowDocument;
            foreach (Inline inline in (doc.Blocks.FirstBlock as Paragraph).Inlines)
            {
                var tr = new TextRange(inline.ContentStart, inline.ContentEnd);
                if (inline.Foreground == db) result += tr.Text;
                else if (inline.Foreground == tb) result += "{4*" + tr.Text + "}";
            }
            return result;
        }
    }
}
