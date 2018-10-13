using Newtonsoft.Json;
using System.IO;
namespace EnergizeTerminal
{
    public class Config
    {
#pragma warning disable 649
        [JsonProperty(PropertyName="TokenBot")]
        private readonly string _TokenBot;

        [JsonProperty(PropertyName="Password")]
        private readonly string _Password;
#pragma warning restore 649

        [JsonIgnore]
        public static string TokenBot;

        [JsonIgnore]
        public static string Password;

        public static void Load()
        {
            using (StreamReader reader = File.OpenText("External/config.json"))
            {
                string json = reader.ReadToEnd();
                Config config = JsonConvert.DeserializeObject<Config>(json);

                TokenBot = config._TokenBot;
                Password = config._Password;
            }
        }
    }
}
