using System;
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
            Guid id;
            if (Guid.TryParse(val, out id)) return id;

            return null;
        }

    }
}
