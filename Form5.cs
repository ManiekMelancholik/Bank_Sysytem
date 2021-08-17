using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadosławBoczońSystemBankowy
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            MyInitializer();
        }
        private void MyInitializer()
        {
            
                int i = 0;//tutaj możnaby się dodatkowo zabespieczyć przed przeciążeniem inta, jako offset jest uznawany również int ale bez wartości ujemnych,
                //wtedy należałoby dodać jeszcze jedne int i sprawdzać czy różnią się o jeden wtedy byłaby pewność że nie wyskoczy nam out of range exeption
                if (Form1.fullListOfTransactions.Count > 0)
                    while (i < Form1.fullListOfTransactions.Count)
                    {

                        ListViewItem sadBackwardsIsDasAndDasNichtGud = Form1.fullListOfTransactions[i].GetMyInfo();
                        sadBackwardsIsDasAndDasNichtGud.SubItems.Add("#  " + (i + 1));
                        //int lenght = sadBackwardsIsDasAndDasNichtGud.SubItems.Count;
                        //ListViewItem sadBackwardsIsDasAndDasNichtGudCOPY = new ListViewItem();
                        //sadBackwardsIsDasAndDasNichtGudCOPY.SubItems.Add("" + (i + 1));


                        ////sadBackwardsIsDasAndDasNichtGud.SubItems.Add("");
                        //for (int h = 0; h < lenght; h++)
                        // sadBackwardsIsDasAndDasNichtGud.SubItems[h] = sadBackwardsIsDasAndDasNichtGud.SubItems[h+1];
                        // sadBackwardsIsDasAndDasNichtGud.SubItems[0].Text=""+(i+1);
                        listView1.Items.Add(sadBackwardsIsDasAndDasNichtGud);
                        if (sadBackwardsIsDasAndDasNichtGud.BackColor == Color.LightPink)
                        {
                            comboBox1.Items.Add("" + (i + 1));
                            button1.Enabled = true;
                        }
                        i++;
                    }
                else
                    button1.Enabled = false;
                //combo boxes
                for (int j = 1; j <= i; j++)
                {

                    comboBox2.Items.Add("" + j);
                }

                comboBox3.Items.Add("==");
                comboBox3.Items.Add("!=");
                comboBox3.Items.Add("++");


            
        }
        public void MyInitUpdate()
        {
            
                {
                    int i = Form1.fullListOfTransactions.Count;
                    comboBox1.Items.Add("" + i);
                    comboBox2.Items.Add("" + i);
                    i--;
                    ListViewItem sadBackwardsIsDasAndDasNichtGud = Form1.fullListOfTransactions[i].GetMyInfo();
                    sadBackwardsIsDasAndDasNichtGud.SubItems.Add("#  " + (i + 1));
                    listView1.Items.Add(sadBackwardsIsDasAndDasNichtGud);

                }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = comboBox1.SelectedIndex;
            //listBox1.Items.Add("|"+comboBox1.SelectedText+"|");
            //string y = comboBox1.SelectedItem.ToString();
            //int x = Int32.Parse(y)-1;
            //listBox1.Items.Add("|"+ y +"|");

            switch (comboBox3.SelectedIndex)
            {
                
                case 0:
                    {

                        

                            //*
                            //int x = Int32.Parse(comboBox1.SelectedText);

                            if (comboBox3.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
                            {
                                Transaction trans2 = Form1.fullListOfTransactions[comboBox2.SelectedIndex];

                                TransactionSTANDARD trans1 = new TransactionSTANDARD();
                                if (Form1.fullListOfTransactions[comboBox1.SelectedIndex].CheckType() == "STD")//w razie czego
                                {
                                    trans1 = Form1.listOfSTANDARD[x];
                                }
                                else
                                    break;



                                if (trans1 == trans2)
                                {

                                    listBox1.Items.Add("--------------------------------");
                                    listBox1.Items.Add("New action (==)");
                                    listBox1.Items.Add(trans1.CheckType() + " == " + trans2.CheckType());
                                    listBox1.Items.Add("result = True");

                                }
                                else
                                {

                                    listBox1.Items.Add("--------------------------------");
                                    listBox1.Items.Add("New action (==)");
                                    listBox1.Items.Add(trans1.CheckType() + " == " + trans2.CheckType());
                                    listBox1.Items.Add("result = False");

                                }

                            }
                            else
                                break;
                            //*/
                        

                    }
                    break;
                case 1:
                    {
                       
                            if (comboBox3.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
                            {
                                // int x = Int32.Parse(comboBox1.SelectedText);
                                Transaction trans2 = Form1.fullListOfTransactions[comboBox2.SelectedIndex];
                                TransactionSTANDARD trans1 = new TransactionSTANDARD();
                                if (Form1.fullListOfTransactions[comboBox1.SelectedIndex].CheckType() == "STD")//w razie czego
                                {
                                    trans1 = Form1.listOfSTANDARD[x];
                                }
                                else
                                    break;



                                if (trans1 != trans2)
                                {
                                    listBox1.Items.Add("--------------------------------");
                                    listBox1.Items.Add("New action (!=)");
                                    listBox1.Items.Add(trans1.CheckType() + " == " + trans2.CheckType());
                                    listBox1.Items.Add("result = True");
                                }
                                else
                                {
                                    listBox1.Items.Add("--------------------------------");
                                    listBox1.Items.Add("New action (!=)");
                                    listBox1.Items.Add(trans1.CheckType() + " == " + trans2.CheckType());
                                    listBox1.Items.Add("result = False");

                                }
                            }
                            else
                                break;
                        
                    }
                    break;
                case 2:

                    {
                        
                            if (comboBox3.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
                            {
                                TransactionSTANDARD trans1 = new TransactionSTANDARD();
                                if (Form1.fullListOfTransactions[comboBox1.SelectedIndex].CheckType() == "STD")//w razie czego
                                {
                                    trans1 = Form1.listOfSTANDARD[x];
                                }
                                else
                                    break;
                                TransactionSTANDARD transactionPlusPlus = trans1++;
                                Form1.fullListOfTransactions.Add(transactionPlusPlus);
                                Form1.listOfSTANDARD.Add(transactionPlusPlus);
                                MyInitUpdate();
                                listBox1.Items.Add("--------------------------------");
                                listBox1.Items.Add("Copied transaction of index " + comboBox1.SelectedText);


                            }
                            else
                                break;
                        
                    }
                    break;


            }

            
        }
    }
}
