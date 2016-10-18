/*
 * learn
 HttpUtility.HtmlEncode 
  HttpUtility.UrlEncode 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kuroneko.Security
{
    public class Validate
    {
        // validates input a-z A-Z 0-9
        public static bool validateUserInput(string input)
        { 
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-z0-9]*$");

            if (!regex.IsMatch(input)) return false;
            else return true;
        }
        
        // example of TryParse not a good one
        public static bool stringToDouble(string input, out double output)
        {
            if (Double.TryParse(input, out output))
            {
                return true;
            }
            else 
            {
                return false;
            }
                         
        }



    }
}