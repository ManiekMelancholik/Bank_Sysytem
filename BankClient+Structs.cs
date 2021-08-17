using System;
using System.Collections.Generic;
using System.Text;
using RadosławBoczońSystemBankowy;
using System.Windows.Forms;
using System.IO;

namespace RadosławBoczońSystemBankowy
{
    public struct informationFOrShow
    {
        public string type_Of_Transaction;
        public int numberOfElement;
    }
    public struct Money
    {
       
        public string currency;
        public double amount;
    }
    public struct Time//aby upożądkować dane związane z przelewem, data + czas
    {
        public int day;
        public int month;
        public int year;
        public int hour;
        public int minute;
        public void CreatingTime(string x)
        {
            try
            {
                List<int> y = new List<int>();
                int j = 0;//położenie ostatniego znaku przerywającego

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] == ':' || x[i] == '.' || x[i] == 'H' || x[i] == 'M')
                    {
                        y.Add(int.Parse(x.Substring(j, i - j)));
                        j = i + 1;//omijamy :

                    }
                    //kolejnośc w pliku to :
                    //16:5:2021.
                    //16M1H


                }//================================================
                this.day = y[0];
                this.month = y[1];
                this.year = y[2];
                this.minute = y[3];
                this.hour = y[4];
            }
            catch (System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
                this.day = -1;
                this.month = -1;
                this.year = -1;
                this.minute = -1;
                this.hour = -1;
                //return;
                
            }
            catch (System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
                this.day = -1;
                this.month = -1;
                this.year = -1;
                this.minute = -1;
                this.hour = -1;
                //return;
            }
            




        }


        public string TimeToString()
        {
            string retString = "";
           




            return retString;
        }

    }
    public class ClientAccount
    {
        public int client_Number;
        public string account_Type;
        public List<Money> AvilableCurrencies;
        public int account_Number;
        

        public ClientAccount(int acc_cl_Number, List<Money> avCurr,int acc_Number)//standard account
        {
            account_Type = "STANDARD";
            client_Number = acc_cl_Number;
            account_Number = acc_Number;
            AvilableCurrencies = avCurr;
            
        }
        
        public void Save(StreamWriter save)
        {
            
                save.WriteLine("+++BANK_ACCOUNT+++");
                save.WriteLine(this.account_Number);
                save.WriteLine(this.account_Type);
                save.WriteLine(this.client_Number);
                foreach (Money m in AvilableCurrencies)
                {
                    save.WriteLine("++Currency++");
                    save.WriteLine(m.amount);
                    save.WriteLine(m.currency);
                }
                save.WriteLine("++++++++++++++");
                //return;
            

        }

        public ClientAccount ( StreamReader load)
        {
            try
            {
                this.AvilableCurrencies = new List<Money>();

                this.account_Number = int.Parse(load.ReadLine());
                this.account_Type = load.ReadLine();
                this.client_Number = int.Parse(load.ReadLine());
                //int s;
                //string v;
                while (load.ReadLine() == "++Currency++")
                {
                    Money curr = new Money();
                    //v = load.ReadLine();
                    //s = int.Parse(v);
                    curr.amount = double.Parse(load.ReadLine());
                    curr.currency = load.ReadLine();
                    this.AvilableCurrencies.Add(curr);
                }
            
            }
            catch(System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
            }

}

    }
    public class BankClient
    {
        public string first_Name;
        public string last_Name;
        public int client_Number;
        public List<ClientAccount> ClientAccounts;

        public int transactionsThisDay;
        public int transactionsThisWeek;

        public int contstantTransactions;


        public void Save(StreamWriter save)
        {
            
                save.WriteLine("++++Bank_Client++++");
                save.WriteLine(this.client_Number);
                save.WriteLine(this.contstantTransactions);
                save.WriteLine(this.first_Name);
                save.WriteLine(this.last_Name);
                save.WriteLine(this.transactionsThisDay);
                save.WriteLine(this.transactionsThisWeek);
                foreach (ClientAccount ca in ClientAccounts)
                    ca.Save(save);

                save.WriteLine("++++++++++++++++");
                //return;
            

        }
        public BankClient( StreamReader load)
        {
            try
            {

                load.ReadLine();
                this.ClientAccounts = new List<ClientAccount>();

                this.client_Number = int.Parse(load.ReadLine());
                this.contstantTransactions = int.Parse(load.ReadLine());
                this.first_Name = load.ReadLine();
                this.last_Name = load.ReadLine();
                this.transactionsThisDay = int.Parse(load.ReadLine());
                this.transactionsThisWeek = int.Parse(load.ReadLine());
                while (load.ReadLine() == "+++BANK_ACCOUNT+++")
                {
                    ClientAccount ca = new ClientAccount(load);
                    ClientAccounts.Add(ca);
                }
            }
            catch(System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
            }
           



        }
        
        public BankClient(string f_Name, string l_Name, int cl_Num, double balance, string currency, int acc_Num)
        {
            first_Name = f_Name;
            last_Name = l_Name;
            client_Number = cl_Num;

            this.transactionsThisDay = 0;
            this.transactionsThisWeek = 0;
            this.contstantTransactions = 0;
            //od tąd twoży się lista na której są poukłądane konta, które na włąsnych listach zawierają rachunki walutowe
            List<ClientAccount> ClientAccounts = new List<ClientAccount>();
            //int acc_Number = ClientAccounts.Count;

            Money clientMoney = new Money();//struct money
            clientMoney.currency = currency;
            clientMoney.amount = balance;

            List<Money> currencies = new List<Money>();
            currencies.Add(clientMoney);//lista wymagana przez klasę konto klijenta
            //----------------------------------------------------------------------------------------------------------------
            ClientAccount account = new ClientAccount(cl_Num, currencies, acc_Num);// konstruktor konta klijenta
            
            this.ClientAccounts = new List<ClientAccount>();//stwożenie listy kont i dodanie konta do listy kont klijenta
            this.ClientAccounts.Add(account);
        }
        public BankClient()
        {
            first_Name = "UNKNOWN";
            last_Name = "ALSO UNKNOWN";
            client_Number = 0;
            this.transactionsThisDay = -1;
            this.transactionsThisWeek = -1;
            this.contstantTransactions = -1;
            //od tąd twoży się lista na której są poukłądane konta, które na włąsnych listach zawierają rachunki walutowe
            List<ClientAccount> ClientAccounts = new List<ClientAccount>();
            //int acc_Number = ClientAccounts.Count;

            Money clientMoney = new Money();//struct money
            clientMoney.currency = "IDK";
            clientMoney.amount = 0.01D;

            List<Money> currencies = new List<Money>();
            currencies.Add(clientMoney);//lista wymagana przez klasę konto klijenta
            //----------------------------------------------------------------------------------------------------------------
            ClientAccount account = new ClientAccount(0, currencies, 0);// konstruktor konta klijenta

            this.ClientAccounts = new List<ClientAccount>();//stwożenie listy kont i dodanie konta do listy kont klijenta
            this.ClientAccounts.Add(account);
        }
        ~BankClient()
        {

        }
        public void addedTransaction()
        {
            this.transactionsThisDay++;
            this.transactionsThisWeek++;
        }
        public void removeTranactions()
        {
            this.transactionsThisDay--;
            this.transactionsThisWeek--;
           
        }
        public void addedConstantTransaction()
        {
            this.contstantTransactions++;
        }
        public void remove_Constant_Transaction()
        {
            this.contstantTransactions--;
        }
        public void zero_Day_Transactions()
        {
            this.transactionsThisDay = 0;
        }
        public void zero_Week_Transactions()
        {
            this.transactionsThisWeek = 0;
        }



        public void DisplayClient( BankClient selected_Client, ref ListBox listBox)
        {
            
            listBox.Items.Add("Firs Name  " + selected_Client.first_Name);
            listBox.Items.Add("Last Name  " + selected_Client.last_Name);
            listBox.Items.Add("Account Number  " + selected_Client.ClientAccounts[0].account_Number);
            listBox.Items.Add("Account Type  " + selected_Client.ClientAccounts[0].account_Type);
            listBox.Items.Add("Currency Type  " + selected_Client.ClientAccounts[0].AvilableCurrencies[0].currency);
            listBox.Items.Add("Balance   " + selected_Client.ClientAccounts[0].AvilableCurrencies[0].amount + "  " + selected_Client.ClientAccounts[0].AvilableCurrencies[0].currency);

        }

    }
}
