using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab.aspxapps
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fromCurrency = "USD";
            string toCurrency = "KWD";
            decimal amount = 1;

            //WebClient web = new WebClient();
            //string url = string.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount);
            //string response = web.DownloadString(url);
            //Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
            //decimal rate = System.Convert.ToDecimal(regex.Match(response).Groups[1].Value);

            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");

            decimal rate = System.Convert.ToDecimal(values[1]);

            string strRateDate = System.Convert.ToString(values[2]).Remove(0, 1);

            int rateDateLastIndex = strRateDate.Length - 1;

            strRateDate = strRateDate.Remove(rateDateLastIndex);

            string strRateTime = System.Convert.ToString(values[3]).Remove(0, 1);
            strRateTime = strRateTime.Substring(0, strRateTime.Length - 2);
            
            DateTime rateDateTime = DateTime.Parse(strRateDate + " " + strRateTime);
            
            decimal myrate = rate * amount;
        }
    }
}