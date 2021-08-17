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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            MyInitializer();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        static bool reciverCheck = false;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        public informationFOrShow GetNeededInformation()
        {
            informationFOrShow neededInfo = new informationFOrShow();
            neededInfo.numberOfElement = Form1.listOfCONSTANT.Count - 1;
            neededInfo.type_Of_Transaction = "STANDARD";
            return neededInfo;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }


        private void button4_Click(object sender, EventArgs e)
        {
            
        }
        static bool senderChceck = false;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }
        //static bool autentication_Attempted = false;
        private void button3_Click(object sender, EventArgs e)
        {
            
                Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].Authenticate(Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].transactionCode);
                //listBox1.Items.Clear();
                button3.Enabled = false;
                //autentication_Attempted = true;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //if (autentication_Attempted == true)
            //{
            //   autentication_Attempted = false;
            //   listBox1.Items.Clear();
            // }



        }

        bool checkIfComboEnabled = false;

        private void button6_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
        private void MyInitializer()
        {
            
                //button4.Enabled = false;
                checkIfComboEnabled = true;
                if (Form1.listOfCONSTANT.Count != 0)
                    button2.Enabled = true;
            foreach (BankClient cli in Form1.listOfClients)
            {
                comboBox1.Items.Add(cli.first_Name + ' ' + cli.last_Name);
                comboBox2.Items.Add(cli.first_Name + ' ' + cli.last_Name);

            }
            //comboBox1.Items.Add(0 + Form1.listOfClients[0].first_Name + " " + Form1.listOfClients[0].last_Name);
            //    comboBox1.Items.Add(1 + Form1.listOfClients[1].first_Name + " " + Form1.listOfClients[1].last_Name);
            //    comboBox2.Items.Add(0 + Form1.listOfClients[0].first_Name + " " + Form1.listOfClients[0].last_Name);
            //    comboBox2.Items.Add(1 + Form1.listOfClients[1].first_Name + " " + Form1.listOfClients[1].last_Name);
                comboBox3.Items.Add("Dayly");
                comboBox3.Items.Add("Weekly");
                comboBox3.Items.Add("Monthly");
            


        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            textBox2.Text = "Something Something to Someone from Someone else";
            textBox1.Text = "100";
            comboBox3.SelectedIndex = 2;
            checkBox1.Checked = true;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                listBox3.Items.Clear();
                button9.Enabled = true;
                button1.Enabled = false;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                button3.Enabled = true;
                button8.Enabled = true;
                button3.Enabled = true;
                listBox2.Items.Clear();
                listBox1.Items.Clear();
                button7.Enabled = true;

                Time theTime = new Time();
                Random rng = new Random();
                int rand_Number = rng.Next(100, 1000000);
                double rnd = Convert.ToDouble(rand_Number);


                theTime.day = dateTimePicker1.Value.Day;
                theTime.month = dateTimePicker1.Value.Month;
                theTime.year = dateTimePicker1.Value.Year;
                theTime.hour = dateTimePicker1.Value.Hour;
                theTime.minute = dateTimePicker1.Value.Minute;
                Money theMoney = new Money();
                double.TryParse(textBox1.Text, out theMoney.amount);
                theMoney.currency = "PLN";//listOfClients




                TransactionCONSTANT con_Transaction = new TransactionCONSTANT(ref currentBitmap, comboBox3.SelectedItem.ToString(), checkBox1.Checked, textBox2.Text, ref theTime, Form1.listOfClients[comboBox1.SelectedIndex], ref theMoney, Form1.listOfClients[comboBox2.SelectedIndex]);
                //Transaction the_Transaction_1 = new Transaction(ref the_Time, listOfClients[0], ref the_Money, listOfClients[1]);
                //Transaction the_Transaction_2 = new Transaction(ref the_Time, listOfClients[1], ref the_Money, listOfClients[0]);
                //listBox1.Items.Add(std_Transaction.Generate_Aut_Code());
                Form1.listOfCONSTANT.Add(con_Transaction);


                Form1.fullListOfTransactions.Add(con_Transaction);
                //Form1.Disp_Element("STANDARD", Form1.list_Of_STANDARD.Count - 1);
            }
            catch (System.NullReferenceException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }            
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
            catch (System.IndexOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
            catch (System.InvalidCastException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
            catch (System.OverflowException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                listBox3.Items.Clear();
                button9.Enabled = true;
                listBox2.Items.Clear();
                listBox1.Items.Clear();
                TransactionCONSTANT copied_Transaction = new TransactionCONSTANT(Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count - 1]);
                Form1.listOfCONSTANT.Add(copied_Transaction);

                Form1.fullListOfTransactions.Add(copied_Transaction);
                copied_Transaction.Write(ref listView1, Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count - 1].CreateItem());
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            button9.Enabled = true;
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            button7.Enabled = true;
            Form1.listOfCONSTANT.Add(new TransactionCONSTANT());


            Form1.fullListOfTransactions.Add(new TransactionCONSTANT());


            button8.Enabled = false;
            button3.Enabled = false;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                button7.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = false;
                button8.Enabled = false;
                if (checkIfComboEnabled == true)
                    button1.Enabled = true;
                Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count - 1].Write(ref listView1, Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count - 1].CreateItem());
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        bool howOftenCheck = false;
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)// w teori można wsykoczyć poza indeks i chociaż nie ma w kodzie skakania tutaj po indeksie, to lepiej sie przed tym zabespieczyć
        {
            
                senderChceck = true;
                if (senderChceck == true && reciverCheck == true && howOftenCheck == true)
                    button1.Enabled = true;
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
                reciverCheck = true;
                if (senderChceck == true && reciverCheck == true && howOftenCheck == true)
                    button1.Enabled = true;
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                howOftenCheck = true;
                if (senderChceck == true && reciverCheck == true && howOftenCheck == true)
                    button1.Enabled = true;
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
            catch (System.NullReferenceException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
                double a;
                bool correct = double.TryParse(textBox1.Text, out a);
                if (correct == false)
                    textBox1.Text = "0,00";//tutaj nie da się wpisać innej wartości niż liczbowa, trudno powiedziec czy text changed wychwytuje kopiowanie i wklejanie więc lepiej try parse

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                listBox2.Items.Clear();
                listBox2.Items.Add("" + Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count() - 1].CalculateBalanceThisContTransactionThisMonth());
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("" + Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count() - 1].EvaluatePosibleFunds());
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                button9.Enabled = false;

                for (int i = 0; i < 6; i++)
                {
                    listBox3.Items.Add("Day" + Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count() - 1].sixNextTransactionDates[i].day + "  Month" + Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count() - 1].sixNextTransactionDates[i].month + "  Year" + Form1.listOfCONSTANT[Form1.listOfCONSTANT.Count() - 1].sixNextTransactionDates[i].year);

                }
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }


        }

        //-------------- L6 ----------------------------- // ctrl+C ctrl+V z Form2
        string photoName;
        Bitmap currentBitmap;
        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogForm = new OpenFileDialog();//twoży nową formatkę do wczytania zdjecia, nie chciało działać bez tego tak jak na zajęciach :/

            if (dialogForm.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    photoName = dialogForm.FileName;
                    currentBitmap = new Bitmap(photoName);
                    pictureBox1.Image = currentBitmap;
                }
                catch (System.ArgumentException exeption)
                {
                    MessageBox.Show(exeption.Message);
                    //return;
                }


            }
            dialogForm.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null; // czyści obrazek
            currentBitmap.Dispose();//powinno wyczyścić mapę bitową jest inicjalizowana w innym miejscu więc w button9 wieć nie powinno być błędu
        }

        //-------------- L6 ----------------------------- // ctrl+C ctrl+V z Form2
    }
}
   
