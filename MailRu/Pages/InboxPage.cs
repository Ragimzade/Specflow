using System;
using System.Linq;
using Framework.BaseClasses;
using Framework.Utils;
using MailRu.Model;
using OpenQA.Selenium;

namespace MailRu.Pages
{
    public class InboxPage : BaseForm
    {
        private static readonly By BtnWriteLetter = By.XPath("//div/span[contains(.,'Написать письмо')]");
        private static readonly By LblLetterSubject = By.XPath("//div[@class='llc__item llc__item_title']");
        private static readonly By LblChildLetterText = By.XPath(".//span[contains(@class,'subj__snippet')]");

        public InboxPage() : base(BtnWriteLetter, "Inbox Page")
        {
        }

        private IWebElement GetRandomLetter()
        {
            var random = new Random();
            var letters = FindElements(LblLetterSubject);
            return letters.OrderBy(e => random.NextDouble()).First();
        }

        public LetterData GetLetterData()
        {
            var randomLetter = GetRandomLetter();
            var subject = GetChildTextNode(randomLetter).Trim();
            var text = GetChildTextNode(randomLetter, 1).Trim();

            return new LetterData(subject, text.RemoveExcessCharacters(48));
        }
    }
}