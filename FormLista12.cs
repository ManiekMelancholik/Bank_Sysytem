using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RadosławBoczońSystemBankowy
{
    public partial class FormLista12 : Form
    {
        public FormLista12()
        {
            InitializeComponent();
        }

        private void FormLista12_Load(object sender, EventArgs e)
        {

        }

        private void b_Search_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            bool data1 = checkBox1.Checked,
                data2 = checkBox2.Checked,
                nameAndSurrname = checkBox3.Checked,
                //surrname = checkBox4.Checked,
                transTime = false,
                transNames = false;


            foreach (Transaction trans in Form1.fullListOfTransactions)
            {

                //data
                if (data1 ==true || data2==true || nameAndSurrname == true)
                {
                    if (data1 == false && data2 == false)
                        transTime = true;
                    else if (data1 == true && data2 == true)
                        transTime = trans.Search(dateTimePicker1.Value, dateTimePicker2.Value);
                    else if (data1 == true)
                        transTime = trans.Search(dateTimePicker1.Value);
                    else if (data2 == true)
                        transTime = trans.Search(dateTimePicker2.Value);
                    else
                        transTime = false;
                    if (nameAndSurrname == true)
                    {
                        transNames = trans.Search(textBox1.Text, textBox2.Text);
                    }
                    else
                        transNames = true;

                    if (transNames ==true && transTime == true)
                    {
                        listView1.Items.Add(trans.CreateItem());
                    }
                }

            }
        }
    }
}
