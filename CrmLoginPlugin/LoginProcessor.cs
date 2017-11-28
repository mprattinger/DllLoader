using System;
using System.Collections.Generic;
using System.Text;

namespace CrmLoginPlugin
{
    public enum LoginUserMode
    {
        USER,
        COUNTRY
    }
    public class LoginProcessor
    {
        public LoginUserMode LoginMode { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
        public string System { get; set; }

        public LoginProcessor()
        {
            //Daten laden
        }

        public void InitializeLogin(string land_user, string system)
        {
            if (land_user.Length == 2)
            {
                LoginMode = LoginUserMode.COUNTRY;
                Country = land_user;
            }
            else
            {
                LoginMode = LoginUserMode.USER;
                Username = land_user;
            }
            System = system;
        }

        public void AddLogin(string[] args)
        {
            throw new NotImplementedException();
        }

        public void ListLogins()
        {
            throw new NotImplementedException();
        }

        public void Login(string[] args)
        {
            var system = args[0];
            var land_user = args[1];
            if (system == "") system = askSystem();
            if (land_user == "") land_user = askLandUser();
            if (String.IsNullOrEmpty(system) && String.IsNullOrEmpty(land_user)) throw new WrongParamsException(land_user, system);
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        private string askLandUser()
        {
            Console.Write("Do you want to use country or user (c for country/u for user)?:");
            var antw = Console.ReadLine();
            if (antw == "c")
            {
                Console.Write("Please enter the country shortname (AT for Austria, DE for Germany,...)?:");
                return Console.ReadLine();
            }
            else
            {
                Console.Write("Please enter the username?:");
                return Console.ReadLine();
            }
        }

        private string askSystem()
        {
            Console.Write("Please enter the system (WCD for CRM Development, WCP for CRM Productive,...)?:");
            return Console.ReadLine();
        }
    }
}
