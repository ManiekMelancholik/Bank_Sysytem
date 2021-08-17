using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace RadosławBoczońSystemBankowy
{
    public partial class Form2 : Form
    {
        public Form2()
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
            reciverCheck = true;
            if (senderChceck == true && reciverCheck == true)
                button1.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                listBox1.Items.Clear();
                button3.Enabled = true;

                button7.Enabled = true;


                Time the_Time = new Time();
                Random rng = new Random();
                int rand_Number = rng.Next(100, 1000000);
                double rnd = Convert.ToDouble(rand_Number);


                the_Time.day = dateTimePicker1.Value.Day;
                the_Time.month = dateTimePicker1.Value.Month;
                the_Time.year = dateTimePicker1.Value.Year;
                the_Time.hour = dateTimePicker1.Value.Hour;
                the_Time.minute = dateTimePicker1.Value.Minute;


                Money the_Money = new Money();
                double.TryParse(textBox1.Text, out the_Money.amount);
                the_Money.currency = "PLN";//listOfClients




                TransactionSTANDARD std_Transaction = new TransactionSTANDARD(ref currentBitmap, Form1.listOfClients[comboBox1.SelectedIndex].transactionsThisDay, Form1.listOfClients[comboBox1.SelectedIndex].transactionsThisWeek, ref the_Time, Form1.listOfClients[comboBox1.SelectedIndex], ref the_Money, Form1.listOfClients[comboBox2.SelectedIndex]);
                //Transaction the_Transaction_1 = new Transaction(ref the_Time, listOfClients[0], ref the_Money, listOfClients[1]);
                //Transaction the_Transaction_2 = new Transaction(ref the_Time, listOfClients[1], ref the_Money, listOfClients[0]);
                listBox1.Items.Add(std_Transaction.GenerateAutCode());
                Form1.listOfSTANDARD.Add(std_Transaction);

                Form1.fullListOfTransactions.Add(std_Transaction);
                //Form1.Disp_Element("STANDARD", Form1.list_Of_STANDARD.Count - 1);
            }
            catch (System.IndexOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }

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
            
                double a;
                bool correct = double.TryParse(textBox1.Text, out a);
                if (correct == false)
                    textBox1.Text = "0,00";
            

        }
        private void MyInitializer()
        {
            
                //button4.Enabled = false;
                checkIfComboEnabled = true;
                if (Form1.listOfSTANDARD.Count != 0)
                    button2.Enabled = true;
            foreach(BankClient cli in Form1.listOfClients)
            {
                comboBox1.Items.Add(cli.first_Name + ' ' + cli.last_Name);
                comboBox2.Items.Add(cli.first_Name + ' ' + cli.last_Name);

            }
            
            //comboBox1.Items.Add(0 + Form1.listOfClients[0].first_Name + " " + Form1.listOfClients[0].last_Name);
            //comboBox1.Items.Add(1 + Form1.listOfClients[1].first_Name + " " + Form1.listOfClients[1].last_Name);
            //comboBox2.Items.Add(0 + Form1.listOfClients[0].first_Name + " " + Form1.listOfClients[0].last_Name);
            //comboBox2.Items.Add(1 + Form1.listOfClients[1].first_Name + " " + Form1.listOfClients[1].last_Name);

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 1;
            textBox1.Text="100,00";

        }
        static bool senderChceck = false;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            senderChceck = true;
            if (senderChceck == true && reciverCheck == true)
                button1.Enabled = true;
        }
        //static bool autentication_Attempted = false;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].Authenticate(Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].transactionCode);
                //listBox1.Items.Clear();
                button3.Enabled = false;
                //autentication_Attempted = true;
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            TransactionSTANDARD copied_Transaction = new TransactionSTANDARD(Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1]);
            Form1.listOfSTANDARD.Add(copied_Transaction);

            Form1.fullListOfTransactions.Add(copied_Transaction);
            copied_Transaction.Write(ref listView1, Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].CreateItem());
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
        private object openFileDialog1;

        private void button6_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                button7.Enabled = false;
                button2.Enabled = true;
                if (checkIfComboEnabled == true)
                    button1.Enabled = true;
                Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].Write(ref listView1, Form1.listOfSTANDARD[Form1.listOfSTANDARD.Count - 1].CreateItem());
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                //return;
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            Form1.listOfSTANDARD.Add(new TransactionSTANDARD());

            Form1.fullListOfTransactions.Add(new TransactionSTANDARD());
        }
        //-------------- L6 -----------------------------
        //bool bitMSelected = false;
        string photoName;
        Bitmap currentBitmap;
        private void button9_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialogForm = new OpenFileDialog();//twoży nową formatkę do wczytania zdjecia, nie chciało działać bez tego tak jak na zajęciach :/

            if (dialogForm.ShowDialog()==DialogResult.OK)
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

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null; // czyści obrazek
            currentBitmap.Dispose();//powinno wyczyścić mapę bitową jest inicjalizowana w innym miejscu więc w button9 wieć nie powinno być błędu

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //-------------- L6 -----------------------------
    }
}
