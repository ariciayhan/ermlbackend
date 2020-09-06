using Iaea.SG.EQUIS.Frontend.Web;
using Iaea.SG.EQUIS.Frontend.Web.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace WebApi.Jwt
{
    public class HomeController : Controller
    {
        public string GetIPAddress()
        {
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Request.Url.Authority); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return ipAddress.ToString();
        }

        private string GetBaseUrl()
        {
            return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        }

        public ActionResult Index()
        {
            var token = JwtManager.GenerateToken(new[] {
                        new Claim(ErmlClaimTypes.LoginName, "ariciogull"),
                        new Claim(ErmlClaimTypes.FullName, "Ayhan Ariciogullarindan"),
                        new Claim(ErmlClaimTypes.EmployeeId, "12354"),                                       
                        new Claim(ErmlClaimTypes.BaseUrl, GetBaseUrl()),
                        new Claim(ErmlClaimTypes.ServerIp, GetIPAddress())
                    }
                );

            ViewBag.Token = token;
            ViewBag.ServerIp = GetIPAddress();
            ViewBag.BaseUrl = GetBaseUrl();            

            using (MemoryStream ms = new MemoryStream())
            {  
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(token, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View();
        }              
    }
}