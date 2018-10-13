using System;
using EnergizeTerminal.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergizeTerminal.Commands.Modules
{
    [CommandModule]
    public class BaseCommands
    {
        [LoggedInCommand]
        [Command("hello-world", "Says hello world")]
        public async Task<string> HelloWorld(CommandContext ctx, params string[] args) => "Hello World!";

        [Command("help","Displays this text","help [command]",1)]
        public async Task<string> Help(CommandContext ctx, params string[] args)
        {
            Dictionary<string,Command> cmds = ctx.Handler.Commands;
            string cmd = args[0];
            if (cmds.ContainsKey(cmd))
            {
                Command cmdobj = cmds[cmd];
                return $"{cmdobj.Help}\nUsage: {cmdobj.Usage}";
            }
            else
                return $"No documentation could be found for \"{cmd}\".";
        }

        [Command("cmds","Displays the list of available commands")]
        public async Task<string> Commands(CommandContext ctx,params string[] args)
        {
            string res = string.Empty;
            uint iter = 0;
            foreach (KeyValuePair<string, Command> kvcmd in ctx.Handler.Commands)
            {
                Command cmd = kvcmd.Value;
                res += $"{(iter > 0 ? "\n\n" : "")}- {cmd.Name}\n\t{cmd.Help}\n\tUsage: {cmd.Usage}";
                iter++;
            }

            return res;
        }

        [Command("login","Tries to login using a password","login [password]",1)]
        public async Task<string> Login(CommandContext ctx,params string[] args)
        {
            if (args[0] == Config.Password)
                return AuthService.GenerateToken();
            else
                throw new Exception("Wrong credentials.");
        }

        [LoggedInCommand]
        [Command("logout", "Logs out of the current session")]
        public async Task<string> Logout(CommandContext ctx, params string[] args)
        {
            AuthService.EndToken(ctx.Token);
            return "Succesfully logged out.";
        }
    }
}
