using YogaMo.Mobile.Models;
using YogaMo.Mobile.Services;
using YogaMo.Mobile.Views;
using PubnubApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace YogaMo.Mobile.ViewModels
{

    public class ChatViewModel : BaseViewModel
    {
        private readonly APIService _serviceInstructors = new APIService("Instructors");

        private Model.Instructor _instructor;
        public string channel_name;

        public Model.Instructor Instructor
        {
            get { return _instructor; }
            set { SetProperty(ref _instructor, value); }
        }

        public int _instructorId { get; set; }



        public Pubnub PN { get; }
        public SubscribeCallbackExt SubscribeListener { get; }

        public ObservableCollection<PNMessage> MessagesList { get; set; } = new ObservableCollection<PNMessage>();

        private string _newMessage = string.Empty;

        public string NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }

        public ChatViewModel(int instructorId)
        {
            _instructorId = instructorId;
            PN = PubNubHelper.GetPubnub();

            SubscribeListener = GetSubscribeListener();
        }

        private SubscribeCallbackExt GetSubscribeListener()
        {
            return new SubscribeCallbackExt(
    async delegate (Pubnub pnObj, PNMessageResult<object> pubMsg)
    {
        if (pubMsg == null)
        {
            return;
        }
        string jsonString = pubMsg.Message.ToString();

        if (jsonString != null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                while (MessagesList.Count >= 10)
                {
                    MessagesList.RemoveAt(0);
                }

                var meta = pubMsg.UserMetadata as Dictionary<string, object>;
                string DatumText = TimeHelper.GetMessageDateString(pubMsg.Timetoken);

                MessagesList.Add(new PNMessage
                {
                    Date = DatumText,
                    User = meta["sender"].ToString(),
                    Content = jsonString

                });

                //await Task.Delay(200);
                //await MessagesScrollView.ScrollToAsync(PosaljiBtn, ScrollToPosition.End, false);

            });


        }
    },
    delegate (Pubnub pnObj, PNPresenceEventResult presenceEvnt)
    {
        if (presenceEvnt != null)
        {
            Debug.Print("In Example, SubscribeCallback received PNPresenceEventResult");
            Debug.Print("", presenceEvnt.Channel + " " + presenceEvnt.Occupancy + " " + presenceEvnt.Event);
        }
    },
    async delegate (Pubnub pnObj, PNStatus pnStatus)
    {
        if (pnStatus.Category == PNStatusCategory.PNConnectedCategory)
        {
            PNResult<PNHistoryResult> results = await PN.History().Channel(channel_name).Count(10).IncludeMeta(true).ExecuteAsync();

            var HistoryResult = results.Result;
            Device.BeginInvokeOnMainThread(async () =>
            {
                for (int i = 0; i < MessagesList.Count; i++)
                {
                    MessagesList.RemoveAt(i);
                    i--;
                }
                foreach (var item in HistoryResult.Messages)
                {
                    if (item.Meta is Dictionary<string, object> meta)
                    {
                        string DatumText = TimeHelper.GetMessageDateString(item.Timetoken);

                        MessagesList.Add(new PNMessage
                        {
                            Content = item.Entry.ToString(),
                            User = System.Net.WebUtility.HtmlDecode(meta["sender"].ToString()),
                            Date = DatumText
                        });
                    }
                    else
                    {
                        MessagesList.Add(new PNMessage
                        {
                            Content = item.Entry.ToString()
                        });
                    }


                    //await Task.Delay(200);
                    //MessagesScrollView.ScrollToAsync(PosaljiBtn, ScrollToPosition.End, false);
                }
            });
        }
    });
        }

        public async Task Init()
        {
            Instructor = await _serviceInstructors.GetById<Model.Instructor>(_instructorId);
            Title = "Chat";

            channel_name = "dm-client-" + APIService.CurrentUser.ClientId + "-instructor-" + Instructor.InstructorId;

            PN.AddListener(SubscribeListener);

            PN.Subscribe<string>()
                .Channels(new string[]{
                    channel_name
            }).Execute();
        }

        public void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage))
                return;

            var meta = new Dictionary<string, object>
            {
                ["role"] = "Client",
                ["sender"] = APIService.CurrentUser.FullName
            };

            PN.Publish()
                .Channel(channel_name)
                .Message(NewMessage)
                .ShouldStore(true)
                .Meta(meta)
                .ExecuteAsync();

            while (MessagesList.Count >= 10)
            {
                MessagesList.RemoveAt(0);
            }

            NewMessage = "";
        }

    }
}
