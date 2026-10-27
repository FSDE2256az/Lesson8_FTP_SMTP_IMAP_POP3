using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Security;
using Renci.SshNet;
using System.Net;
using System.Net.Mail;

namespace Lesson8_FTP_SMTP_IMAP_POP3;

public class Program
{
    static void Main(string[] args)
    {
        // RequestSftpServer();
        // UploadFileSftpServer();
        // DownLoadSftpServer();

        string email = "yusufbagirov2010@gmail.com";
        string password = "ispddsadasdsadaidxmqcy";
        // Pop3();
        // Imap();

        Smtp();

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

        void Pop3()
        {
            try
            {
                var host = "pop.gmail.com";
                var port = 995;

                using var client = new Pop3Client();

                client.Connect(host, port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(email, password);

                int messageCount = client.GetMessageCount();
                Console.WriteLine(messageCount);

                for (int i = 0; i < messageCount; i++)
                {
                    var message = client.GetMessage(i);

                    Console.WriteLine($"From: {message.From}");
                    Console.WriteLine($"Subject: {message.Subject}");
                    Console.WriteLine($"Body: {message.Body}");
                }

                client.Disconnect(true);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        void Imap()
        {
            var host = "imap.gmail.com";
            var port = 993;

            using var client = new ImapClient();

            client.Connect(host, port, SecureSocketOptions.SslOnConnect);
            client.Authenticate(email, password);

            client.Inbox.Open(FolderAccess.ReadOnly);

            int messageCount = client.Inbox.Count;
            Console.WriteLine(messageCount);

            for (int i = 0; i < messageCount; i++)
            {
                var message = client.Inbox.GetMessage(i);

                Console.WriteLine($"From: {message.From}");
                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"Body: {message.Body}");
            }

            client.Disconnect(true);
        }


        void Smtp()
        {
            var host = "smtp.gmail.com";
            var port = 587;

            using var client = new SmtpClient(host)
            {
                Port = port,
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password)
            };

            using var message = new MailMessage()
            {
                Body = "Test message body",
                Subject = "Test subject",
                IsBodyHtml = false,
            };

            message.From = new MailAddress(email);
            message.To.Add(new MailAddress("kamranforstep@gmail.com"));

            client.Send(message);
        }

    }
}
