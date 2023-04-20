namespace VolumeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welche Informationen möchtest du anzeigen? (1 - Gesamtvolumen, 2 - Volumen Info, 3 - Freier Speicher in Prozent, 4 - Beenden)");
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
        
        static void ShowTotalSpace()
        {
            double totalFreeSpace = 0;
            double totalSpace = 0;
            double totalSpace2 = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    totalFreeSpace += drive.TotalFreeSpace;
                    totalSpace += drive.TotalSize;
                    totalSpace2 += drive.TotalSize;
                }
            }

            double totalPercentFreeSpace = (totalFreeSpace / totalSpace) * 100;

            Console.WriteLine("Gesamtspeicher: {0:F2} GB / {0:F2}", (totalSpace / (1024 * 1024 * 1024)), (totalSpace2 / (1024 * 1024 * 1024)));
            Console.WriteLine("Freier Speicher: {0:F2} GB", (totalFreeSpace / (1024 * 1024 * 1024)));
            Console.WriteLine("Freier Speicher insgesamt: {0:F2}%", totalPercentFreeSpace);
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
