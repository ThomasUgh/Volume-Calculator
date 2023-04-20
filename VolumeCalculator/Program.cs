using System;
using System.IO;

namespace DiskSpaceChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welche Informationen möchtest du anzeigen? (1 - Gesamtvolumen, 2 - Freier Speicher, 3 - Freier Speicher in Prozent, 4 - Beenden)");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        ShowTotalSpace();
                        break;
                    case 2:
                        ShowFreeSpace();
                        break;
                    case 3:
                        ShowFreeSpacePercentage();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ungültige Option.");
                        break;
                }
            }
        }

        static void ShowTotalSpace()
        {
            double totalSpace = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine("{0} - Gesamtvolumen: {1:F2} GB", drive.Name, (drive.TotalSize / (1024 * 1024 * 1024)));
                    totalSpace += drive.TotalSize;
                }
            }

            Console.WriteLine("Gesamtspeicher: {0:F2} GB", (totalSpace / (1024 * 1024 * 1024)));
            Console.WriteLine();
        }

        static void ShowFreeSpace()
        {
            double totalFreeSpace = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine("{0} - Freier Speicher: {1:F2} GB", drive.Name, (drive.TotalFreeSpace / (1024 * 1024 * 1024)));
                    totalFreeSpace += drive.TotalFreeSpace;
                }
            }

            Console.WriteLine("Freier Speicher insgesamt: {0:F2} GB", (totalFreeSpace / (1024 * 1024 * 1024)));
            Console.WriteLine();
        }

        static void ShowFreeSpacePercentage()
        {
            double totalFreeSpace = 0;
            double totalSpace = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    totalFreeSpace += drive.TotalFreeSpace;
                    totalSpace += drive.TotalSize;
                }
            }

            double totalPercentFreeSpace = (totalFreeSpace / totalSpace) * 100;

            Console.WriteLine("Freier Speicher insgesamt: {0:F2}%", totalPercentFreeSpace);
            Console.WriteLine();
        }
    }
}
