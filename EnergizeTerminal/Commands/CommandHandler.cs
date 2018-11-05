using EnergizeTerminal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EnergizeTerminal.Commands
{
    public class CommandHandler
    {
        private static CommandHandler _Instance = null;

        public CommandHandler()
        {
            this.Commands = new Dictionary<string, Command>();
            this.LoadCommands();
        }

        public Dictionary<string, Command> Commands { get; }
        public static CommandHandler Instance
        {
            get {
                if (_Instance == null)
                    _Instance = new CommandHandler();
                return _Instance;
            }
        }

        public void AddCommand(Command cmd) => this.Commands.Add(cmd.Name, cmd);

        public void LoadCommands()
        {
            IEnumerable<Type> services = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.FullName.StartsWith("EnergizeTerminal.Commands.Modules") 
                    && Attribute.IsDefined(type,typeof(CommandModuleAttribute)));
            foreach(Type stype in services)
            {
                object inst = Activator.CreateInstance(stype);
                IEnumerable<MethodInfo> methods = stype.GetMethods()
                    .Where(x => Attribute.IsDefined(x, typeof(CommandAttribute)));
                foreach(MethodInfo method in methods)
                {
                    CommandAttribute att = method.GetCustomAttribute<CommandAttribute>();
                    LoggedInCommandAttribute latt = method.GetCustomAttribute<LoggedInCommandAttribute>();

                    this.AddCommand(new Command(att.Name, att.Help, att.Usage,
                        (Command.Callback)method.CreateDelegate(typeof(Command.Callback), inst), 
                        att.ArgumentCount, latt != null));
                }
            }
        }

        public async Task<CommandResult> TryRunCommandAsync(SentCommand scmd)
        {
            if (!this.Commands.ContainsKey(scmd.Name))
                return new CommandResult(false, "Unknown command.");

            Command cmd = this.Commands[scmd.Name];
            bool authorized = AuthService.IsAuthorized(scmd.Token);
            if (cmd.NeedsLogin && !authorized)
                return new CommandResult(false,"You are not logged in. Login using the \"login\" command.");

            // Refresh the token to last longer
            if (authorized) AuthService.RefreshToken(scmd.Token);

            CommandContext ctx = new CommandContext(this, cmd, scmd);
            List<string> parameters = scmd.Parameters.ToList();

            // If first parameter is empty string remove it
            if (parameters.Count == 1 && string.IsNullOrWhiteSpace(parameters[0]))
                parameters.RemoveAt(0);

            if (parameters.Count > cmd.ArgumentCount)
                return new CommandResult(false, $"Too many arguments\n{ctx.BadUsage}");
            else if (parameters.Count < cmd.ArgumentCount)
                return new CommandResult(false, $"Too few arguments\n{ctx.BadUsage}");

            CommandResult result = await cmd.RunAsync(ctx, parameters.ToArray());
            result.Result = result.Result ?? string.Empty;

            return result;
        }
    }
}
