using System;

namespace EnergizeTerminal.Commands
{
    public class CommandAttribute : Attribute
    {
        public string Name { get; }
        public string Help { get; }
        public string Usage { get; }
        public uint ArgumentCount { get; }

        public CommandAttribute(string name,string help,string usage=null,uint argcount=0)
        {
            this.Name = name;
            this.Help = help;
            this.Usage = usage ?? name;
            this.ArgumentCount = argcount;
        }
    }
}
