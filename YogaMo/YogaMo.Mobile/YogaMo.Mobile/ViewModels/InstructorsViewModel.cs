using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YogaMo.Mobile.Views;

namespace YogaMo.Mobile.ViewModels
{
    public class InstructorsViewModel: BaseViewModel
    {
        private readonly APIService _serviceInstructors = new APIService("Instructors");
        private readonly INavigation Navigation;

        public ICommand SendMessageCommand { get; set; }
        public InstructorsViewModel(INavigation navigation)
        {
            Title = "Instructors";
            SendMessageCommand = new Command<Model.Instructor>(OnSendMessageClicked);
            this.Navigation = navigation;
        }

        private async void OnSendMessageClicked(Model.Instructor obj)
        {
            await Navigation.PushAsync(new ChatPage(obj.InstructorId));
        }

        public ObservableCollection<Model.Instructor> Instructors { get; set; } = new ObservableCollection<Model.Instructor>();
       
       
        public async Task LoadInstructors()
        {

            var list = await _serviceInstructors.Get<List<Model.Instructor>>(null);
            Instructors.Clear();
            foreach (var item in list)
            {

                if (item.ProfilePicture == null || item.ProfilePicture.Length == 0)
                {
                    item.ProfilePicture = File.ReadAllBytes("default_profile.png");
                }
                Instructors.Add(item);
            }
        }

        public async Task Init()
        {
            await LoadInstructors();
        }
    }
}
