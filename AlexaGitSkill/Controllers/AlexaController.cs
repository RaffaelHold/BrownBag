using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlexaGitSkill.Speechlets;
using Microsoft.AspNetCore.Mvc;

namespace AlexaGitSkill.Controllers
{
    [Route("api/[controller]")]
    public class AlexaController : Controller
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Service()
        {
            var speechlet = new GitSpeechlet();
            return await speechlet.GetResponseAsync(Request);
        }
    }
}
