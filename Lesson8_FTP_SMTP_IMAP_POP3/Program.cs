using Renci.SshNet;

namespace Lesson8_FTP_SMTP_IMAP_POP3;

public class Program
{
    static void Main(string[] args)
    {
        // RequestSftpServer();

        // UploadFileSftpServer();

        DownLoadSftpServer();

        void RequestSftpServer()
        {
            var host = "eu-central-1.sftpcloud.io";
            int port = 22;
            var username = "8c43f3b237ad44dcb5938cbda70f2a12";
            var password = "7kQGs6WCFu9tIt4yKFfR1lXDkG19WHb3";

            using var client = new SftpClient(host, port, username, password);


            client.Connect();

            var directories = client.ListDirectory("Xayyam");


            foreach (var item in directories)
            {
                Console.WriteLine($"Directory name: {item.FullName}");
            }
        }

        void UploadFileSftpServer()
        {
            var host = "eu-central-1.sftpcloud.io";
            int port = 22;
            var username = "8c43f3b237ad44dcb5938cbda70f2a12";
            var password = "7kQGs6WCFu9tIt4yKFfR1lXDkG19WHb3";

            string localFilePath = "Xayyam.txt";
            string remoteFilePath = "Xayyam/Xayyam.txt";

            if (!File.Exists(localFilePath))
                File.WriteAllText(localFilePath, "Xayyam dersde az saqqiz ceyne...");

            using var client = new SftpClient(host, port, username, password);
    
            client.Connect();

            using var fileSteam = new FileStream(localFilePath, FileMode.Open);

            client.UploadFile(fileSteam, remoteFilePath);


            Console.WriteLine("Successfully sending file...");
        }

        void DownLoadSftpServer()
        {
            var host = "eu-central-1.sftpcloud.io";
            int port = 22;
            var username = "8c43f3b237ad44dcb5938cbda70f2a12";
            var password = "7kQGs6WCFu9tIt4yKFfR1lXDkG19WHb3";
         
            using var client = new SftpClient(host, port, username, password);
            
            string remoteFilePath = "Xayyam/Xayyam.txt";
            
            client.Connect();

            using var output = File.Create("file.txt");

            client.DownloadFile(remoteFilePath, output);

            Console.WriteLine("Successfully dowload file...");
        }
    }
}
