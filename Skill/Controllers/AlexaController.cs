using Skill.Speechlets;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Skill.Controllers
{
    public class AlexaController : ApiController
    {
        [Route("alexa/git")]
        [HttpPost]
        public async Task<HttpResponseMessage> Git()
        {
            var speechlet = new GitSpeechlet();
            return await speechlet.GetResponseAsync(Request);
        }

        [Route("alexa/powerpoint")]
        [HttpPost]
        public async Task<HttpResponseMessage> PowerPoint()
        {
            var speechlet = new PowerPointSpeechlet();
            return speechlet.GetResponse(Request);
        }

        [Route("alexa/acando")]
        [HttpPost]
        public async Task<HttpResponseMessage> Acando()
        {
            var speechlet = new AcandoSpeechlet();
            return speechlet.GetResponse(Request);
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