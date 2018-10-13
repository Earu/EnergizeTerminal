using System;
using System.Threading.Tasks;

namespace EnergizeTerminal.Commands
{
    public class Command
    {
        public delegate Task<string> Callback(CommandContext ctx,params string[] parameters);

        private readonly Callback _Callback;

        public Command(string name,string help,string usage,Callback callback,uint argcount,bool nlogin=false)
        {
            this.Name = name;
            this.Help = help;
            this.Usage = usage;
            this._Callback = callback;
            this.ArgumentCount = argcount;
            this.NeedsLogin = nlogin;
        }

        public string Name { get; }
        public string Help { get; }
        public string Usage { get; }
        public uint ArgumentCount { get; }
        public bool NeedsLogin { get; }

        public async Task<CommandResult> RunAsync(CommandContext ctx,params string[] parameters)
        {
            string result;
            bool success;
            try
            {
                result = await this._Callback(ctx,parameters);
                success = true;
            }
            catch(Exception e)
            {
                result = e.Message;
                success = false;
            }

            return new CommandResult(success, result);
        }
    }
}