using AlexaSkillsKit.Speechlet;
using Common.Services;
using Common.Statics;
using System.Threading.Tasks;
using AlexaSkillsKit.Interfaces.Dialog.Directives;
using System.Collections.Generic;
using Skill.Statics;

namespace Skill.Speechlets
{
    public class PowerPointSpeechlet : BaseSpeechlet
    {
        public override async Task<SpeechletResponse> HandleIntentAsync(IntentRequest intentRequest, Session session)
        {
            if (intentRequest.DialogState == IntentRequest.DialogStateEnum.STARTED || intentRequest.DialogState == IntentRequest.DialogStateEnum.IN_PROGRESS)
            {
                return new SpeechletResponse
                {
                    Directives = new List<Directive>
                    {
                        new DialogDelegateDirective()
                        {
                            UpdatedIntent = intentRequest?.Intent
                        }
                    }
                };
            }

            string intentName = intentRequest?.Intent?.Name;
            string message;

            switch (intentName)
            {
                case PowerPointIntents.GoToNextPage:
                    message = "{RIGHT}";
                    break;
                case PowerPointIntents.GoToPreviousPage:
                    message = "{LEFT}";
                    break;
                case PowerPointIntents.GoToIntro:
                    message = "2+{ENTER}";
                    break;
                case PowerPointIntents.GoToSSML:
                    message = "8+{ENTER}";
                    break;

                default:
                    throw new SpeechletException("Invalid Intent");
            }

            var serviceBus = new ServiceBus(ServiceBusQueues.PowerPoint);
            await serviceBus.SendMessage(message);

            return BuildSpeechletResponse("Okay", "Okay", true);
        }
    }
}