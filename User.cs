using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Web;

namespace CustomerAccounter
{
    internal abstract class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    internal class Admin:User
    {
        private static Admin instance;
        public string Name { get; set; }
        public string Surname { get; set; }
        //private Admin()
        //{
        //    Login = login;
        //    Password = pass;
        //    Name = name;
        //    Surname = surname;

        //}

        public static Admin GetInstance()
        {
            if (instance == null)
            {
                instance = new Admin();
            }
            return instance;
        }
    }

    internal abstract class Client:User
    {
        public int Code { get; set; }
        public int phoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
    }
    
    internal class Fiziki:Client
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        //public Fiziki(string name, string surname)
        //{
        //    Name = name;
        //    Surname = surname;
        //}

        //public Fiziki(string name, string surname, int code)
        //{
        //    Name = name;
        //    Surname = surname;
        //    Code = code;
        //}
        //public Fiziki(string name, string surname, int code, int phoneNumber, string adress)
        //{
        //    Name = name;
        //    Surname = surname;
        //    Code = code;
        //    this.phoneNumber = phoneNumber;
        //    Adress = adress;
        //}
    }

    internal class FOP:Fiziki
    {
        public decimal BankAccount;
        public int MFO;
        public Bank bank;
        
        //public FOP (string name, string surname, int bankAccount, int mfo, Bank bank):base(name, surname)
        //{
        //    BankAccount= bankAccount;
        //    MFO= mfo;
        //    this.bank = bank;
        //}
    }

    enum UrikiForm 
    {
        [Description("Товариство з обмеженою відповідальністю")] TOV,
        [Description ("Товариство з додатковою відповідальністю")] TDV,
        [Description("Приватне підприємство")] PP,
        [Description("Селянське фермерське господарство")] CFG,
        [Description ("Житлово-будівельний кооператив")] ZBK,
        [Description("Об'єднання співвласників багатоквартирного будинку")] OSBB
    }

    enum Bank 
    {
        [Description ("Акціонерне товариство \"Комерційний банк \"ПриватБанк\"")] PB,
        [Description ("Акціонерне товариство \"РайффайзенБанк\"")] PF,
        [Description("Акціонерне товариство \"УніверсалБанк\"")] UB,
        [Description("\"MonoBank\"")] Mono
    }
    internal class Uriki:Client
    {
        UrikiForm Form;
        public string? Name { get; set; }
        public int BankAccount;
        public int MFO;
        Bank bank;

        public Uriki(UrikiForm form, string name, int bankAccount, int mfo, Bank bank)
        {
            Form= form;
            Name= name;
            BankAccount= bankAccount;
            MFO = mfo;
            this.bank= bank;
        }
    }
}
