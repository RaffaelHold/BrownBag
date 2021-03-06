﻿using Skill.Statics;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Common;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlexaSkillsKit.Interfaces.Dialog.Directives;
using Common.Statics;
using Common.Services;
using Newtonsoft.Json;

namespace Skill.Speechlets
{
    public class GitSpeechlet : BaseSpeechlet
    {
        public override string WelcomeMessage => "Git started.";

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

            var result = String.Empty;
            var intent = intentRequest?.Intent;
            var command = intent.Name;
            var argument = String.Empty;

            switch (intent.Name)
            {
                case GitIntents.Status:
                    return BuildSpeechletResponse("Okay", "The file changes dot txt has been modified.", true);
                case GitIntents.Add:
                    result = "All changes have been staged.";
                    break;
                case GitIntents.Commit:
                    intent.Slots.TryGetValue(GitSlots.CommitMessage, out Slot slot);
                    argument = slot?.Value;
                    result = "I committed your changes.";
                    break;
                case GitIntents.Pull:

                    result = "Upstream branches have been pulled.";
                    break;
                case GitIntents.Push:
                    intent.Slots.TryGetValue(GitSlots.RemoteBranch, out Slot branchSlot);
                    intent.Slots.TryGetValue(GitSlots.RemoteRepository, out Slot repositorySlot);
                    argument = repositorySlot?.Value + " " + branchSlot?.Value;
                    result = $"I successfully pushed your changes to {repositorySlot?.Value} {branchSlot?.Value}.";
                    break;
            }

            var commandDto = new Command
            {
                CommandText = command.ToLower(),
                Argument = argument
            };

            var serviceBus = new ServiceBus(ServiceBusQueues.GitQueue);
            await serviceBus.SendMessage(JsonConvert.SerializeObject(commandDto));

            return BuildSpeechletResponse("Okay", result, true);
        }
    }
}