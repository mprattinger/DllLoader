using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmLoginPlugin
{
    public enum LoginUserMode
    {
        USER,
        COUNTRY
    }
    public enum ProcessorMode
    {
        DELETE,
        ADD,
        LIST,
        DELETEALL,
        LOGIN
    }
    public class LoginProcessor
    {
        const string fileName = "CRMLogin.db";

        public List<LoginDataModel> Logins { get; set; }
        public LoginUserMode LoginMode { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
        public string Sys { get; set; }
        public ProcessorMode ProcessorMode { get; set; }

        public LoginProcessor()
        {
            //Daten laden
            //Wenn es kein File gibt, dann eines anlegen
            if (!System.IO.File.Exists(fileName))
            {
                var sw = System.IO.File.CreateText(fileName);
                sw.Flush();
                sw.Close();
                sw.Close();
            }
            using (var sr = new StreamReader(fileName))
            {
                string json = sr.ReadToEnd();
                Logins = JsonConvert.DeserializeObject<List<LoginDataModel>>(json);
                sr.Close();
            }
            if (Logins == null) Logins = new List<LoginDataModel>();
        }

        public async Task DeleteLogin(string[] args)
        {
            if (Logins.Count == 0) return;

            ProcessorMode = ProcessorMode.DELETE;

            loadUser(args);
            if (String.IsNullOrEmpty(Username))
            {
                Console.WriteLine("Please declare a username to delete the login!");
                return;
            }

            loadSystem(args);
            if (String.IsNullOrEmpty(Sys))
            {
                Console.WriteLine("Please declare a system to delete the login!");
                return;
            }

            var login2Delete = Logins.Where(l => l.Sys.ToLower() == Sys.ToLower() && l.Username.ToLower() == Username.ToLower()).FirstOrDefault();
            if (login2Delete == null)
            {
                Console.Write("\n\tNo login to delete for this parameters!\n\tUse -l/--list to show all logins!\n");
                return;
            }

            Logins.Remove(login2Delete);

            Console.WriteLine($"\nDo you want to delete this login {Username}@{Sys}? (y/n): ");
            var answer = Console.ReadKey();
            if (answer.Key != ConsoleKey.Y) return;
            if (await saveData()) Console.WriteLine("\n\nLogin deleted!");
        }

        public async Task AddLogin(string[] args)
        {
            ProcessorMode = ProcessorMode.ADD;

            loadUser(args);
            if (String.IsNullOrEmpty(Username))
            {
                Console.WriteLine("Please declare a username to add the login!");
                return;
            }

            loadSystem(args);
            if (String.IsNullOrEmpty(Sys))
            {
                Console.WriteLine("Please declare a system to add the login!");
                return;
            }

            loadCountry(args);
            if (String.IsNullOrEmpty(Country))
            {
                Console.WriteLine("Please declare a country to add the login!");
                return;
            }

            var pw = loadPassword(args);
            if (String.IsNullOrEmpty(Country))
            {
                Console.WriteLine("Please declare a password to add the login!");
                return;
            }

            var model = new LoginDataModel { Country = Country, Sys = Sys, Username = Username, Password = pw };
            Logins.Add(model);
            if (await saveData()) Console.WriteLine("Login added!");
        }

        public void ListLogins()
        {
            ProcessorMode = ProcessorMode.LIST;

            Console.WriteLine();
            Console.WriteLine("Availiable logins:");
            Console.WriteLine();
            var title = "\tSystem\tCountry\tUsername";
            Console.WriteLine(title);
            var line = String.Empty;
            for (var x = 0; x < title.Length; x++)
            {
                line += "*";
            }
            Console.WriteLine($"\t{line}");
            Logins.ForEach(l =>
            {
                Console.WriteLine($"\t{l.Sys}\t{l.Country}\t{l.Username}");
            });
        }

        public async Task RemoveAll()
        {
            ProcessorMode = ProcessorMode.DELETEALL;

            Console.WriteLine("\nDo you want to delete all logins? (y/n): ");
            var answer = Console.ReadKey();
            if (answer.Key != ConsoleKey.Y) return;

            var fi = new System.IO.FileInfo(fileName);
            if (fi.Exists)
            {
                await Task.Factory.StartNew(() => fi.Delete());
            }
            Console.WriteLine("\n\nLogin deleted!");
        }

        public void Login(string[] args)
        {
            ProcessorMode = ProcessorMode.LOGIN;
        }

        private async Task<bool> saveData()
        {
            try
            {
                var fi = new System.IO.FileInfo(fileName);
                if (fi.Exists)
                {
                    await Task.Factory.StartNew(() => fi.Delete());
                }
                using (var sw = System.IO.File.CreateText(fileName))
                {
                    var data = JsonConvert.SerializeObject(Logins);
                    await sw.WriteAsync(data);
                    sw.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private void loadSystem(string[] args)
        {
            var system = String.Empty;
            try
            {
                system = args[0];
            }
            catch (Exception) { }

            if (String.IsNullOrEmpty(system)) system = askSystem();
            Sys = system.ToUpper();
        }

        private string askSystem()
        {
            Console.Write("Please enter the system (WCD for CRM Development, WCP for CRM Productive,...)?: ");
            return Console.ReadLine();
        }

        private string loadCountry(string[] args)
        {
            var user = String.Empty;
            try
            {
                user = args[1];
            }
            catch (Exception) { }
            if (String.IsNullOrEmpty(user)) user = askUser();
            Username = user.ToUpper();
            return user;
        }

        private string askCountry()
        {
            Console.Write("Please enter the country shortname (AT for Austria, DE for Germany,...)?: ");
            return Console.ReadLine();
        }

        private string loadUser(string[] args)
        {
            var user = String.Empty;
            var index = -1;

            switch (ProcessorMode)
            {
                case ProcessorMode.DELETE:
                    index = 0;
                    break;
                case ProcessorMode.ADD:
                    index = 2;
                    break;
                case ProcessorMode.LOGIN:
                    break;
                default:
                    break;
            }

            if (index == -1) return string.Empty;

            try
            {
                user = args[index];
            }
            catch (Exception) { }
            if (String.IsNullOrEmpty(user)) user = askUser();
            Username = user.ToUpper();
            return user;
        }

        private string askUser()
        {
            Console.Write("Please enter the username?:");
            return Console.ReadLine();
        }

        private string loadPassword(string[] args)
        {
            var pw = String.Empty;
            try
            {
                pw = args[3];
            }
            catch (Exception) { }
            if (String.IsNullOrEmpty(pw)) pw = askPw();
            return pw;
        }

        private string askPw()
        {
            Console.Write("Please enter your password?: ");
            return Console.ReadLine();
        }

        //private void loadLoginData(string[] args, bool requireLandNUser)
        //{
        //    loadSystem(args);
        //    var raw = loadLandUser(args, requireLandNUser);
        //    if (String.IsNullOrEmpty(Sys) && String.IsNullOrEmpty(raw)) throw new WrongParamsException(raw, Sys);
        //}

        //private string loadLandUser(string[] args, bool requireLandNUser)
        //{
        //    var land_user = String.Empty;
        //    try
        //    {
        //        land_user = args[1];
        //    }
        //    catch (Exception) { }
        //    if (String.IsNullOrEmpty(land_user)) land_user = askLandUser();
        //    if (land_user.Length == 2)
        //    {
        //        LoginMode = LoginUserMode.COUNTRY;
        //        Country = land_user;
        //    }
        //    else
        //    {
        //        LoginMode = LoginUserMode.USER;
        //        Username = land_user;
        //    }
        //    if (requireLandNUser)
        //    {
        //        var land_user2 = String.Empty;
        //        try
        //        {
        //            land_user2 = args[1];
        //        }
        //        catch (Exception) { }
        //        if (String.IsNullOrEmpty(land_user2)) land_user2 = askLandUser();
        //        if (land_user2.Length == 2)
        //        {
        //            LoginMode = LoginUserMode.COUNTRY;
        //            Country = land_user2;
        //        }
        //        else
        //        {
        //            LoginMode = LoginUserMode.USER;
        //            Username = land_user2;
        //        }
        //        land_user += land_user2;
        //    }
        //    return land_user;
        //}



        //private string askLandUser()
        //{
        //    Console.Write("Do you want to use country or user (c for country/u for user)?: ");
        //    var antw = Console.ReadLine();

        //    if (antw.ToLower() == "c")
        //    {
        //        Console.Write("Please enter the country shortname (AT for Austria, DE for Germany,...)?: ");
        //        return Console.ReadLine();
        //    }
        //    else
        //    {
        //        Console.Write("Please enter the username?:");
        //        return Console.ReadLine();
        //    }
        //}
    }
}
