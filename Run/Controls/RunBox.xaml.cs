using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.NetworkOperators;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Run.Controls
{
    public sealed partial class RunBox : UserControl
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
                   DependencyProperty.Register("Text", typeof(string), typeof(RunBox), null);

        public RunBox()
        {
            this.InitializeComponent();
        }

        private void Run_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (string.IsNullOrEmpty(Run.Text))
                     Reject();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) => Run.Text = "";

        private void Run_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TypingAnimation.GetCurrentState() != ClockState.Active)
            {
                Run.Foreground = ShineBrush;
                TypingAnimation.Begin();
            }
        }

        public void Reject()
        {
            Run.Foreground = RedLinearGradientBrush;
            Run.Focus(FocusState.Programmatic);
            RejectAnimation.Start();
        }

        private void Run_LostFocus(object sender, RoutedEventArgs e)
        {
            TypingAnimation.Stop();
            Run.Foreground = AccentLinearGradientBrush;
        }

        private void Run_GotFocus(object sender, RoutedEventArgs e)
        {
            TypingAnimation.Stop();
            if(Run.Foreground != RedLinearGradientBrush) // Don't switch if red
                Run.Foreground = AccentLinearGradientBrush;
        }
    }
}
