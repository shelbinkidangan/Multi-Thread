using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        public async Task Get()
        {
            MailMessage mail = new MailMessage();
            var addresses = "shelbin@cormsquare.com;";

            mail.From = new MailAddress("sales@kjwallpapers.com", "KJ Wallpaper");
            mail.Priority = MailPriority.High;
            foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mail.To.Add(address);
            }


            mail.Subject = "KJ Wallpaper - Business Proposal Intro";
            mail.IsBodyHtml = true; //to make message body as html  
            mail.Body = getContent("COM", false);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.kjwallpapers.com";
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sales@kjwallpapers.com", "redred@007");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);
        }



        private string getContent(string companyName, bool isPurchase)
        {
            var htmlContent = @"<p><em><span style='font-size: 12pt;'>Respected Sir or Ma&rsquo;am,</span></em></p>
            <p>A pleasant day to you. I am writing on behalf of KJ Wallpaper, one of the sought-after interior products dealer based in Bangalore. We would like to formally introduce our company and services to you.</p>
            <p>We have been in this business for 20 years and we can proudly say that we have grown our client base because of our relentless drive to meet our clients&rsquo; needs. We provide best quality products at reasonable prices. We are associated with several major interior products manufacturers and agencies across the country and many overseas. We have a high performing team and successfully executed several corporate projects in time.</p>
            <p><em>We supply and install the below items.</em></p>
            <ul style='margin-top: 0in;'>
            <li style='margin-left: 0in;'><em>Wallpapers</em></li>
            <li style='margin-left: 0in;'><em>Wooden Flooring</em></li>
            <li style='margin-left: 0in;'><em>Vinyl Flooring</em></li>
            <li style='margin-left: 0in;'><em>Carpets</em></li>
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
}
