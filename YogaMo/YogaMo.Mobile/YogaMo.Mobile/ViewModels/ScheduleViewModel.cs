using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using YogaMo.Model;

namespace YogaMo.Mobile.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private readonly APIService _serviceYogaClasses = new APIService("YogaClasses");

        public ScheduleViewModel()
        {
            Title = "Yoga Schedule";
            LoadDaysOfWeek();
        }

        public ObservableCollection<string> DaysOfWeek { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<YogaClass> YogaClasses { get; set; } = new ObservableCollection<YogaClass>();
        private string _selectedDay;

        public string SelectedDay
        {
            get { return _selectedDay; }
            set { SetProperty(ref _selectedDay, value); }
        }


        private async void LoadDaysOfWeek()
        {
            DaysOfWeek.Clear();
            foreach (var dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                DaysOfWeek.Add(Enum.GetName(typeof(DayOfWeek), dayOfWeek));
            }
            DaysOfWeek.Add(DaysOfWeek[0]);
            DaysOfWeek.RemoveAt(0);

            foreach (var day in DaysOfWeek)
            {
                if (DateTime.Now.DayOfWeek.ToString() == day)
                {
                    SelectedDay = day;
                    await LoadSchedule();
                }
            }

        }

        public async Task LoadSchedule()
        {
            var request = new Model.Requests.YogaClassSearchRequest
            {
                Day = SelectedDay
            };

            var list = await _serviceYogaClasses.Get<List<YogaClass>>(request);
            YogaClasses.Clear();
            foreach (var item in list)
            {
                YogaClasses.Add(item);
            }
        }
    }
}
