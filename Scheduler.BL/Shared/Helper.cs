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
    }
}
