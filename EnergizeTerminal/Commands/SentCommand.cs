using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EnergizeTerminal.Commands
{
    public class SentCommand
    {
        [JsonProperty(PropertyName="command")]
        public string Name { get; set; }

        [JsonProperty(PropertyName="parameters")]
        public string[] Parameters { get; set; }

        [JsonProperty(PropertyName="token")]
        public string Token { get; set; }

        [JsonIgnore]
        public HttpRequest AssociatedRequest { get; set; }
    }
}
