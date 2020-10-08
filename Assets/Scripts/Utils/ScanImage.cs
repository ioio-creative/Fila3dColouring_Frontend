using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WIA;
using UnityEngine;
using System.IO;

namespace Fila3dColouring.Utils
{
    public class ScanImage
    {
        // https://youtu.be/PwnCjZSnE_E
        // https://www.codesenior.com/sources/docs/tutorials/How-to-get-Images-From-Scanner-in-C-with-Windows-Image-AcqusitionWIA-Library-.pdf
        public static bool Scan(string deviceName, string outputPath, int dpi)
        {
            try
            {
                DeviceInfo availableScanner = GetScannerDeviceByName(deviceName);

                if (availableScanner == null)
                {
                    Debug.LogError($"Scanner {deviceName} does not exist");
                    return false;
                }

                var device = availableScanner.Connect();
                //device.Properties.get_Item("3088").set_Value(5); // Double scanning or dublex scanning
                //device.Properties.get_Item("3088").set_Value(1); // Single scanning or simplex scanning

                var scannerItem = device.Items[1];
                scannerItem.Properties.get_Item("6147").set_Value(dpi); // horizontal dpi
                scannerItem.Properties.get_Item("6148").set_Value(dpi); // vertical dpi

                var imgFile = scannerItem.Transfer(FormatID.wiaFormatJPEG) as ImageFile;                

                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }


                imgFile.SaveFile(outputPath);
            }
            catch (COMException ex)
            {
                Debug.LogError(ex.Message);
                return false;
            }

            return true;
        }

        private static DeviceInfo GetScannerDeviceByName(string name)
        {
            string cleanedName = name.ToUpper();
            return GetScannerDeviceInfos().FirstOrDefault(x => GetNameFromDeviceInfo(x).ToUpper().Contains(cleanedName));
        }

        private static IEnumerable<DeviceInfo> GetScannerDeviceInfos()
        {
            return new DeviceManager().DeviceInfos.OfType<DeviceInfo>().Where(x => x.Type == WiaDeviceType.ScannerDeviceType);
        }

        private static string GetNameFromDeviceInfo(DeviceInfo deviceInfo)
        {
            return deviceInfo.Properties["Name"].get_Value() as string;
        }
    }
}
