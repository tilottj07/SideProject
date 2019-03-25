using System;
using System.Linq;

namespace Scheduler.BL.Shared
{
    public static class Helper
    {
       

        public static string CleanString(string val)
        {
            string v = string.Empty;
            if (!string.IsNullOrWhiteSpace(val))
            {
                v = val.Trim();
            }
            return v;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public static DateTime GetDefaultEndDate()
        {
            return Convert.ToDateTime("1/1/2070");
        }

        public static Guid? ConvertToGuid(string val)
        {
            if (Guid.TryParse(val, out Guid id)) return id;

            return null;
        }

        public static string FormatPhoneNumber(string val)
        {
            try
            {
                val = CleanString(val);
                string number = string.Empty;

                number = new string(val.Where(c => char.IsDigit(c)).ToArray());

                if (number.Length == 10)
                    number = $"{number.Substring(0, 3)}-{number.Substring(3, 3)}-{number.Substring(6, 4)}";
                else if (number.Length == 11)
                    number = $"{number.Substring(0, 1)}-{number.Substring(1, 3)}-{number.Substring(4, 3)}-{number.Substring(7, 4)}";

                return number;
            }
            catch(Exception ex)
            {
                string s = ex.ToString();
                return CleanString(val);
            }
        }


        public static string GetDisplayName(string userName, string firstName, string middleInitial, string lastName)
        {
            string val = string.Empty;
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(middleInitial) && !string.IsNullOrWhiteSpace(lastName))
            {
                val = $"{firstName} {middleInitial}. {lastName}";
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                val = $"{firstName} {lastName}";
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(middleInitial))
            {
                val = $"{firstName} {middleInitial}.";
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                val = firstName;
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                val = lastName;
            }
            else
            {
                val = userName;
            }

            return val.Trim();
        }

    }
}
