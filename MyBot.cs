// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
//using System.Random;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service. Transient lifetime services are created
    /// each time they're requested. Objects that are expensive to construct, or have a lifetime
    /// beyond a single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class MyBot : IBot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyBot"/> class.
        /// </summary>
        public MyBot()
        {
        }

        /// <summary>
        /// Every conversation turn calls this method.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>

        private string tranform(string str){

            int n = str.Length;

            System.Random r = new System.Random();

            int t = r.Next(3);

            if (t == 0){
                char[] l = str.ToCharArray();

                for (int i = 0; i < n; i++)
                    if (i%2 == 0) l[i] = char.ToUpper(l[i]);

                str = new string(l);
                return str;
            }

            else if (t == 1){

                string[] emojis = {" ^~ ", " :3 ", " xD ", " rawr ", " ¬¬ ", " ( ͡° ͜ʖ ͡°) ", " XD "
                ," :p ", " :X ", " :B ", " <3 ", " ?.? "};

                int em = emojis.Length;

                string[] splited = str.Split(' ');

                string ans = splited[0];
                int ns = splited.Length;

                for (int i = 1; i < ns; i++){
                    System.Random moji = new System.Random();
                    int pos = moji.Next(em);
                    ans = ans + emojis[pos] + splited[i];
                }

                return ans;
            }

            else if(t == 2){
                char[] l = str.ToCharArray();
                for(int i = 0; i < n; i++){
                    switch(char.ToLower(l[i])){
                        case 'a':
                            l[i] = '4';
                            break;
                        case 'i':
                            l[i] = '1';
                            break;
                        case 'e':
                            l[i] = '3';
                            break;
                        case 's':
                            l[i] = '$';
                            break;
                        case 't':
                            l[i] = '7';
                            break;
                        case 'o':
                            l[i] = '0';
                            break;
                        default:
                            break;
                        }
                }
                str = new string(l);
                return str;
            }

            return null;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                string str = turnContext.Activity.Text;
                str = tranform(str);

                // Echo back to the user whatever they typed.
                var responseMessage = $"{str}\n";
                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
