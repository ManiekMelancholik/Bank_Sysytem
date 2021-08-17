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
    //===============================LISTA 8 ========================
    /*w formatce 5 rzowiązanie
     * w klasie TransactionSTANDARD dodano 3 przeciążena == oraz != porónują czy tranzakce są tego samego typu 
     * ++ kopiuje wybraną tranzakcję
     * 
     * w poleceniui nie było nic o tym aby dodwać przeciazenia dla drugiej klasy więc zabezpieczyłem comboBox1 w Form5 poprzez umieszczenietylko indeksów transactionSTANDARD,
     *
     *
     
     
     
     */
    //+++++++++++++++++++++++++++++LISTA 7++++++++++++++++++++++++++
    //Rozwiązanie znajduje sięw  Form 4 
    //Dodano funkcje inicjalizujące, uruchamiaja się na startupie formatki
    //nowa kalsa AbstClassTransaction - aby nie przepisywać i nie zmieniać od teraz klasa bazowa transaction dziedziczy po clasie abstarkcynej
    //nowa lista zwykła dla obiektów klasy abstrakcyjnej fullListOfTransactions
    //pozatym wszystko do wybierania określonej tranzakcji i wyświetlania w formatce 4,
    //osobne listy dla tranzakcji Const oraz Standard nadal istnieją aby nie tracić pewnych możliwości(może to przydać się później)




    //--------------------------NOWE-----------------------------------------

    //-----------------------------------------------------------------------------------------------------------
    //Najważniejszy do oceny obecnego zaadania to pliki Transaction_CONST.cs oraz Transaction_STANDARD.cs
    // są tam konstruktory oraz funkcje.
    // w Form2 oraz Form3 są wywoływane funkcje oraz twożone obiekty, oraz są wypisaywane w lisrView, są zapisywane w specyficznych dlasiebie listach w Form1
    //------- wyłączyłem z widoku obiekty z Form1 ale nie usuwałem ich, z powodów trechnicznych jak by kiedyś mogły się przydać.----------
    //struckty oraz klasy dla klijentów i kont bankowych są w BankClient+Structs.cs 
    //------------------------------------------------------------------------------------------------------------

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyInitializer();
        }


        public static List<BankClient> listOfClients = new List<BankClient>();

        public static List<Transaction> listOfTransactions = new List<Transaction>();

        public static List<TransactionSTANDARD> listOfSTANDARD = new List<TransactionSTANDARD>();//jak to zrobić przez referencje
        public static List<TransactionCONSTANT> listOfCONSTANT = new List<TransactionCONSTANT>();
        public static List<Transaction> fullListOfTransactions = new List<Transaction>();


        private void button2_Click(object sender, EventArgs e)
        {
            FormLista12 search = new FormLista12();

            if (search.ShowDialog() == DialogResult.OK)
            {
               
            }
            search.Dispose();

        }

        public void DispElement(informationFOrShow typeAndNumber)
        {
            

                switch (typeAndNumber.type_Of_Transaction)
                {
                    case "STANDARD":
                        listOfSTANDARD[typeAndNumber.numberOfElement].Write(ref listView1, listOfSTANDARD[typeAndNumber.numberOfElement].CreateItem());

                        break;


                    case "CONSTANT":
                        listOfCONSTANT[typeAndNumber.numberOfElement].Write(ref listView1, listOfCONSTANT[typeAndNumber.numberOfElement].CreateItem());

                        break;

                        //in case ;) there might be more 

                };
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        List<string> Names = new List<string>(new string[] { "Adam", "Viktor", "Dawid", "Simone", "Angelina", "Antonina" });
        List<string> Surrnames = new List<string>(new string[] { "Sariff", "Jensen", "Sobieski", "Heszen", "Kowalczyk" });
        Random rng = new Random();

        private void MyInitializer()
        {
            button7.Enabled = true;
            button8.Enabled = true;

            button3.Enabled = false;
            button4.Enabled = true;
            button3.Visible = true;
            button3.Enabled = true;
            //button3.BackColor = default;
            
            int randNumber = rng.Next(100, 1000000);
            double rnd = Convert.ToDouble(randNumber);
            rnd /= 100;
            randNumber = rng.Next(1000000000, 1199999999);
            
            //kreator klijentów

            // BankClient sarif = new BankClient("Dawid", "Sariff", listOfClients.Count, rnd, "PLN", randNumber);
            //listOfClients.Add(sarif);
            //listOfClients[0].DisplayClient(listOfClients[0], ref listBox1);


            randNumber = rng.Next(-100, 1000000);
            rnd = Convert.ToDouble(randNumber);
            rnd /= 100;
            randNumber = rng.Next(1000000000, 1199999999);

            for (int i = 0; i < 7; i++)
            {
                BankClient Client = new BankClient(Names[rng.Next(0, 6)], Surrnames[rng.Next(0, 5)], listOfClients.Count, rnd, "PLN", randNumber);
                //listOfClients.Add(Client);
                listBox1.Items.Add("++Client Added++");
                listOfClients.Add(Client);
                listBox2.Items.Add("" + (i + 1));
             
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {


            /*
            button5.Enabled = true;
            Time theTime = new Time();
            Random rng = new Random();
            int randNumber = rng.Next(100, 1000000);
            double rnd = Convert.ToDouble(randNumber);


            theTime.day = rng.Next(0,30);
            theTime.month = rng.Next(0, 13);
            theTime.year = rng.Next(1990, 2020);
            theTime.hour = rng.Next(1, 25);
            theTime.minute = rng.Next(0, 61);
            Money theMoney = new Money();
            theMoney.amount = rnd;
            theMoney.currency = "PLN";

            
            Transaction theTransaction1 = new Transaction(ref theTime, listOfClients[0], ref theMoney, listOfClients[1]);
            Transaction theTransaction2 = new Transaction(ref theTime, listOfClients[1], ref theMoney, listOfClients[0]);
            if (rng.Next(0, 2) == 0)
            {
                theTransaction1.Write(ref listView1, listOfTransactions[listOfTransactions.Count() - 1].CreateItem());
                listOfTransactions.Add(theTransaction1);
            }
            else
            {
                theTransaction2.Write(ref listView1, listOfTransactions[listOfTransactions.Count() - 1].CreateItem());
                listOfTransactions.Add(theTransaction2);
            }
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Transaction copiedTransaction = new Transaction(listOfTransactions[listOfTransactions.Count() - 1]);
            //listOfTransactions.Add(copiedTransaction);
            //copiedTransaction.Write(ref listView1, listOfTransactions[listOfTransactions.Count() - 1].CreateItem());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listOfTransactions[listOfTransactions.Count() - 1].ForgetLastTransaction(ref listView1, listOfTransactions);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
                Form2 INSTANT = new Form2();

                if (INSTANT.ShowDialog() == DialogResult.OK)
                {
                    DispElement(INSTANT.GetNeededInformation());
                }
                INSTANT.Dispose();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
                Form3 CONSTANT = new Form3();

                if (CONSTANT.ShowDialog() == DialogResult.OK)
                {
                    DispElement(CONSTANT.GetNeededInformation());
                }
                CONSTANT.Dispose();
            
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
                Form4 Show = new Form4();

                if (Show.ShowDialog() == DialogResult.OK)
                {

                }
                Show.Dispose();
            
        }
        // no longer needed 
        private void button3_Click(object sender, EventArgs e)
        {
           
                Form5 Show = new Form5();

                if (Show.ShowDialog() == DialogResult.OK)
                {

                }
                Show.Dispose();
            

            { /*
            button7.Enabled = true;
            button8.Enabled = true;

            button3.Enabled = false;
            button4.Enabled = true;
            button3.BackColor = default;
            Random rng = new Random();
            int randNumber = rng.Next(100, 1000000);
            double rnd = Convert.ToDouble(randNumber);
            rnd /= 100;
            randNumber = rng.Next(1000000000, 1199999999);

            //kreator klijentów

            BankClient sarif = new BankClient("Dawid", "Sariff", listOfClients.Count, rnd, "PLN", randNumber);
            listOfClients.Add(sarif);
            listOfClients[0].DisplayClient(listOfClients[0], ref listBox1);


            randNumber = rng.Next(-100, 1000000);
            rnd = Convert.ToDouble(randNumber);
            rnd /= 100;
            randNumber = rng.Next(1000000000, 1199999999);


            BankClient jensen = new BankClient("Adam", "Jensen", listOfClients.Count, rnd, "PLN", randNumber);
            listOfClients.Add(jensen);
            listOfClients[0].DisplayClient(listOfClients[1], ref listBox2);
            */
            }
        }
    }
}
