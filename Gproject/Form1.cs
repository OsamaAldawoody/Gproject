using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Gproject
{
    public partial class Form1 : Form
    {
        private static string path = @"D:\json\position.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(txtX.Text == ""|| txtY.Text == ""||txtZ.Text == ""))
            {
                Position position = new Position(int.Parse(txtX.Text.Trim()), int.Parse(txtY.Text.Trim()), int.Parse(txtZ.Text.Trim()));

                serializeObjectAndSaveItInFile((Position)position, path);
            }
            else
            {
                showWarningMessage();
            }    
        }
        

        private void txtX_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowDigitsOnly(sender, e);
        }

        private void txtY_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowDigitsOnly(sender, e);
        }

        private void txtZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowDigitsOnly(sender, e);
        }

       
        public void showWarningMessage()
        {
            MessageBox.Show("Enter Coordinates", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void showSuccessfulMessage()
        {
            MessageBox.Show("File Saved !", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void serializeObjectAndSaveItInFile(object obj,string path)
        {
            string jsonResult = JsonConvert.SerializeObject(obj);

            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult.ToString());
                    tw.Close();
                }
                showSuccessfulMessage();
            }
            else if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult.ToString());
                    tw.Close();
                }
                showSuccessfulMessage();
            }
        }

        public void allowDigitsOnly(object sender,KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == '-')))
            {
                e.Handled = true;
            }
        }
    }
    
}
