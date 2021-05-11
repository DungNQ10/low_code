using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Lowcode.Domain.Shared
{
    public static class BarcodeHelper
    {
        public static string GeneratePng(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using (var memoryStream = new MemoryStream())
            {
                qrCodeImage.Save(memoryStream, ImageFormat.Png);

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}