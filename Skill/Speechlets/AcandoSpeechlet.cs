using AlexaSkillsKit.Interfaces.Dialog.Directives;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using Skill.Statics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skill.Speechlets
{
    public class AcandoSpeechlet : BaseSpeechlet
    {
        public override async Task<SpeechletResponse> HandleIntentAsync(IntentRequest intentRequest, Session session)
        {
            var result = String.Empty;
            var intent = intentRequest?.Intent;
            var command = intent.Name;
            var argument = String.Empty;

            switch (intent.Name)
            {
                case AcandoIntents.WhoAreWe:
                    return BuildWhoWeAre();
                default:
                    throw new SpeechletException("Invalid Intent");
            }
        }

        private SpeechletResponse BuildWhoWeAre()
        {
            // Create the plain text output.
            SsmlOutputSpeech speech = new SsmlOutputSpeech
            {
                Ssml = "<speak><audio src=\"https://shop.it-hold.de/whoweare.mp3\" /></speak>"
            };

            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse
            {
                ShouldEndSession = true,
                OutputSpeech = speech
            };

            return response;
        }
    }
}