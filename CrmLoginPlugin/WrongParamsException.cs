using System;
using System.Runtime.Serialization;

namespace CrmLoginPlugin
{
    internal class WrongParamsException : Exception
    {
        public WrongParamsException(string land_user, string system):this(land_user, system, null)
        { 
        }

        public WrongParamsException(string land_user, string system, Exception innner) : base($"The given parameters country/user {(String.IsNullOrEmpty(land_user) ? "<missing>" : land_user)} or system {(String.IsNullOrEmpty(system) ? "<missing>" : system)} is empty!") 
        {
        }
    }
}