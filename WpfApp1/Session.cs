using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Session
    {
        public string token;
        private DateTime sessionStart;
        public Session(string token)
        {
            this.token = token;
            sessionStart = DateTime.Now;
        }
        public void UpdateSession()
        {
            sessionStart = DateTime.Now;
        }
        bool ValidateToken(string token)
        {
            bool isValid = false;

            // Ask server if token is valid;
            // for now
            if (token == "b23ee88746a876c854a58ee11aa8ef35585e30561a2117d18236cb3edb419b8a")
            {
                isValid = true;
            }

            return isValid;
        }
        public bool IsValid()
        {
            if ((sessionStart < sessionStart.AddMinutes(120)) && ValidateToken(token))
            {
                return true;
            } 
            else
            {
                token = null;

                return false;
            }
        }
    }
}
