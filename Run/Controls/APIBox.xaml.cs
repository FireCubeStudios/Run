using CubeKit.UI.Icons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OpenAI.Managers;
using OpenAI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Run.Services;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using Run.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Run.Controls
{
    public sealed partial class APIBox : UserControl
    {
        public APIBox()
        {
            this.InitializeComponent();
            KeyBox.Password = new KeyService().GetKey();
        }

        private async void AddApi()
        {
            if (string.IsNullOrEmpty(KeyBox.Password))
            {
                Reject();
                return;
            }
            try
            {
                Ring.Visibility = Visibility.Visible;
                OpenAIService AI = new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = KeyBox.Password
                });
                var completionResult = await AI.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("Test")
                    },
                    Model = Models.ChatGpt3_5Turbo,
                    MaxTokens = 10,
                });

                if (completionResult.Successful)
                {
                    new KeyService().SetKey(KeyBox.Password);
                    SettingsService.HasKey = true;
                    ErrorBlock.Text = "";
                    Accept();
                }
                else
                {
                    ErrorBlock.Text = completionResult.Error.Message;
                    Reject();
                }
                Ring.Visibility = Visibility.Collapsed;
            }
            catch
            {
                Reject();
            }
        }

        private FluentSymbol PrivacyToIcon(bool? boolean) => (boolean ?? false) ? FluentSymbol.EyeShow20 : FluentSymbol.EyeHide20;

        private PasswordRevealMode PrivacyToPassword(bool? boolean) => (boolean ?? false) ? PasswordRevealMode.Visible : PasswordRevealMode.Hidden;

        private void ApiBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                AddApi();
        }

        private void Submit_Click(object sender, RoutedEventArgs e) => AddApi();

        private void Accept()
        {
            KeyBox.Foreground = GreenLinearGradientBrush;
            KeyBox.Focus(FocusState.Programmatic);
        }

        private void Reject()
        {
            KeyBox.Foreground = RedLinearGradientBrush;
            KeyBox.Focus(FocusState.Programmatic);
            PasswordLoadAnimation.Start();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            new KeyService().SetKey("");
            SettingsService.HasKey = false;
        }
    }
}
