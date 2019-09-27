using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Tulpep.NotificationWindow;

namespace Save_Your_Code.PL
{
    public partial class frm_view : Form
    {

        public frm_view()
        {
            InitializeComponent();
            dv = new DataView();
            ds = new DataSet();
        }

        public DataSet ds
        {
            get;
            set;
        }

        public DataView dv
        {
            get;
            set;
        }


        //delete botton
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (textBox1.Text == ds.Tables[0].Rows[i]["ID_Code"].ToString())
                    {
                        ds.Tables[0].Rows[i].Delete();
                        ds.WriteXml("myFilexml.xml");
                        MessageBox.Show("تم مسح الكود بنجاح", "مسح كود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("حدد الكود المراد مسحه", "مسح كود", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //show id in textbox for delete
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        //add botton
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            frm_add frm = new frm_add();
            frm.ShowDialog();
        }

        //update botton
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            frm_add frm = new frm_add();
            frm.txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.txtDes.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm.txtCode.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm.txtNote.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frm.btnAdd.Text = "تعديل";
            frm.state = "update";
            frm.txtID.ReadOnly = true;
            frm.txtDes.Select();
            frm.ShowDialog();
        }
        //Exit botton
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //search in dataGriedView
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dv.RowFilter = "ID_Code + Des_code + Format_code + Note_code like '%" + txtSearch.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        //necessry for show xml data in dataGriedView
        private void frm_view_Load(object sender, EventArgs e)
        {
            string path = "myFilexml.xml";
            ds.ReadXml(path);
            dv.Table = ds.Tables[0];
            dataGridView1.DataSource = dv;

            //رسالة ترحيبية 
            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType() == typeof(MdiClient))
                {
                    ctl.BackColor = Color.CornflowerBlue;
                }
                {
                    PopupNotifier pope = new PopupNotifier();
                    pope.TitleText = "❦ملحوظه❦";
                    pope.TitleFont = new Font("Arial", 18, FontStyle.Bold);
                    pope.TitleColor = Color.White;
                    pope.IsRightToLeft = true;
                    pope.ContentText = "ابحث اولا عن الكود ثم اضغط عرض";
                    pope.ContentFont = new Font("Arial", 15, FontStyle.Regular);
                    pope.ContentColor = Color.White;
                    pope.BodyColor = Color.MediumPurple;
                    pope.Popup();
                }
            }

        }

        //from xml to txt
        //view botton
        private void button5_Click(object sender, EventArgs e)
        {
            //This line of code creates a text file for the data export.

            StreamWriter file = new StreamWriter(@"D:\MyCode.txt");
            try
            {
                string sLine = "";

                //This for loop loops through each row in the table
                for (int r = 0; r <= dataGridView1.Rows.Count - 1; r++)
                {
                    //This for loop loops through each column, and the row number
                    //is passed from the for loop above.
                    for (int c = 0; c <= dataGridView1.Columns.Count - 1; c++)
                    {
                        sLine = sLine + dataGridView1.Rows[r].Cells[c].Value;
                        if (c != dataGridView1.Columns.Count - 1)
                        {
                            //A comma is added as a text delimiter in order
                            //to separate each field in the text file.
                            //You can choose another character as a delimiter.
                            sLine = sLine + ",";
                        }
                    }
                    //The exported text is written to the text file, one line at a time.
                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
               MessageBox.Show("تم حفظ الكود وسيتم فتحه", "حفظ الكود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //لفتح الملف مباشرة ببرنامج نوت باد
                Process.Start(@"D:\MyCode.txt");
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }
    }
}


