using FlintCli.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CrmLoginPlugin
{
    public class CrmLogin : IPlugin
    {
        public string Execute(string[] args)
        {
            var proc = new LoginProcessor();

            if (args.Length == 0 || IsArg(args[0], "h", "help"))
            {
                //Print help
                printHelp();
            }
            else if (IsArg(args[0], "a", "add"))
            {
                var a = args.Skip(1).Take(args.Length - 1).ToArray();
                proc.AddLogin(a).Wait();
            }
            else if (IsArg(args[0], "l", "list"))
            {
                proc.ListLogins();
            }
            else if (IsArg(args[0], "d", "delete"))
            {
                string[] a = new List<string>().ToArray();
                if(args.Length > 1) a = args.Skip(1).Take(args.Length - 1).ToArray();
                proc.DeleteLogin(a).Wait();
            }
            else if (IsArg(args[0], "r", "removeall"))
            {
                proc.RemoveAll().Wait();
            }
            else if (args[0].StartsWith("-")){
                //Es wurde ein flag angegeben, das es aber nicht gibt - hilfe anzeigen
                printHelp();
            }
            else
            {
                var a = args.Skip(1).Take(args.Length - 1).ToArray();
                proc.Login(a);
            }

            return "";
        }

        private void printHelp()
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($@"Usage: crmlogin [plugin-options] [arguments]");
            sb.AppendLine("");
            sb.AppendLine($"Options:");
            sb.AppendLine("\t-h|--help\tHelp");
            sb.AppendLine("\t-a|--add\tAdd new login, usage: -a/--add <system> <country_short> <username> <password>");
            sb.AppendLine("\t-l|--list\tList logins");
            sb.AppendLine("\t-d|--delete\tDelete loging, usage: -d/--delete <username>");
            sb.AppendLine("\t-r|--removeall\tDelete all logins");
            sb.AppendLine("");
            sb.AppendLine($"Login:");
            sb.AppendLine($"\tUsage <system> <username>");
            sb.AppendLine("");
            Console.Write(sb.ToString());
        }

        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                UniquePluginName = "CRM Login",
                PluginCommand = "crmlogin"
            };
        }

        private bool IsArg(string candidate, string longName)
        {
            return IsArg(candidate, shortName: null, longName: longName);
        }

        private bool IsArg(string candidate, string shortName, string longName)
        {
            return (shortName != null && candidate.Equals("-" + shortName)) || (longName != null && candidate.Equals("--" + longName));
        }
    }
}
