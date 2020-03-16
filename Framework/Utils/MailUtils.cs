using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Framework.Utils
{
    public static class MailUtils
    {
        private static readonly List<MimeMessage> EmailList = new List<MimeMessage>();
        private const string Host = "imap.mail.ru";
        private const string UserName = "mihalichenkoo@mail.ru";
        private const string Password = "9802357s";


        private static void GetInboxMessages()
        {
            using (var client = new ImapClient())
            {
                client.Connect(Host);

                client.Authenticate(UserName, Password);
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);
                for (var i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    EmailList.Add(message);
                }

                client.Disconnect(true);
            }
        }
        
        public static MimeMessage FindEmail(string subject)
        {
            GetInboxMessages();
            var email = EmailList.FirstOrDefault(e => e.Subject.Equals(subject));
            if (email != null)
            {
                return email;
            }

            throw new Exception($"Message with subject '{subject}' is not found");
        }
    }
}