using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;
using Flurl;
using YogaMo.Model.Requests;
using YogaMo.Model;

namespace YogaMo.WinUI.YogaClasses
{
    public partial class frmYogaClasses : Form
    {
        private readonly APIService _serviceYogaClasses = new APIService("YogaClasses");
        public frmYogaClasses()
        {
            InitializeComponent();
        }

        private async Task LoadYogaClasses()
        {
            var request = new YogaClassSearchRequest()
            {
                Day = cmbDay.Text
            };
            var result = await _serviceYogaClasses.Get<List<Model.YogaClass>>(request);
            dgvYogaClasses.AutoGenerateColumns = false;
            dgvYogaClasses.DataSource = result;
        }

        private async void frmYogaClasses_Load(object sender, EventArgs e)
        {
            await LoadDaysOfWeek();
        }

        private async void dgvYogaClasses_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var yogaClass = dgvYogaClasses.SelectedRows[0].DataBoundItem as Model.YogaClass;
            if(yogaClass != null)
            {
                var frm = new frmYogaClassDetails(yogaClass.YogaClassId);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadYogaClasses();
                }
            }
        }

        private async void btnNewYogaClasses_Click(object sender, EventArgs e)
        {
            frmYogaClassDetails frm = new frmYogaClassDetails();
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadYogaClasses();
        }


        private async void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadYogaClasses();
        }
        private async Task LoadDaysOfWeek()
        {
            var DaysOfWeek = new List<string>();
            foreach (var dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                DaysOfWeek.Add(Enum.GetName(typeof(DayOfWeek), dayOfWeek));
            }
            DaysOfWeek.Add(DaysOfWeek[0]);
            DaysOfWeek.RemoveAt(0);

            cmbDay.SelectedIndexChanged -= cmbDay_SelectedIndexChanged;
            cmbDay.DataSource = DaysOfWeek;
            cmbDay.SelectedIndexChanged += cmbDay_SelectedIndexChanged;

            foreach (string day in cmbDay.Items)
            {
                if (DateTime.Now.DayOfWeek.ToString() == day)
                {
                    cmbDay.SelectedItem = day;
                    await LoadYogaClasses();
                }
            }

        }
    }
}
