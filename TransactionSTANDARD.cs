using System;
using System.Collections.Generic;
using System.Text;
using RadosławBoczońSystemBankowy;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace RadosławBoczońSystemBankowy
{
    public class TransactionSTANDARD : Transaction
    {
        public int numOfTransactionByday;
        public int numOfTransactionByWeek;
        public bool wasAuthenticated;
        public bool wasSuccesfull;//there will be method for chcecking if user has enought money, or if there exists another user on reciving end of transaction
        public int transactionCode;





        public override void Save(StreamWriter save)
        {
            save.WriteLine("-----STD-----");
            if (base.IsEmpty() == false)
            {
                save.WriteLine("----NOT-EMPTY----");
                base.Save(save);
                save.WriteLine(this.numOfTransactionByday);
                save.WriteLine(this.numOfTransactionByWeek);
                save.WriteLine(this.wasAuthenticated);
                save.WriteLine(this.wasSuccesfull);
                save.WriteLine(this.transactionCode);
            }
            else
                save.WriteLine("----EMPTY----");



            return;
        }
        public TransactionSTANDARD( StreamReader load) : base( load)
        {
            try
            {
                this.numOfTransactionByday = int.Parse(load.ReadLine());
                this.numOfTransactionByWeek = int.Parse(load.ReadLine());
                this.wasAuthenticated = bool.Parse(load.ReadLine());
                this.wasSuccesfull = bool.Parse(load.ReadLine());
                this.transactionCode = int.Parse(load.ReadLine());
            
            }
            catch(System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
            }


}
        public TransactionSTANDARD(ref Bitmap photo, int Day, int Week, ref Time theTime, BankClient theSender, ref Money theTransferM, BankClient theReciver) : base(ref photo, ref theTime, theSender, ref theTransferM, theReciver)
        {
            this.sender.addedTransaction();
            this.numOfTransactionByday = theSender.transactionsThisDay;
            this.numOfTransactionByWeek = theSender.transactionsThisWeek;

            this.wasAuthenticated = false;
            this.wasSuccesfull = false;
            this.transactionCode = 0;

        }
        public TransactionSTANDARD(TransactionSTANDARD instantToCopy) : base(instantToCopy)//copy constructor
        {
            this.sender.addedTransaction();//new transaction so we add one by using fucntio even if we will have to remove it 1 sec later
            this.numOfTransactionByday = instantToCopy.sender.transactionsThisDay;
            this.numOfTransactionByWeek = instantToCopy.sender.transactionsThisWeek;
            this.wasSuccesfull = instantToCopy.wasSuccesfull;
            this.wasAuthenticated = instantToCopy.wasAuthenticated;
            this.photo = instantToCopy.photo;
        }
        public TransactionSTANDARD() : base()//empty constructor
        {
            this.numOfTransactionByday = -1;//-1 special walue for empty
            this.numOfTransactionByWeek = -1;//----------||-------------
            this.wasAuthenticated = false;//because it was not authenticated
            this.wasSuccesfull = false;//same as above
        }
        public int GenerateAutCode()//on clic for new transaction
        {
            Random theCode = new Random();
           
           
            this.transactionCode = theCode.Next(1000, 10000);//generates authentication code
            return this.transactionCode;
        }
        ~TransactionSTANDARD()
        {

        }

        
        public override List<string> ToTXT()
        {
            var toTxt = base.ToTXT();

            toTxt.Add(this.wasAuthenticated.ToString());
            //toTxt.Add(this.wasSuccesfull.ToString()); nie ma na razie snsu
            toTxt.Add(this.transactionCode.ToString());


            return toTxt;
        }

        public void Authenticate(int passedCode)
        {
            
            //int.TryParse(code, out passedCode);
            if (passedCode == transactionCode)
                this.wasAuthenticated = true;
            else
                this.sender.removeTranactions();

        }

        public override ListViewItem CreateItem()
        {
            return CreateItemYN(false);
        }
    public override ListViewItem CreateItemYN(bool x)
        {
            ListViewItem itemForInstant = base.CreateItemYN(false);
            
            itemForInstant.SubItems.Add("" + this.numOfTransactionByday);
            
            itemForInstant.SubItems.Add("" + this.numOfTransactionByWeek);
            if (x == false)
            {
                if (this.wasAuthenticated == true)
                {
                    itemForInstant.SubItems.Add("Authenticated by code: " + transactionCode);
                }
                else
                {
                    itemForInstant.SubItems.Add("UN AUTHICATED");
                }
            }
            else
            {
                itemForInstant.SubItems.Add("" + this.wasAuthenticated);
                itemForInstant.SubItems.Add("" + this.transactionCode);
            }
            itemForInstant.SubItems.Add(""+wasSuccesfull);
            return itemForInstant;
        }
        public override void Write(ref ListView listView, ListViewItem item)
        {
            listView.Items.Add(item);

        }
        //--------------------- L6 ------------------
        /*
        public override ref Bitmap getPhoto()
        {

            return ref this.photo;

        }*/


        public override ListViewItem GetMyInfo()
        {
            ListViewItem myInfoZ7 = base.CreateItem();

            //itemForInstant.SubItems.Add("" + this.numOfTransactionByday);

            //itemForInstant.SubItems.Add("" + this.numOfTransactionByWeek);
            if (this.wasAuthenticated == true)
            {
                myInfoZ7.SubItems.Add("Authenticated by code: " + transactionCode);
            }
            else
            {
                myInfoZ7.SubItems.Add("UN AUTHICATED");
            }
            myInfoZ7.BackColor = Color.LightPink;

            //itemForInstant.SubItems.Add("Was sucesfull? " + wasSuccesfull);
            return  myInfoZ7;
        }

        public override string CheckType()
        {

            return "STD";
        }

        //---------------------- Poprawione ----------------------------
        public static bool operator ==(TransactionSTANDARD trans1, TransactionSTANDARD trans2)
        {
            
            if (trans1.transactionCode==trans2.transactionCode)
                return true;
            else
                return false;
        }
        public static bool operator !=(TransactionSTANDARD trans1, TransactionSTANDARD trans2)
        {
            if ( trans1.transactionCode != trans2.transactionCode)
                return true;
            else
                return false;
           
        }
        //-------------------------- Poprawione --------------------------
        public static TransactionSTANDARD operator ++(TransactionSTANDARD trans1)
        {
            if (trans1.CheckType() == "STD")
                return new TransactionSTANDARD(trans1);
            else
                return new TransactionSTANDARD();
        }

        //-------------------- zostało aby aplikacja się nie wysypywała ------------
         public static bool operator ==(TransactionSTANDARD trans1, Transaction trans2)
        {
            if (trans1.CheckType() == trans2.CheckType() )
                return true;
            else
                return false;
        }
        public static bool operator !=(TransactionSTANDARD trans1, Transaction trans2)
        {
            if (trans1.CheckType() == trans2.CheckType() )
                return false;
            else
                return true;
           
        }
        public override double MoneyEarnedOnTransaction()
        {
            
                if (this.transferMoney.amount > 0.0D)
                    return this.transferMoney.amount * 0.05D;
                else
                    return 1.50D;
            

        }


    }
}
