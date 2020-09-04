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

namespace WebApi.Jwt
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var token = JwtManager.GenerateToken(new[] {
                    new Claim(ErmlClaimTypes.LoginName, "ariciogull"),
                    new Claim(ErmlClaimTypes.FullName, "Ayhan Ariciogullarindan"),
                    new Claim(ErmlClaimTypes.EmployeeId, "12354"),
                    new Claim(ErmlClaimTypes.Host, @"sequoia-test.sg.iaea.org"),
                    new Claim(ErmlClaimTypes.ServerIp, @"192.168.0.221"),
                    new Claim(ErmlClaimTypes.BaseUrl, @"http://192.168.0.221/jwt/"),

                    }
                );

            ViewBag.Token = token;

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