using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YogaMo.Model;

namespace YogaMo.WinUI.YogaClasses
{
    public partial class frmYogaClassDetails : Form
    {
        private YogaClass _yogaClass;
        private int _yogaClassId;

        private readonly APIService _serviceYogaClasses = new APIService("YogaClasses");
        private readonly APIService _serviceYogas = new APIService("Yogas");
        private Model.Requests.YogaClassInsertRequest request = new Model.Requests.YogaClassInsertRequest();

        public frmYogaClassDetails(int YogaId = 0)
        {
            InitializeComponent();
            this._yogaClassId = YogaId;
        }

        private async void frmYogaDetails_Load(object sender, EventArgs e)
        {
            LoadDaysOfWeek();
            await LoadYogas();

            if (_yogaClassId == 0)
            {
                return;
            }

            _yogaClass = await _serviceYogaClasses.GetById<Model.YogaClass>(_yogaClassId);

            string[] timeParts = _yogaClass.Time.Split('-');

            DateTime date1 = DateTime.Today;
            date1 = date1.Add(TimeSpan.Parse(timeParts[0]));

            DateTime date2 = DateTime.Today;
            date2 = date2.Add(TimeSpan.Parse(timeParts[1]));

            dtpFrom.Value = date1;
            dtpTo.Value = date2;

            foreach (Model.Yoga yoga in cmbYoga.Items)
            {
                if (yoga.YogaId == _yogaClass.YogaId)
                    cmbYoga.SelectedItem = yoga;
            }

            cmbDay.Text = _yogaClass.Day;
        }

        private async Task LoadYogas()
        {
            cmbYoga.DataSource = await _serviceYogas.Get<List<Model.Yoga>>();
            cmbYoga.DisplayMember = "Name";
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            request.YogaId = (cmbYoga.SelectedItem as Model.Yoga).YogaId;
            request.Time = dtpFrom.Value.ToString("HH:mm") + "-" + dtpTo.Value.ToString("HH:mm");
            request.Day = cmbDay.Text;

            if(_yogaClassId == 0) // inserting
            {
                var yogaClass = await _serviceYogaClasses.Insert<Model.YogaClass>(request);
                if (yogaClass != null)
                {
                    MessageBox.Show("Yoga class has been successfully created.", "Create yoga class", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            else // updating
            {
                var yogaClass = await _serviceYogaClasses.Update<Model.YogaClass>(_yogaClassId, request);
                if (yogaClass != null)
                {
                    MessageBox.Show("Yoga class has been successfully updated.", "Update yoga class", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
        }
        private void LoadDaysOfWeek()
        {
            var DaysOfWeek = new List<string>();
            foreach (var dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                DaysOfWeek.Add(Enum.GetName(typeof(DayOfWeek), dayOfWeek));
            }
            DaysOfWeek.Add(DaysOfWeek[0]);
            DaysOfWeek.RemoveAt(0);

            cmbDay.DataSource = DaysOfWeek;
        }

        private void cmbDay_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as ComboBox);
            if (element.SelectedItem == null)
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }
    }
}
