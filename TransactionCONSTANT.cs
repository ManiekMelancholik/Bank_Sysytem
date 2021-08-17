using System;
using System.Collections.Generic;
using System.Text;
using RadosławBoczońSystemBankowy;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace RadosławBoczońSystemBankowy
{
    public class TransactionCONSTANT : Transaction
    {
        bool reserveMoneyMonthly;
        string howOften;
        string titleOfConstTransaction;
        int numOfContTransactions;

        //--------------------------------- L6 -------------------------
        public List<Time> sixNextTransactionDates;
        //--------------------------------- L6 -------------------------

        //no authentication constant/cyclic transactions only to authorised clients
        public TransactionCONSTANT(ref Bitmap photo, string howOften, bool reserve, string title, ref Time theTime, BankClient theSender, ref Money theTransferM, BankClient theReciver) : base(ref photo,ref theTime, theSender, ref theTransferM, theReciver)
        {
            
            this.sender.addedConstantTransaction();
            //this.num_Of_Transaction_By_day = the_Sender.transactions_This_Day;
            //this.num_Of_Transaction_By_Week = the_Sender.transactions_This_Week;
            this.numOfContTransactions = theSender.contstantTransactions;
            this.reserveMoneyMonthly = reserve;
            this.howOften = howOften;
            this.titleOfConstTransaction = title;
            //this.was_Authenticated = false;
            //this.was_Succesfull = false;
            //this.transaction_Code = 0;
            this.sixNextTransactionDates = new List<Time>();
            this.sixNextTransactionDates.Add(this.CalculateNextTransactionDate(this.timeOfTransaction));//1
            for (int i = 0; i < 5; i++)
                this.sixNextTransactionDates.Add(this.CalculateNextTransactionDate(this.sixNextTransactionDates[i]));

        }
        public override List<string> ToTXT()
        {
            var toTxt = base.ToTXT();

            toTxt.Add(this.reserveMoneyMonthly.ToString());
            toTxt.Add(this.howOften);
            toTxt.Add(this.titleOfConstTransaction);
            

            return toTxt;
        }
        public override void Save(StreamWriter save)
        {
            
                save.WriteLine("-----CON-----");
                if (base.IsEmpty() == false)
                {
                    save.WriteLine("----NOT-EMPTY----");
                    base.Save(save);
                    save.WriteLine(this.reserveMoneyMonthly);
                    save.WriteLine(this.howOften);
                    save.WriteLine(this.titleOfConstTransaction);
                    save.WriteLine(this.numOfContTransactions);
                }
                else
                    save.WriteLine("----EMPTY----");


                //return;
           
        }
        public TransactionCONSTANT(StreamReader load) : base(load)
        {
            try { 
                this.reserveMoneyMonthly = bool.Parse(load.ReadLine());
                this.howOften = load.ReadLine();
                this.titleOfConstTransaction = load.ReadLine();
                this.numOfContTransactions = int.Parse(load.ReadLine());
            }
            catch (System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
            }

        }
        public TransactionCONSTANT(TransactionCONSTANT constToCopy) : base(constToCopy)//copy constructor-----//although should be there even posibility for that
        {
            //this.sender.added_Transaction();//new transaction so we add one by using fucntio even if we will have to remove it 1 sec later
            this.sender.addedConstantTransaction();
            //this.num_Of_Transaction_By_day = instant_To_Copy.sender.transactions_This_Day;
            //this.num_Of_Transaction_By_Week = instant_To_Copy.sender.transactions_This_Week;
            this.numOfContTransactions = constToCopy.sender.contstantTransactions;
            this.reserveMoneyMonthly = constToCopy.reserveMoneyMonthly;
            this.howOften = constToCopy.howOften;
            this.titleOfConstTransaction = constToCopy.titleOfConstTransaction;
            this.photo = constToCopy.photo;
            //this.was_Succesfull = instant_To_Copy.was_Succesfull;
            //this.was_Authenticated = instant_To_Copy.was_Authenticated;

            //--------------------------------- L6 -------------------------
            this.sixNextTransactionDates = constToCopy.sixNextTransactionDates;
            //--------------------------------- L6 -------------------------
        }
        public TransactionCONSTANT() : base()//empty constructor
        {
            //this.num_Of_Transaction_By_day = -1;//-1 special walue for empty
            //this.num_Of_Transaction_By_Week = -1;//----------||-------------
            //this.was_Authenticated = false;//because it was not authenticated
            //this.was_Succesfull = false;//same as above
            this.numOfContTransactions = -1;
            this.reserveMoneyMonthly = false;
            this.howOften = "NEVER";
            this.titleOfConstTransaction = "UNKNOWN";

            //--------------------------------- L6 -------------------------
            this.sixNextTransactionDates = new List<Time>();
            Time nextTransactionTime = new Time();
            nextTransactionTime.day = -1;
            nextTransactionTime.month = -1;
            nextTransactionTime.year = -1;
            nextTransactionTime.minute = -1;
            nextTransactionTime.hour = -1;
            for (int i = 0; i < 6; i++)
                this.sixNextTransactionDates.Add(nextTransactionTime);
            //--------------------------------- L6 -------------------------

        }

        ~TransactionCONSTANT()
        {

        }
       

        

        public override ListViewItem CreateItem()
        {
            return CreateItemYN(false);
        }
        public override ListViewItem CreateItemYN(bool x)
        {
            ListViewItem itemForInstant = base.CreateItemYN(false);
            itemForInstant.SubItems.Add("" + this.titleOfConstTransaction);
            itemForInstant.SubItems.Add("" + this.howOften);

            if (x == false)
                itemForInstant.SubItems.Add("Reserve is" + reserveMoneyMonthly);
            else
                itemForInstant.SubItems.Add("" + reserveMoneyMonthly);

            itemForInstant.SubItems.Add("" + this.numOfContTransactions);
            return itemForInstant;
        }
        public override void Write(ref ListView listView, ListViewItem item)
        {
            listView.Items.Add(item);

        }
        public string EvaluatePosibleFunds()
        {
            
                string evaluation = "";
                if (this.CalculateBalanceThisContTransactionThisMonth() + 1000 >= this.sender.ClientAccounts[0].AvilableCurrencies[0].amount && this.CalculateBalanceThisContTransactionThisMonth() < this.sender.ClientAccounts[0].AvilableCurrencies[0].amount)
                    evaluation = "You may not make it this month, after this tranactions you will have less than 1000PLN";
                else if (this.CalculateBalanceThisContTransactionThisMonth() + 1000 < this.sender.ClientAccounts[0].AvilableCurrencies[0].amount)
                    evaluation = "Yoy will do fine, you will have more than 1000 PLN left after transaction";
                else
                    evaluation = "You dont have enought Money for these transactions. Please add founds to your bank wallet";
                return evaluation;
            

        }
        public double CalculateBalanceThisContTransactionThisMonth()
        {
            
                double x = 1;
                //month = 30 days
                if (this.howOften == "Dayly")
                    x = 30D;
                else if (this.howOften == "Weekly")
                    x = 4D;
                else if (this.howOften == "Monthly")
                    x = 1D;
                else
                    x = 0D;



                return this.transferMoney.amount * x;
            
        }

        public Time CalculateNextTransactionDate(Time t)
        {
            try
            {
                switch (this.howOften)
                {
                    case "Dayly":
                        t.day++;
                        break;
                    case "Weekly":
                        t.day += 7;
                        break;
                    case "Monthly":
                        t.month++;
                        break;
                }

                if (t.day > 30)
                {
                    t.day = 1;
                    t.month++;
                }
                if (t.month > 12)
                {
                    t.month = 1;
                    t.year++;
                }

                return t;
            }
            catch (System.ArgumentException exeption)
            {
                MessageBox.Show(exeption.Message);
                var errorTime = new Time();
                errorTime.CreatingTime("-1:-1:-1:-1M-1H");
                return errorTime;
            }
        }
        /*
        public override ref Bitmap getPhoto()
        {

            return ref this.photo;

        }*/
        public override ListViewItem GetMyInfo()// czy da się i jak to zrobić poprzez referencję ???
        {
            ListViewItem myInfoZ7 = base.CreateItem();
            //itemForInstant.SubItems.Add("" + this.titleOfConstTransaction);
            myInfoZ7.SubItems.Add("" + this.howOften);
            //itemForInstant.SubItems.Add("Reserve is" + reserveMoneyMonthly);
            //itemForInstant.SubItems.Add("" + this.numOfContTransactions);
            return myInfoZ7;
        }

        public override string CheckType()
        {

            return "CON";
        }

        public static bool operator ==(TransactionCONSTANT trans1, TransactionCONSTANT trans2)
        {
            if (trans1.howOften == trans2.howOften && trans1.titleOfConstTransaction == trans2.titleOfConstTransaction)
                return true;
            else
                return false;
        }
        public static bool operator !=(TransactionCONSTANT trans1, TransactionCONSTANT trans2)
        {
            if (trans1.howOften != trans2.howOften && trans1.titleOfConstTransaction != trans2.titleOfConstTransaction)
                return true;
            else
                return false;

        }
        public override double MoneyEarnedOnTransaction()
        {
            
                if (this.transferMoney.amount > 0.0D)
                    return this.transferMoney.amount * 0.01D;
                else
                    return 2.5D;
            

        }



    }
   
}
       