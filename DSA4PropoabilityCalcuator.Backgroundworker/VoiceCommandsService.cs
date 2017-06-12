using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace DSA4PropoabilityCalcuator.Backgroundworker
{
    public sealed class VoiceCommandsService : IBackgroundTask
    {
        private BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //Take a service deferral so the service isn&#39;t terminated.
            this.serviceDeferral = taskInstance.GetDeferral();

            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails =
              taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null &&
            triggerDetails.Name == "VoiceCommandsService")
        {
                try
                {
                    voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

                    voiceServiceConnection.VoiceCommandCompleted += VoiceCommandCompleted;

                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                    switch (voiceCommand.CommandName)
                    {
                        case "calculatePropablityDSA":
                            {
                                string e1 = voiceCommand.Properties["eigenschaft1"][0];
                                string e2 = voiceCommand.Properties["eigenschaft2"][0];
                                string e3 = voiceCommand.Properties["eigenschaft3"][0];
                                string tawString = voiceCommand.Properties["taw"][0];
                                var voiceParameter = new DSAVoiceCommand(e1, e2, e3);
                                var calculator = new DSAPropabilityCaluator(voiceParameter.Eigentschaft1, voiceParameter.Eigentschaft2, voiceParameter.Eigentschaft3);
                                int taw;
                                if(!int.TryParse(tawString, out taw))
                                {
                                    taw = 5;
                                }
                                var probability = calculator.CalcualteDicePropability(taw);
                                SendCompletionMessageForDestination(probability, voiceParameter);
                                break;
                            }

                        // As a last resort, launch the app in the foreground.
                        default:
                            LaunchAppInForeground();
                            break;
                    }
                }
                finally
                {
                    if (this.serviceDeferral != null)
                    {
                        // Complete the service deferral.
                        this.serviceDeferral.Complete();
                    }
                }
            }
        }

        private void VoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                // Insert your code here.
                // Complete the service deferral.
                this.serviceDeferral.Complete();
            }
        }

        private async void SendCompletionMessageForDestination(double proablity, DSAVoiceCommand voiceCommand)
        {
            // Take action and determine when the next trip to destination
            // Insert code here.

            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.

            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = $"Die Chance auf erfolg ist: {proablity:P2}";
            userMessage.SpokenMessage = $"Die Chance auf erfolg ist: {proablity:P2}";           

            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.
            var response =
              VoiceCommandResponse.CreateResponse(userMessage);

            // Cortana will present a “Go to app_name” link that the user 
            // can tap to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument = $"{voiceCommand.Eigentschaft1};{voiceCommand.Eigentschaft2};{voiceCommand.Eigentschaft3}"; 

            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await voiceServiceConnection.ReportSuccessAsync(response);
        }

        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Starte DSA Wahrscheinlichkeit";

            var response = VoiceCommandResponse.CreateResponse(userMessage);          

            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }

        /// <summary>
        /// When the background task is cancelled, clean up/cancel any ongoing long-running operations.
        /// This cancellation notice may not be due to Cortana directly. The voice command connection will
        /// typically already be destroyed by this point and should not be expected to be active.
        /// </summary>
        /// <param name="sender">This background task instance</param>
        /// <param name="reason">Contains an enumeration with the reason for task cancellation</param>
        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Task cancelled, clean up");
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }
    }
}
