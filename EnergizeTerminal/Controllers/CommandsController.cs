using System.Threading.Tasks;
using EnergizeTerminal.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EnergizeTerminal.Controllers
{
    [Route("commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandHandler _CommandHandler;

        public CommandsController() => this._CommandHandler = CommandHandler.Instance;

        [HttpPost]
        public async Task<CommandResult> PostAsync([FromBody]SentCommand scmd)
        {
            scmd.AssociatedRequest = Request;
            return await this._CommandHandler.TryRunCommandAsync(scmd);
        }
    }
}