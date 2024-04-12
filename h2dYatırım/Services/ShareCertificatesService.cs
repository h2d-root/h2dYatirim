using HtmlAgilityPack;
using System.Net;

namespace h2dYatırım.Services
{
    public class ShareCertificatesService
    {
        public static List<string> Hisse(string tr)
        {
            List<string> hissealis = new List<string>();
            WebClient client = new WebClient();
            string html = client.DownloadString("https://www.getmidas.com/canli-borsa/xu100-bist-100-hisseleri");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            //HtmlNodeCollection name = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div/div[6]/table/tbody/tr["+tr+"]/td[1]");
            HtmlNodeCollection son = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div/div[6]/table/tbody/tr["+tr+"]/td[2]");
            HtmlNodeCollection fark = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div/div[6]/table/tbody/tr["+tr+"]/td[5]");
            //HtmlNodeCollection gmin = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div/div[6]/table/tbody/tr["+tr+"]/td[6]");
            //HtmlNodeCollection gmax = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div/div[6]/table/tbody/tr["+tr+"]/td[7]");

            //hissealis.Add(name[0].InnerText.Replace("\n", "").Replace(" ", ""));
            hissealis.Add(fark[0].InnerText.Replace("\n", "").Replace(" ", ""));
            hissealis.Add(son[0].InnerText.Replace("\n", "").Replace(" ", "").Replace(".", "").Replace(",", "."));
            //hissealis.Add(gmin[0].InnerText.Replace("\n", "").Replace(" ", "").Replace(".", "").Replace(",", "."));
            //hissealis.Add(gmax[0].InnerText.Replace("\n", "").Replace(" ", "").Replace(".", "").Replace(",", "."));


            return hissealis;
        }
    }
}
