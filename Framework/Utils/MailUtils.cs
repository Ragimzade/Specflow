using System;
using System.Collections.Generic;
using System.Linq;
using Framework.BaseClasses;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Framework.Utils
{
    public abstract class MailUtils : BaseEntity
    {
        private static readonly List<MimeMessage> EmailList = new List<MimeMessage>();
        private const string Host = "imap.mail.ru";
        private static readonly string UserName = Config.MailRuLogin;
        private static readonly string Password = Config.MailRuPassword;


        private static void GetInboxMessages()
        {
            using (var client = new ImapClient())
            {
                client.Connect(Host);
                client.Authenticate(UserName, Password);
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                Log.Info($"Total messages: {inbox.Count}");
                for (var i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    EmailList.Add(message);
                }

                client.Disconnect(true);
            }
        }

        public static MimeMessage FindLetter(string subject)
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