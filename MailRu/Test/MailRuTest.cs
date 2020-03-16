using System;
using Framework.Utils;
using MimeKit.Text;
using NUnit.Framework;

namespace MailRu.Test
{
    public class MailRuTest : BaseTest
    {
        [Test]
        public void InboxLettersTest()
        {
            StepHelper.LogStep("Login in");
            var inboxPage = MainPage.SignIn(JsonReader.ReadValueFromConfig("mailRuLogin"),
                JsonReader.ReadValueFromConfig("mailRuPassword"));

            StepHelper.LogStep("Get random email");
            var letter = inboxPage.GetLetterData();
            Console.WriteLine(letter.Text);

            StepHelper.LogStep("Get the same email by api");
            var emailByApi = MailUtils.FindEmail(letter.Subject);

            StepHelper.LogStep("Assert emails have the same subjects");
            Assert.That(letter.Subject, Is.EqualTo(emailByApi.Subject),
                $"Subject '{letter.Subject}' is not equal to subject from api '{emailByApi.Subject}'");

            StepHelper.LogStep("Assert emails from api contains letter's text body");
            var emailContentByApi = emailByApi.GetTextBody(TextFormat.Text).ReplaceNewLineWithSpace();
            Assert.That(emailContentByApi.Contains(letter.Text),
                $"Email text body fro, api doesn't contain {letter.Text}");
        }
    }
}