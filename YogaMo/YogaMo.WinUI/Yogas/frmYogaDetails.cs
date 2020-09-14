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
        private Model.Requests.YogaInsertRequest request = new Model.Requests.YogaInsertRequest();

        public frmYogaDetails(int YogaId = 0)
        {
            InitializeComponent();
            this._yogaId = YogaId;
        }

        private async void frmYogaDetails_Load(object sender, EventArgs e)
        {
            if (_yogaId == 0)
            {
                return;
            }

            _yoga = await _serviceYogas.GetById<Model.Yoga>(_yogaId);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if(_yogaId == 0) // inserting
            {
                var yoga = await _serviceYogas.Insert<Model.Yoga>(request);
                if (yoga != null)
                {
                    MessageBox.Show("Yoga profile has been successfully created.", "Create profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            else // updating
            {
                var yoga = await _serviceYogas.Update<Model.Yoga>(_yogaId, request);
                if (yoga != null)
                {
                    MessageBox.Show("Yoga profile has been successfully updated.", "Update profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
        }

    }
}
