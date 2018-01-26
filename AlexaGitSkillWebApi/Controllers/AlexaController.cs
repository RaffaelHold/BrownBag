using AlexaGitSkill.Speechlets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AlexaGitSkillWebApi.Controllers
{
    public class AlexaController : ApiController
    {
        [Route("alexa/git")]
        [HttpPost]
        public async Task<HttpResponseMessage> Service()
        {
            var speechlet = new GitSpeechlet();
            return await speechlet.GetResponseAsync(Request);
        }

        [Route("alexa/test")]
        [HttpGet]
        public HttpResponseMessage Debug()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("Success")
            };
        }
    }
}