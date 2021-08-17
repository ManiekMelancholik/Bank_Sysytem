using System;
using System.Collections.Generic;
using System.Text;
using RadosławBoczońSystemBankowy;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RadosławBoczońSystemBankowy
{
    public abstract class Transaction: IComparable<Transaction>
    {
        protected Time timeOfTransaction;
        protected BankClient sender;
        protected Money transferMoney;
        protected BankClient reciver;
        protected Bitmap photo;

        
        public bool Search(string clientFirstName, string clientLastName)
        {
            bool fName = false;
            bool lName = false;
            bool a, b;
           
            if (a=clientFirstName != "")
            {
                if (this.sender.first_Name.Contains(clientFirstName))
                {
                    fName = true;
                }
            }
            if (b=clientLastName!="")
            {
                if(this.sender.last_Name.Contains(clientLastName))
                {
                    lName = true;
                }
            }
            if ((a == false && lName == true) || (b == false && fName == true) || (lName == true && fName == true))
            {
                return true;
            }
            else
            return false;
        }

        public bool Search(DateTime dateOfTransaction)
        {
            if (this.timeOfTransaction.day == dateOfTransaction.Day && this.timeOfTransaction.month == dateOfTransaction.Month && this.timeOfTransaction.year == dateOfTransaction.Year)
                return true;
            else
                return false;
        }
        public bool Search(DateTime beginingFrom, DateTime endingAt)
        {//jeżeli istnieje rozbierznośc pomiędzy datami to wyszukujemy wszystko za wykluczeniem przedziału
            int start = (365 * beginingFrom.Year) + (30 * beginingFrom.Month) + beginingFrom.Day,
                end = (365 * endingAt.Year) + (30 * endingAt.Month) + endingAt.Day,
                myTime = (365 * this.timeOfTransaction.year) + (30 * this.timeOfTransaction.month) + this.timeOfTransaction.day;

            if(end>start)
            {
                if (myTime >= start && myTime <= end)
                    return true;
                else
                    return false;

            }
            else
            {
                if (myTime > start || myTime < end)
                    return true;
                else
                    return false;
            }



            
        }






        public int CompareTo(Transaction trans)
        {
            //asc
            if (trans == null)
                return 1;

            if (String.Compare(this.sender.first_Name, trans.sender.first_Name) == -1)
                return -1;
            else if (String.Compare(this.sender.first_Name, trans.sender.first_Name) == 1)
                return 1;
            else
            {
                if (String.Compare(this.sender.last_Name, trans.sender.last_Name) == -1)
                    return -1;
                else if (String.Compare(this.sender.last_Name, trans.sender.last_Name) == 1)
                    return 1;
                else
                {
                    if (this.transferMoney.amount < trans.transferMoney.amount)
                        return 1;
                    else if (this.transferMoney.amount > trans.transferMoney.amount)
                        return -1;
                    else
                    {
                        if (this.timeOfTransaction.year < trans.timeOfTransaction.year)
                            return 1;
                        else if (this.timeOfTransaction.year > trans.timeOfTransaction.year)
                            return -1;
                        else
                        {
                            if (this.timeOfTransaction.month < trans.timeOfTransaction.month)
                                return 1;
                            else if (this.timeOfTransaction.month > trans.timeOfTransaction.month)
                                return -1;
                            else
                            {
                                if (this.timeOfTransaction.day < trans.timeOfTransaction.day)
                                    return 1;
                                else if (this.timeOfTransaction.day > trans.timeOfTransaction.day)
                                    return -1;
                                else
                                {

                                    if (this.timeOfTransaction.hour < trans.timeOfTransaction.hour)
                                        return 1;
                                    else if (this.timeOfTransaction.hour > trans.timeOfTransaction.hour)
                                        return -1;
                                    else
                                    {
                                        if (this.timeOfTransaction.minute < trans.timeOfTransaction.minute)
                                            return 1;
                                        else if (this.timeOfTransaction.minute > trans.timeOfTransaction.minute)
                                            return -1;
                                        else
                                            return 0;
                                    }
                                }

                            }

                        }

                    }

                }

            }
        }





        public virtual void Save(StreamWriter save)
        {
           
                save.WriteLine(this.timeOfTransaction.day + ":" + this.timeOfTransaction.month + ":" + this.timeOfTransaction.year + "." +
                this.timeOfTransaction.minute + "M" + this.timeOfTransaction.hour + "H");//time
                this.sender.Save(save);
                save.WriteLine(this.transferMoney.amount);
                save.WriteLine(this.transferMoney.currency);
                this.reciver.Save(save);
                if (this.photo != null)
                {
                    save.WriteLine("++YES++");
                    convertPhotho(save);
                }
                else
                {
                    save.WriteLine("++NO++");
                }
                //return;
            



        }

        public Transaction(StreamReader load)
        {
            try
            {
                //string y = load.ReadLine();
                this.timeOfTransaction = new Time();
                string y = load.ReadLine();
                this.timeOfTransaction.CreatingTime(y);//time
                this.sender = new BankClient(load);
                this.transferMoney = new Money();
                this.transferMoney.amount = int.Parse(load.ReadLine());
                this.transferMoney.currency = load.ReadLine();
                this.reciver = new BankClient(load);
                if (load.ReadLine() == "++YES++")
                    this.photo = new Bitmap(recoverPhoto(load));
            }
            catch (System.FormatException exeption)
            {
                MessageBox.Show(exeption.Message);
            }

        }
        public virtual List<string> ToTXT()
        {
            var toTxt = new List<string>();

            toTxt.Add(this.timeOfTransaction.day.ToString() + ':' + this.timeOfTransaction.month.ToString() + ':' + this.timeOfTransaction.year.ToString() + '.' + this.timeOfTransaction.minute.ToString() + 'M' + this.timeOfTransaction.hour.ToString() + 'H');
            toTxt.Add(this.transferMoney.amount.ToString());
            toTxt.Add(this.transferMoney.currency);
            toTxt.Add(this.sender.client_Number.ToString() + '|' + this.reciver.client_Number.ToString());
            return toTxt;
        }

        public void reducePhoto()
        {

            this.photo.ToString();
        }
        

        public Transaction(ref Bitmap selectedPhoto, ref Time theTime, BankClient theSender, ref Money theTransferM, BankClient theReciver)
        {
            //------------------------------  L6  ---------------------------
            //this.photo = new Bitmap(selectedPhoto); chciałem aby towżło tą bitmape dopiero w klasie ale jak to ma się wyśiwetlać w formatkach to wtedy działoby się to 2 razy więc chyba lepiej jak utwoży w formatce i podeśle do konstruktora samą bitmapę

            this.photo = selectedPhoto;

            //------------------------------ L6 ------------------------------



            this.reciver = theReciver;
            this.timeOfTransaction = theTime;
            this.transferMoney = theTransferM;
            this.sender = theSender;

        }
        public Transaction(Transaction transactionToCopy)//coppy
        {
            this.reciver = transactionToCopy.reciver;
            this.timeOfTransaction = transactionToCopy.timeOfTransaction;
            this.transferMoney = transactionToCopy.transferMoney;
            this.sender = transactionToCopy.sender;


        }


        public Transaction()//empty constructor
        {
            this.timeOfTransaction = new Time();
            this.timeOfTransaction.day = -1;
            this.timeOfTransaction.month = -1;
            this.timeOfTransaction.year = -1;
            this.timeOfTransaction.hour = -1;
            this.timeOfTransaction.minute = -1;

            sender = new BankClient();
            reciver = new BankClient();

            transferMoney = new Money();
            transferMoney.amount = 0.00D;
            transferMoney.currency = "IDK";


        }
        ~Transaction()
        {

        }
        public virtual ListViewItem CreateItem()
        {
            var item = CreateItemYN(false);
            return item;
        }
        public virtual ListViewItem CreateItemYN(bool x)
        {
            
            ListViewItem ITEM = new ListViewItem();
            try
            {
                ITEM.SubItems.Add(this.timeOfTransaction.day + ":" + this.timeOfTransaction.month + ":" + this.timeOfTransaction.year + ".");
                ITEM.SubItems.Add(this.timeOfTransaction.minute + "M" + this.timeOfTransaction.hour + "H");
                ITEM.SubItems.Add(this.sender.ClientAccounts[0].account_Number + this.sender.ClientAccounts[0].account_Type);
                ITEM.SubItems.Add(this.sender.first_Name);
                ITEM.SubItems.Add(this.sender.last_Name);
                ITEM.SubItems.Add(this.transferMoney.amount + this.transferMoney.currency);
                ITEM.SubItems.Add(this.reciver.ClientAccounts[0].account_Number + this.reciver.ClientAccounts[0].account_Type);
                ITEM.SubItems.Add(this.reciver.first_Name);
                ITEM.SubItems.Add(this.sender.last_Name);
                ITEM.SubItems.Add(this.reciver.ClientAccounts[0].AvilableCurrencies[0].amount + this.reciver.ClientAccounts[0].AvilableCurrencies[0].currency);
                ITEM.SubItems.Add((this.reciver.ClientAccounts[0].AvilableCurrencies[0].amount + this.transferMoney.amount) + this.reciver.ClientAccounts[0].AvilableCurrencies[0].currency);
            }
            catch (System.IndexOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        
            catch(System.ArgumentOutOfRangeException exeption)
            {
                MessageBox.Show(exeption.Message);
            }
            return ITEM;

        }
        public virtual void Write(ref ListView listView, ListViewItem item)
        {
            listView.Items.Add(item);

        }
        private void ForgetAndRedraw(ref ListView listView, List<Transaction> list)
        {
            listView.Items.Clear();
            list.RemoveAt(list.Count - 1);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Write(ref listView, CreateItem());
            }

        }
        public void ForgetLastTransaction(ref ListView listView, List<Transaction> list)
        {
            this.ForgetAndRedraw(ref listView, list);

        }
        public ref Bitmap getPhoto()
        {

            return ref this.photo;

        }
        public virtual ListViewItem GetMyInfo()
        {
            ListViewItem myInfoBaseZ7 = new ListViewItem();
            myInfoBaseZ7.SubItems.Add(this.timeOfTransaction.day + ":" + this.timeOfTransaction.month + ":" + this.timeOfTransaction.year + ".");
            myInfoBaseZ7.SubItems.Add(this.timeOfTransaction.minute + "M" + this.timeOfTransaction.hour + "H");
            myInfoBaseZ7.SubItems.Add(this.sender.ClientAccounts[0].account_Number + this.sender.ClientAccounts[0].account_Type);
            myInfoBaseZ7.SubItems.Add(this.sender.first_Name);
            myInfoBaseZ7.SubItems.Add(this.sender.last_Name);
            myInfoBaseZ7.SubItems.Add(this.transferMoney.amount + this.transferMoney.currency);
            myInfoBaseZ7.SubItems.Add(this.reciver.ClientAccounts[0].account_Number + this.reciver.ClientAccounts[0].account_Type);
            myInfoBaseZ7.SubItems.Add(this.reciver.first_Name);
            myInfoBaseZ7.SubItems.Add(this.sender.last_Name);
            myInfoBaseZ7.SubItems.Add(this.reciver.ClientAccounts[0].AvilableCurrencies[0].amount + this.reciver.ClientAccounts[0].AvilableCurrencies[0].currency);
            myInfoBaseZ7.SubItems.Add((this.reciver.ClientAccounts[0].AvilableCurrencies[0].amount + this.transferMoney.amount) + this.reciver.ClientAccounts[0].AvilableCurrencies[0].currency);
            return myInfoBaseZ7;
        }
        public abstract string CheckType();



        public void convertPhotho(StreamWriter save)
        {
            
                using (MemoryStream bitmapStream = new MemoryStream())
                {

                    this.photo.Save(bitmapStream, ImageFormat.Bmp);//do streama
                    save.WriteLine(Convert.ToBase64String(bitmapStream.ToArray())); // stream do tabliocy byte[]

                    bitmapStream.Dispose();
                }
                //return;
           
        }
        public bool IsEmpty()
        {
            if (this.timeOfTransaction.day > -1)
                return false;
            else
                return true;

        }
        public Bitmap recoverPhoto(StreamReader load)
        {

            using (MemoryStream recoverBitmap = new MemoryStream(Convert.FromBase64String(load.ReadLine())))
            {
                try
                {
                    var map = new Bitmap(recoverBitmap);
                    recoverBitmap.Dispose();
                    return map;
                }
                catch (System.FormatException exeption)
                {
                    MessageBox.Show(exeption.Message);
                    return null;
                }
            }


        }

        //kontekst funkcji
        //bank zarabia na każdej tranzakcji więc można policzyć ile bank zarobił na wszzystkich tranzakcjach kture są na liście
        // załóżmy że bank pobiera 5% od tranzakcji STD i 10% od CON
        // za każdą pustą tranzakcje bez kwotową stawka jest ustalona std 1.50 con 2.50


        public abstract double MoneyEarnedOnTransaction();


    }
}
