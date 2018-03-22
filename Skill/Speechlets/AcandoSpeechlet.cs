using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using Skill.Statics;
using System;
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
                    return BuildAudioResponse("https://shop.it-hold.de/whoweare.mp3");
                case AcandoIntents.ImDone:
                    return BuildAudioResponse("https://shop.it-hold.de/imdone.mp3");
                case AcandoIntents.GetStatusHenning:
                    return BuildSpeechletResponse("Okay", "Hans Henning Hoffmann is in the office today but he is currently in a meeting with the title Alexa - git push. He will be avaliable at 1pm", true);
                case AcandoIntents.GetStatusProkscha:
                    return BuildSpeechletResponse("Okay", "Waldemar Prokscha is not in the office today. His current status is offline 40 days", true);
                case AcandoIntents.GetStatusKnuth:
                    return BuildSpeechletResponse("Okay", "Sabine Knuth is in the office today. Her current status is online", true);
                default:
                    throw new SpeechletException("Invalid Intent");
            }
        }

        private SpeechletResponse BuildAudioResponse(string uri)
        {
            // Create the plain text output.
            SsmlOutputSpeech speech = new SsmlOutputSpeech
            {
                Ssml = $"<speak><audio src=\"{uri}\" /></speak>"
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