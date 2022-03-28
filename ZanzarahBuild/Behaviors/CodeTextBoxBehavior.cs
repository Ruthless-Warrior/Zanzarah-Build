using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZanzarahBuild.Behaviors
{
    public class CodeTextBoxBehavior : BehaviorBase<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;

            CommandManager.AddPreviewExecutedHandler(AssociatedObject, OnPreviewExecuted);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;

            CommandManager.RemovePreviewExecutedHandler(AssociatedObject, OnPreviewExecuted);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var snd = sender as TextBox;
            int i = snd.CaretIndex;
            string txt = snd.Text;
            switch (e.Key)
            {
                case Key.Back: // Backspace
                    if (i > 0)
                    {
                        if (snd.SelectedText.Length > 0)
                        {
                            snd.Text = txt.Substring(0, i) + new string('0', snd.SelectedText.Length) + txt.Substring(i + snd.SelectedText.Length);
                            snd.CaretIndex = i;
                        }
                        else
                        {
                            snd.Text = txt.Substring(0, i - 1) + "0" + txt.Substring(i);
                            snd.CaretIndex = i - 1;
                        }
                    }
                    e.Handled = true;
                    break;
                case Key.Delete:
                    if (i < 8)
                    {
                        if (snd.SelectedText.Length > 0)
                        {
                            snd.Text = txt.Substring(0, i) + new string('0', snd.SelectedText.Length) + txt.Substring(i + snd.SelectedText.Length);
                            snd.CaretIndex = i + snd.SelectedText.Length;
                        }
                        else
                        {
                            snd.Text = txt.Substring(0, i) + txt.Substring(i + 1) + "0";
                            snd.CaretIndex = i;
                        }
                    }
                    e.Handled = true;
                    break;
                case Key.Space:
                case Key.Tab:
                    e.Handled = true;
                    break;
            }
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var snd = sender as TextBox;
            e.Handled = !int.TryParse(e.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _) ||
                string.IsNullOrWhiteSpace(e.Text);
            if (e.Handled) return;
            int i = snd.CaretIndex;
            string txt = snd.Text;
            if (i == 8) return;
            snd.Text = txt.Substring(0, i) + e.Text.ToUpper() + txt.Substring(i + 1);
            snd.CaretIndex = i + 1;
            e.Handled = true;
        }

        private void OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
