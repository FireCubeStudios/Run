using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using Run.Interfaces;
using Run.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorCode.Compilation.Languages;
using Microsoft.UI.Xaml.Documents;
using static ABI.System.Windows.Input.ICommand_Delegates;
using static System.Net.Mime.MediaTypeNames;
using Windows.System;
namespace Run.Helpers
{
    public class GPTRunner : IAdminCommandRunner
    {
        private const string Instruction = "You power an AI enhanced Windows Run app where the user can launch stuff using natural language. You will recieve a prompt which needs to be responded with one cmd command which you are allowed to execute. Only respond with the cmd command and no additional text. If the command is unattainable or dangerous then respond with a single '.' dot so that my app can parse it. Additionally if the User Profile Name is needed substitute it with {user}. Finally if admin privileges are required then prepend '{A}' to the message.";
        private OpenAIService AI;
        public async Task<bool> RunAsync(string command, bool isAdmin)
        {
            try
            {
                AI = new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = new KeyService().GetKey()
                });
                var completionResult = await AI.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem(Instruction),
                        ChatMessage.FromUser(command)
                    },
                    Model = Models.ChatGpt3_5Turbo,
                    MaxTokens = 100,
                });

                if (completionResult.Successful)
                {
                    string Command = completionResult.Choices.First().Message.Content.Replace("{user}", Environment.GetEnvironmentVariable("USERPROFILE"));
                    if (Command == ".")
                        return false;
                    else if (Command.StartsWith("{A}") || isAdmin)
                        await CmdRunner.HiddenRunAsync(Command.Replace("{A}", ""), true);
                    else
                        await CmdRunner.HiddenRunAsync(Command, isAdmin);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
