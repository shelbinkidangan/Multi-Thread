using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Walls.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private void SendEmail(KJContact kJContact)
        {
            MailMessage mail = new MailMessage();
            var addresses = kJContact.Emails;

            mail.From = new MailAddress("sales@kjwallpapers.com", "Shijo - KJ Wallpaper");
            mail.Priority = MailPriority.High;
            foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mail.To.Add(address);
            }


            mail.Subject = "KJ Wallpaper - Business Proposal Intro";
            mail.IsBodyHtml = true; //to make message body as html  
            mail.Body = getContent(kJContact.CompanyName, kJContact.IsPurchase);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.kjwallpapers.com";
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sales@kjwallpapers.com", "redred@007");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);
        }

        [HttpGet("SendMail")]
        public async Task<IActionResult> FileUpload()
        {
            using (var reader = new StreamReader(@"C:\Users\Shelbin\Documents\DevHub\Repos\Multi-Thread\Walls\Walls\KJContacts.csv"))
            using (var csv = new CsvReader(reader))
            {
                var record = new KJContact();
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    // r is the same instance as record.
                    SendEmail(r);
                }
                return Ok(records);
            }
        }

        private string getContent(string companyName, bool isPurchase)
        {
            var htmlContent = @"<p><em><span style='font-size: 12pt;'>Respected Sir,</span></em></p>
            <p>A pleasant day to you. I am writing on behalf of <b>KJ Wallpaper</b>, one of the sought-after interior products dealer based in Bangalore. We would like to formally introduce our company and services to you.</p>
            <p>We have been in this business for 20 years and we can proudly say that we have grown our client base because of our relentless drive to meet our clients&rsquo; needs. We provide best quality products at reasonable prices. We are associated with several major interior products manufacturers and agencies across the country and many overseas. We have a high performing team and successfully executed several corporate projects in time.</p>
            <p><em>We supply and install the below items.</em></p>
            <ul style='margin-top: 0in;'>
              <li style='margin-left: 0in;'><em><b>Wallpapers</b></em></li>
            <li style='margin-left: 0in;'><em><b>Wooden Flooring</b></em></li>
            <li style='margin-left: 0in;'><em><b>Vinyl Flooring</b></em></li>
            <li style='margin-left: 0in;'><em><b>Carpets</b></em></li>
            </ul>
            <p>It would be a pleasure to be associated with ";

            htmlContent += companyName;

            htmlContent += " and provide our services for your institution. Please do not hesitate to contact us anytime for any queries. You may also learn more about our company and services by visiting our website: <em><a href='https://kjwallpapers.com/'>kjwallpapers.com</a>.</em></p>";

            if (!isPurchase)
                htmlContent += "<p><em>We would really be grateful, if you could connect us to purchase or administration team.</em></p>";

            htmlContent += @"<p>I look forward to getting a positive reply from your end soon.</p>
            <p>Yours sincerely,</p>
            <p>Shijo K Jacob</p>
            <p><em><span style='font-size: 10.0pt; line-height: 105%;'>(9611 9211 65)</span></em></p>";
            return htmlContent;
        }
    }

    public class KJContact
    {
        public string CompanyName { get; set; }
        public string Emails { get; set; }
        public bool IsPurchase { get; set; }
    }
}
