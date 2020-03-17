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
        private static readonly By BtnWriteLetter =
            By.XPath("//div[contains(@class,'sidebar')]//span[@class='compose-button__wrapper']");

        private static readonly By LblLetterTitle =
            By.XPath("//div[@class='llc__content']//div[contains(@class,'item_title')]");

        public InboxPage() : base(BtnWriteLetter, "Inbox Page")
        {
        }

        private IWebElement GetRandomLetter()
        {
            var random = new Random();
            var letters = FindElements(LblLetterTitle);
            return letters.OrderBy(e => random.NextDouble()).First();
        }

        public LetterData GetLetterData()
        {
            var randomLetter = GetRandomLetter();
            var subject = GetChildTextNodeByIndex(randomLetter).Trim();
            var textBody = GetChildTextNodeByIndex(randomLetter, 1).Trim();
            return new LetterData(subject, textBody.RemoveExcessCharacters(80));
        }
    }
}