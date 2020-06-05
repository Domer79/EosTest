using Aspose.BarCode;
using Aspose.BarCode.Generation;
using System;
using System.IO;

namespace Eos.AsposeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var license = new License();
                string licFileName = Path.Combine(
                    System.AppDomain.CurrentDomain.BaseDirectory,
                    //Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Bin"),
                    "Aspose.Total.lic");
                license.SetLicense(licFileName);

                using var barcodeGen = new BarcodeGenerator(EncodeTypes.EAN13, ((long)93).ToString("d13"));
               var img = barcodeGen.GenerateBarCodeImage();
                barcodeGen.Save("barcode.jpg", BarCodeImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
