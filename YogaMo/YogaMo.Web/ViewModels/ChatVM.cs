using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.ViewModels
{
    public class ChatVM
    {
        public List<Instructor> Instructors{ get; set; }
        public List<Client> Clients { get; set; }
        public UserInfoVM Sender { get; set; }
        public string Role { get; internal set; }

        public List<DirectMessagesVM> DirectMessages { get; set; }
        public List<string> ChannelsList { get; set; }
    }

    public class DirectMessagesVM
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("instructor")]
        public DMParticipant Instructor { get; set; }
        [JsonProperty("client")]
        public DMParticipant Client { get; set; }
        [JsonProperty("lastMessage")]
        public DateTime LastMessage { get; set; }
        [JsonProperty("lastMessageTimestamp")]
        public long LastMessageTimestamp => LastMessage.MillisecondsTimestamp();

        [JsonProperty("lastSeenClient")]
        public string LastSeenClient { get; set; }
        [JsonProperty("lastSeenInstructor")]
        public string LastSeenInstructor { get; set; }
    }

    public class DMParticipant
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
