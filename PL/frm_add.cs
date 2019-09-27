using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_Your_Code.PL
{
    public partial class frm_add : Form
    {
        //for save and edit in the same form
        public string state = "add";
        //inherit from frm_view and DataSet
        frm_view frm = new frm_view();
        DataSet Ds = new DataSet();
        public frm_add()
        {
            InitializeComponent();
            //calls xmlFile and dataGriedView from frm_view 
            Ds.ReadXml("myFilexml.xml");
            frm.dataGridView1.DataSource = Ds.Tables[0];

        }

        //save code botton
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (state == "add")
            {
                if (txtID.Text != "")
                { 
                DataRow r = Ds.Tables[0].NewRow();
                r[0] = txtID.Text;
                r[1] = txtDes.Text;
                r[2] = txtCode.Text;
                r[3] = txtNote.Text;
                Ds.Tables[0].Rows.Add(r);
                Ds.WriteXml("myFilexml.xml");
                MessageBox.Show("تم حفظ الكود بنجاح", "اضافة كود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                }
                else
                {
                    MessageBox.Show("تاكد من كتابة اسم الكود", "اضافة كود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                for (int i = 0; i < Ds.Tables[0].Rows.Count - 1; i++)
                {

                    if (txtID.Text == Ds.Tables[0].Rows[i]["ID_Code"].ToString())
                    {
                        DataRow r = Ds.Tables[0].Rows[i];
                        r[0] = txtID.Text;
                        r[1] = txtDes.Text;
                        r[2] = txtCode.Text;
                        r[3] = txtNote.Text;
                        Ds.WriteXml("myFilexml.xml");
                        MessageBox.Show("تم تحديث الكود بنجاح", "تحديث كود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
        }



        //Exit botton
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
