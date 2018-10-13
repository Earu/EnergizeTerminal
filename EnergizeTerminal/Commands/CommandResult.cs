using Newtonsoft.Json;

namespace EnergizeTerminal.Commands
{
    public class CommandResult
    {
        [JsonProperty(PropertyName="success")]
        public bool IsSuccess { get; set; }

        [JsonProperty(PropertyName="result")]
        public string Result { get; set; }

        public CommandResult(bool success,string res)
        {
            this.IsSuccess = success;
            this.Result = res;
        }
    }
}
