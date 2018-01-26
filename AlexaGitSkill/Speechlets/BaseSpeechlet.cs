using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaGitSkill.Speechlets
{
    public abstract class BaseSpeechlet : SpeechletBase, ISpeechletWithContextAsync
    {
        public virtual string WelcomeMessage => "Willkommen";

        public abstract Task<string> HandleIntentAsync(string intentName, Intent intent, Session session);

        public async Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context)
        {
            //Log.Info("OnIntent requestId={0}, sessionId={1}", request.RequestId, session.SessionId);

            // Get intent from the request object.
            var intent = intentRequest.Intent;
            string intentName = intent?.Name;

            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            var result = await HandleIntentAsync(intentName, intent, session);

            if (string.IsNullOrEmpty(result))
            {
                throw new SpeechletException("Invalid Intent");
            }

            return BuildSpeechletResponse("", result, true);
        }

        public async Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session, Context context)
        {
            return await GetWelcomeResponseAsync(WelcomeMessage);
        }

        public Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session, Context context)
        {
            return Task.CompletedTask;
        }

        public Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session, Context context)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns a welcome message.
        /// </summary>
        /// <returns></returns>
        protected Task<SpeechletResponse> GetWelcomeResponseAsync(string message)
        {
            // Here we are setting shouldEndSession to false to not end the session and
            // prompt the user for input
            return Task.FromResult(BuildSpeechletResponse("Wilkommen", message, false));
        }

        protected SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession)
        {
            // Create the Simple card content.
            var card = new SimpleCard
            {
                Title = String.Format("SessionSpeechlet - {0}", title),
                Content = String.Format("SessionSpeechlet - {0}", output)
            };

            // Create the plain text output.
            var speech = new PlainTextOutputSpeech
            {
                Text = output
            };

            // Create the speechlet response.
            var response = new SpeechletResponse
            {
                ShouldEndSession = shouldEndSession,
                OutputSpeech = speech,
                Card = card
            };
            return response;
        }

    }
}
