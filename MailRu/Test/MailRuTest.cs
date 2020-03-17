using Framework.Utils;
using MailRu.Api;
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

            StepHelper.LogStep("Get random letter");
            var letter = inboxPage.GetLetterData();

            StepHelper.LogStep("Get the same letter by api");
            var apiLetter = MailApi.FetchApiLetter(letter.Subject);

            StepHelper.LogStep("Assert letters are equal");
            Assert.That(apiLetter, Is.EqualTo(letter), "Api letter and letter from UI are not equal");
        }
    }
}