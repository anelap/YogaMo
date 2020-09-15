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

namespace YogaMo.WinUI.Yogas
{
    public partial class frmYogaDetails : Form
    {
        private Yoga _yoga;
        private int _yogaId;

        private readonly APIService _serviceYogas = new APIService("Yogas");
        private readonly APIService _serviceInstructors = new APIService("Instructors");
        private Model.Requests.YogaInsertRequest request = new Model.Requests.YogaInsertRequest();

        public frmYogaDetails(int YogaId = 0)
        {
            InitializeComponent();
            this._yogaId = YogaId;
        }

        private async void frmYogaDetails_Load(object sender, EventArgs e)
        {
            await LoadInstructors();
            await LoadYoga();
        }

        private async Task LoadInstructors()
        {
            cmbInstructor.DataSource = await _serviceInstructors.Get<List<Model.Instructor>>();
            cmbInstructor.DisplayMember = "Name";
        }

        private async Task LoadYoga()
        {
            if (_yogaId == 0)
            {
                return;
            }

            _yoga = await _serviceYogas.GetById<Model.Yoga>(_yogaId);

            txtName.Text = _yoga.Name;
            txtDescription.Text = _yoga.Description;
            foreach (Model.Instructor item in cmbInstructor.Items)
            {
                if(item.InstructorId == _yoga.InstructorId)
                {
                    cmbInstructor.SelectedItem = item;
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            request.InstructorId = (cmbInstructor.SelectedItem as Model.Instructor).InstructorId;
            request.Name = txtName.Text;
            request.Description = txtDescription.Text;

            if(_yogaId == 0) // inserting
            {
                var yoga = await _serviceYogas.Insert<Model.Yoga>(request);
                if (yoga != null)
                {
                    MessageBox.Show("Yoga has been successfully created.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            else // updating
            {
                var yoga = await _serviceYogas.Update<Model.Yoga>(_yogaId, request);
                if (yoga != null)
                {
                    MessageBox.Show("Yoga has been successfully updated.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if (element.Text.Length > 50)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MaximumLength, 50));
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void cmbInstructor_Validating(object sender, CancelEventArgs e)
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
