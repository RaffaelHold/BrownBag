using Microsoft.AspNetCore.Mvc;

namespace GitExecuter.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        // GET api/git
        [HttpGet]
        public void Get(string command, string argument)
        {
            var executor = new GitExecutor();
            executor.Execute(command, argument);
        }
    }
}
