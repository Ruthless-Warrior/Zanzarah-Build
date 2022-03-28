using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZanzarahBuild.Behaviors
{
    public class NumberTextBoxBehavior : BehaviorBase<TextBox>
    {
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumberTextBoxBehavior), new PropertyMetadata(0));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumberTextBoxBehavior), new PropertyMetadata(0));


        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.TextChanged += OnTextChanged;

            CommandManager.AddPreviewExecutedHandler(AssociatedObject, OnPreviewExecuted);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.TextChanged -= OnTextChanged;

            CommandManager.RemovePreviewExecutedHandler(AssociatedObject, OnPreviewExecuted);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var snd = sender as TextBox;
            if (int.TryParse(e.Text, out _))
            {
                if (snd.Text == "0" && snd.CaretIndex == 1)
                {
                    snd.Text = e.Text;
                    snd.CaretIndex = 1;
                    e.Handled = true;
                }
                else return;
            }
            else if (e.Text == "-")
            {
                if (snd.Text.Contains("-") || snd.CaretIndex > 0)
                    e.Handled = true;
                else return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var snd = sender as TextBox;
            if (string.IsNullOrEmpty(snd.Text))
            {
                snd.Text = "0";
                snd.CaretIndex = 1;
                e.Handled = true;
                return;
            }
            if (snd.Text == "-")
            {
                return;
            }
            int i = Convert.ToInt32(snd.Text);
            if (MaxValue < MinValue)
            {
                MaxValue = MinValue = 0;
                snd.Text = "0";
                snd.CaretIndex = 1;
                e.Handled = true;
            }
            else if (MinValue == MaxValue)
            {
                snd.Text = MaxValue.ToString();
                e.Handled = true;
            }
            else if (i > MaxValue)
            {
                snd.Text = MaxValue.ToString();
                e.Handled = true;
            }
            else if (i < MinValue)
            {
                snd.Text = MinValue.ToString();
                e.Handled = true;
            }
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
