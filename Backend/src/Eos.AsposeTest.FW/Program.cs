using Aspose.BarCode;
using Aspose.BarCode.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.AsposeTest.FW
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var license = new License();
                string licFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspose.Total.lic");
                license.SetLicense(licFileName);

                using (var barcodeGen = new BarcodeGenerator(EncodeTypes.EAN13, ((long)93).ToString("d13")))
                {
                    barcodeGen.Parameters.Barcode.XDimension.Millimeters = 1f;
                    var img = barcodeGen.GenerateBarCodeImage();
                    barcodeGen.Save("barcode.jpg", BarCodeImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
