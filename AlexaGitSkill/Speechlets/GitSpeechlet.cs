﻿using AlexaGitSkill.Statics;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlexaGitSkill.Speechlets
{
    public class GitSpeechlet : BaseSpeechlet
    {

        public override string WelcomeMessage => "Wilkommen zur Acando Service Demo mit Alexa. Frage mich nach dem Status deines Tickets oder schilder mir ein Problem, das du hast.";

        public override async Task<string> HandleIntentAsync(string intentName, Intent intent, Session session)
        {
            var result = String.Empty;
            var command = intentName;
            var argument = String.Empty;

            Slot slot;
            switch (intentName)
            {
                case GitIntentNames.Add:
                    result = "Ich habe die Änderungen gestaged";
                    break;
                case GitIntentNames.Commit:
                    intent.Slots.TryGetValue(GitSlotNames.CommitMessage, out slot);
                    argument = slot?.Value;
                    result = "Ich habe die Änderungen commited";
                    break;
                case GitIntentNames.Pull:

                    result = "Ich habe erfolgreich gepullt";
                    break;
                case GitIntentNames.Push:
                    intent.Slots.TryGetValue(GitSlotNames.RemoteBranch, out slot);
                    argument = slot?.Value;
                    result = "Ich habe die Änderungen erfolgreich gepusht";
                    break;
            }

            // TODO: get base URI value from config;
            var client = new HttpClient();
            await client.GetAsync($"ngrok.io/api/git?command={command}&argument={argument}");

            return result;
        }
    }
}