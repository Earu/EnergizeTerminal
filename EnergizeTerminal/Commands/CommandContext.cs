using Microsoft.AspNetCore.Http;

namespace EnergizeTerminal.Commands
{
    public class CommandContext
    {
        public CommandHandler Handler { get; }
        public Command        Command { get; }
        public HttpRequest    Request { get; }
        public string         Token   { get; }

        public CommandContext(CommandHandler handler,Command cmd,SentCommand scmd)
        {
            this.Handler = handler;
            this.Command = cmd;
            this.Request = scmd.AssociatedRequest;
            this.Token   = scmd.Token;
        }

        public string BadUsage { get => $"Correct usage is: {this.Command.Usage}"; }
    }
}
