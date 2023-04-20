using static System.Console;

namespace VolumeCalculator
{
    class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                WriteLine("Welche Informationen möchtest du anzeigen? (1 - Gesamtvolumen, 2 - Volumen Info, 3 - Freier Speicher in Prozent, 4 - Beenden)");
                var option = int.Parse(ReadLine());

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
                        WriteLine("Ungültige Option.");
                        break;
                }
            }
        }


        private static void ShowFreeSpace()
        {
            double totalFreeSpace = 0;

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady) continue;
                WriteLine("{0} - Freier Speicher: {1:F2} GB", drive.Name, (drive.TotalFreeSpace / (1024 * 1024 * 1024)));
                totalFreeSpace += drive.TotalFreeSpace;
            }

            WriteLine("Freier Speicher insgesamt: {0:F2} GB", (totalFreeSpace / (1024 * 1024 * 1024)));
            WriteLine();
        }

        private static void ShowTotalSpace()
        {
            double totalFreeSpace = 0;
            double totalSpace = 0;
            double totalSpace2 = 0;

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady) continue;
                totalFreeSpace += drive.TotalFreeSpace;
                totalSpace += drive.TotalSize;
                totalSpace2 += drive.TotalSize;
            }

            var totalPercentFreeSpace = (totalFreeSpace / totalSpace) * 100;

            WriteLine("Gesamtspeicher: {0:F2} GB / {0:F2}", (totalSpace / (1024 * 1024 * 1024)), (totalSpace2 / (1024 * 1024 * 1024)));
            WriteLine("Freier Speicher: {0:F2} GB", (totalFreeSpace / (1024 * 1024 * 1024)));
            WriteLine("Freier Speicher insgesamt: {0:F2}%", totalPercentFreeSpace);
            WriteLine();
        }

        private static void ShowFreeSpacePercentage()
        {
            double totalFreeSpace = 0;
            double totalSpace = 0;

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady) continue;
                totalFreeSpace += drive.TotalFreeSpace;
                totalSpace += drive.TotalSize;
            }

            var percentFreeSpace = (totalFreeSpace / totalSpace) * 100;

            WriteLine("Freier Speicher insgesamt: {0:F2}%", percentFreeSpace);
            WriteLine();
        }
    }
}
