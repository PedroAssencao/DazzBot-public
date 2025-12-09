using System.Text.Json.Serialization;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    public class sendMessageSuccess
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Contact
        {
            [JsonPropertyName("input")]
            public string? input { get; set; }

            [JsonPropertyName("wa_id")]
            public string? wa_id { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("id")]
            public string id { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("messaging_product")]
            public string? messaging_product { get; set; }

            [JsonPropertyName("contacts")]
            public List<Contact>? contacts { get; set; }

            [JsonPropertyName("messages")]
            public List<Message>? messages { get; set; }
        }


    }
}
