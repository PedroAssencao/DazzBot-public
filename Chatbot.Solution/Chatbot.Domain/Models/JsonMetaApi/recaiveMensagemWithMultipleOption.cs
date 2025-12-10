using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    public class recaiveMensagemWithMultipleOption
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Change
        {
            [JsonPropertyName("value")]
            public Value value { get; set; }

            [JsonPropertyName("field")]
            public string field { get; set; }
        }

        public class Contact
        {
            [JsonPropertyName("profile")]
            public Profile profile { get; set; }

            [JsonPropertyName("wa_id")]
            public string wa_id { get; set; }
        }

        public class Context
        {
            [JsonPropertyName("from")]
            public string from { get; set; }

            [JsonPropertyName("id")]
            public string id { get; set; }
        }

        public class Entry
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("changes")]
            public List<Change> changes { get; set; }
        }

        public class Interactive
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("list_reply")]
            public ListReply list_reply { get; set; }
        }

        public class ListReply
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("title")]
            public string title { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("context")]
            public Context context { get; set; }

            [JsonPropertyName("from")]
            public string from { get; set; }

            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("timestamp")]
            public string timestamp { get; set; }

            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("interactive")]
            public Interactive interactive { get; set; }
        }

        public class Metadata
        {
            [JsonPropertyName("display_phone_number")]
            public string display_phone_number { get; set; }

            [JsonPropertyName("phone_number_id")]
            public string phone_number_id { get; set; }
        }

        public class Profile
        {
            [JsonPropertyName("name")]
            public string name { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("object")]
            public string @object { get; set; }

            [JsonPropertyName("entry")]
            public List<Entry> entry { get; set; }
        }

        public class Value
        {
            [JsonPropertyName("messaging_product")]
            public string messaging_product { get; set; }

            [JsonPropertyName("metadata")]
            public Metadata metadata { get; set; }

            [JsonPropertyName("contacts")]
            public List<Contact> contacts { get; set; }

            [JsonPropertyName("messages")]
            public List<Message> messages { get; set; }
        }


    }
}
