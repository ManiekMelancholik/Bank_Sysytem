using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RadosławBoczońSystemBankowy
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            MyInitializer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //przycisk zapisu
            preventColidingActions(true);
            if (Form1.fullListOfTransactions.Count > 0)
            {
                
                    SaveFileDialog saveForm = new SaveFileDialog();
                    saveForm.AddExtension = true;
                    saveForm.DefaultExt = "TXT";

                    if (saveForm.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter save = new StreamWriter(saveForm.FileName))
                        {
                            save.WriteLine("ID=Radosław.Boczoń.Lista9");
                            foreach (Transaction t in Form1.fullListOfTransactions)
                                t.Save(save);

                            save.Dispose();
                            //save.WriteLine("Lista9END");
                        }

                    }
                    saveForm.Dispose();
                
                
            }

            preventColidingActions(false);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //odczyt
            preventColidingActions(true);
            OpenFileDialog loadForm = new OpenFileDialog();
            loadForm.AddExtension = true;
            loadForm.DefaultExt = "TXT";

            if (loadForm.ShowDialog() == DialogResult.OK)
            {
                
                var y = new string("");
                using StreamReader load = new StreamReader(loadForm.FileName);
                if (load.ReadLine() == "ID=Radosław.Boczoń.Lista9")
                {
                    // string z = load.ReadLine();
                    //while (z != "Lista9END")
                    
                        while (load.EndOfStream == false)
                        {
                            if (load.ReadLine() == "----NOT-EMPTY----")
                            {
                                if (load.ReadLine() == "-----CON-----")
                                {
                                    var transCON = new TransactionCONSTANT(load);
                                    Form1.fullListOfTransactions.Add(transCON);
                                }
                                else
                                {
                                    var transSTD = new TransactionSTANDARD(load);
                                    Form1.fullListOfTransactions.Add(transSTD);

                                }
                            }
                            else
                            {
                                if (load.ReadLine() == "-----CON-----")
                                {
                                    var emptyCON = new TransactionCONSTANT();
                                    Form1.fullListOfTransactions.Add(emptyCON);
                                }
                                else
                                {
                                    var emptySTD = new TransactionSTANDARD();
                                    Form1.fullListOfTransactions.Add(emptySTD);

                                }
                            }
                            //z = load.ReadLine();
                        }
                    

                }

                load.Dispose();

            }
            loadForm.Dispose();
            listView1.Items.Clear();
            preventColidingActions(false);
            MyInitializer();
            
        }


       

        private void MyInitializer()
        {
            // if (Form1.listOfCONSTANT.Count > 0)
            listView1.Items.Clear();
            listBox1.Items.Clear();
            pictureBox1.Image = null;

                button2.Enabled = true;
            if (Form1.fullListOfTransactions.Count > 0)
            {
                button1.Enabled = true;
                showEarnings.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                    
            }
            if (Form1.fullListOfTransactions.Count > 0)
                for (int i = 0; i < Form1.fullListOfTransactions.Count; i++)
                    listView1.Items.Add(Form1.fullListOfTransactions[i].GetMyInfo());

            if (listView1.Items.Count == 0)
                DeleteObject.Enabled = false;

        }
        //desc
        private void button4_Click(object sender, EventArgs e)
        {
            preventColidingActions(true);

            Form1.fullListOfTransactions.Sort();
            Form1.fullListOfTransactions.Reverse();//poprostu odwraca kolejnośc elementów na liście/ nie mogłem znaleźć rozwiązania aby podać dwa elementy i móc wpłynąć na to co z czym jest porównywane
            // w sensie jak mamy ASC to porównujemy this. z podaną trans, a żeby zrobić normalnie DESC to należałoby porównywać odwrotnie podaną Trans z this.
            preventColidingActions(false);
            MyInitializer();

            
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (listView1.SelectedItems.Count != 0)
                {
                    int index = listView1.SelectedItems[0].Index;
                    //listView1.selectedIndex
                    //listView1.SelectedItems.Clear();
                    listBox1.Items.Clear();
                    pictureBox1.Image = null;
                    listBox1.Items.Clear();
                    pictureBox1.Image = null;

                    pictureBox1.Image = Form1.fullListOfTransactions[index].getPhoto();

                    ListViewItem L = Form1.fullListOfTransactions[index].CreateItem();
                    for (int i = 0; i < L.SubItems.Count; i++)
                    {

                        //toString(L.SubItems[i]);
                        listBox1.Items.Add(L.SubItems[i].ToString());



                    }
                    //listView1.SelectedIndices.Clear();
                    //listView1
                }
            
        }
        bool butt1State;
        bool butt2State;
        bool butt3State;
        bool butt4State;
        bool butt5State;
        bool butt6State;
        bool showEarningsState;
        private void preventColidingActions(bool state)
        {
            if (state == true)
            {
                butt1State = button1.Enabled;
                butt2State = button2.Enabled;
                butt3State = button3.Enabled;
                butt4State = button4.Enabled;
                butt5State = button5.Enabled;
                butt6State = button6.Enabled;
                showEarningsState = showEarnings.Enabled;

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                showEarnings.Enabled = false;
            }
            else
            {
                button1.Enabled = butt1State;
                button2.Enabled = butt2State;
                button3.Enabled = butt3State;
                button4.Enabled = butt4State;
                button5.Enabled = butt5State;
                button6.Enabled = butt6State;
                showEarnings.Enabled = showEarningsState;
            }
        }
        

        //REFRESH LIST
        private void button3_Click(object sender, EventArgs e)
        {

            MyInitializer();
        }
        //ASC
        private void button5_Click(object sender, EventArgs e)
        {
            preventColidingActions(true);
            Form1.fullListOfTransactions.Sort();
            preventColidingActions(false);
            MyInitializer();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            Form1.fullListOfTransactions.Clear();
            button2_Click(sender, e);
            

            
        }

        private void DeleteObject_Click(object sender, EventArgs e)
        {
            
            if (listView1.SelectedIndices.Count != 0)
            {
                preventColidingActions(true);
                DeleteObject.Enabled = false;
                var toBeDeleted = listView1.SelectedIndices;
                for (int i = 0; i < toBeDeleted.Count; i++)
                    Form1.fullListOfTransactions.RemoveAt(toBeDeleted[i] - i);


                //w to be deleted są indeksy elementów i korenspondują one bezpośrednio z elementami na liście - kolejnośc się zgadza
                // wiec usuwamy elementy zaznaczone i uwzgledniając przesunięcie odejmujemy i od indeksu.



                listView1.Items.Clear();
                preventColidingActions(false);
                MyInitializer();
                DeleteObject.Enabled = true;
            }
            

        }

        private void showEarnings_Click(object sender, EventArgs e)
        {
            preventColidingActions(true);
            if (listView1.Items.Count!=0)
            {
                listBox1.Items.Clear();
                pictureBox1.Image = null;
                this.Enabled = false;
                double bankProfit = 0.00D;
                double bankProfitOnSTD = 0.00D;
                double bankProfitOnCON = 0.00D;
                int numOfSTD = 0;
                int numOfCON = 0;

                double profitFromTransaction = 0.0D;
                
                    foreach (Transaction trans in Form1.fullListOfTransactions)
                    {
                        profitFromTransaction = trans.MoneyEarnedOnTransaction();
                        bankProfit += profitFromTransaction;
                        if (trans.CheckType() == "STD")
                        {
                            numOfSTD++;
                            bankProfitOnSTD += profitFromTransaction;
                        }
                        if (trans.CheckType() == "CON")
                        {
                            numOfCON++;
                            bankProfitOnCON += profitFromTransaction;
                        }

                    }
                
                
                listBox1.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                listBox1.Items.Add("Bank profited from those transactions by " + bankProfit + "  PLN");
                listBox1.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                if (numOfSTD != 0)
                {
                    listBox1.Items.Add("" + (bankProfitOnSTD * 100) / (bankProfit) + "%  of the profit comes from STD transations, witch is " + bankProfitOnSTD);
                    listBox1.Items.Add("Arithmetic mean for STD transactions was "+ bankProfitOnSTD/numOfSTD+ "  for "+numOfSTD+" standard transactions");
                }
                listBox1.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                if (numOfCON != 0)
                {
                    listBox1.Items.Add("" + (bankProfitOnCON*100)/(bankProfit) + "%  of the profit comes from CON transations, witch is " + bankProfitOnCON);
                    listBox1.Items.Add("Arithmetic mean for STD transactions was " + bankProfitOnCON / numOfCON + "  for " + numOfCON + " standard transactions");
                }
                listBox1.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                this.Enabled = true;//ponieważ akcja moze zająć chwilę lepiej nie pozwolić na ponowne kliknięcie w trakcie wyliczania
            }
            preventColidingActions(false);
        }
       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseHover(object sender, EventArgs e)
        {
            var listBoxTooltip = new System.Windows.Forms.ToolTip();
            listBoxTooltip.SetToolTip(this.listBox1, "THIS BOX SHOWS INFORMATION, CLICK LEFT MOUSE BUTTON TWICE TO CLEAR THIS BOX");

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void showEarnings_MouseHover(object sender, EventArgs e)
        {
            var CALCULATEbANKpROFITSTooltip = new System.Windows.Forms.ToolTip();
            CALCULATEbANKpROFITSTooltip.SetToolTip(this.showEarnings , "CLICK THIS BUTTON TO SHOW HOW MUCH DID BANK EARN ON TRANSACTIONS LISTED ABOVE");
        }
    }
}
