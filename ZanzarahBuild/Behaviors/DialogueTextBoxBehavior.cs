using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Behaviors
{
    public class DialogueTextBoxBehavior : BehaviorBase<RichTextBox>
    {
        private static string changedString;

        public string DialogueMessage
        {
            get { return (string)GetValue(DialogueMessageProperty); }
            set { SetValue(DialogueMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DialogueMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogueMessageProperty =
            DependencyProperty.Register("DialogueMessage", typeof(string), typeof(DialogueTextBoxBehavior), new PropertyMetadata("", new PropertyChangedCallback(OnDialogueMessageChanged)));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.TextChanged += OnTextChanged;
            DataObject.AddPastingHandler(AssociatedObject, OnPasting);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.TextChanged -= OnTextChanged;
            DataObject.RemovePastingHandler(AssociatedObject, OnPasting);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text == "{" || e.Text == "}";
        }

        private static void OnDialogueMessageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue || changedString == e.NewValue as string) return;
            RichTextBox rtb = (sender as DialogueTextBoxBehavior).AssociatedObject;
            string str = e.NewValue as string;
            FlowDocument doc = new FlowDocument();

            TextRange tr;
            Queue<string> strings = new Queue<string>();
            if (str == "" || str[0] == '{') strings.Enqueue("");
            while (true)
            {
                if (str == "") break;
                if (!str.Contains("{"))
                {
                    strings.Enqueue(str);
                    str = "";
                }
                else
                {
                    strings.Enqueue(str.Substring(0, str.IndexOf('{')));
                    str = str.Substring(str.IndexOf('{') + 3);
                }
                if (str == "") break;
                strings.Enqueue(str.Substring(0, str.IndexOf('}')));
                str = str.Substring(str.IndexOf('}') + 1);
            }
            LinearGradientBrush db = rtb.TryFindResource("DialogueBrush") as LinearGradientBrush;
            LinearGradientBrush tb = rtb.TryFindResource("TextBrush") as LinearGradientBrush;
            while (true)
            {
                if (strings.Count == 0) break;
                tr = new TextRange(doc.ContentEnd, doc.ContentEnd) { Text = strings.Dequeue() };
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, db);
                if (strings.Count == 0) break;
                tr = new TextRange(doc.ContentEnd, doc.ContentEnd) { Text = strings.Dequeue() };
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, tb);
            }
            try
            {
                (sender as DialogueTextBoxBehavior).AssociatedObject.Document = doc;
            }
            catch (InvalidOperationException)  
            {
                //    Managed Debugging Assistant 'FatalExecutionEngineError:
                /*
                 *    The runtime has encountered a fatal error. The address of the error was at 0x6c2ca7d0, on thread 0x14d0.
                 *    The error code is 0x80131623. This error may be a bug in the CLR or in the unsafe or non-verifiable
                 *    portions of user code. Common sources of this bug include user marshaling errors for COM-interop or
                 *    PInvoke, which may corrupt the stack.
                 */

                //    Cannot set the Document property inside the scope of DeclareChangeBlock or BeginChange/EndChange calls.
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string result = "";
            var db = AssociatedObject.TryFindResource("DialogueBrush");
            var tb = AssociatedObject.TryFindResource("TextBrush");
            FlowDocument doc = AssociatedObject.Document;
            if (doc.Blocks.FirstBlock != null) foreach (Inline inline in (doc.Blocks.FirstBlock as Paragraph).Inlines)
            {
                var tr = new TextRange(inline.ContentStart, inline.ContentEnd);
                if (inline.Foreground == db) result += tr.Text;
                else if (inline.Foreground == tb) result += "{4*" + tr.Text + "}";
            }
            changedString = result;
            if (DialogueMessage != result) DialogueMessage = result;
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var rtb = sender as RichTextBox;
            string text = e.DataObject.GetData("Text").ToString()
                .Replace("{", "")
                .Replace("}", "")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "");
            //int caretPosition = new TextRange(rtb.Document.ContentStart, rtb.CaretPosition).Text.Length;
            rtb.CaretPosition.InsertTextInRun(text);
            e.CancelCommand();
        }
    }
}
