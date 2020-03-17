using Framework.Utils;
using MailRu.Model;

namespace MailRu.Api
{
    public class MailApi
    {
        public static LetterData FetchApiLetter(string subject)
        {
            var apiLetter = MailUtils.FindLetter(subject);
            var textBody = apiLetter.TextBody.ReplaceNewLineWithSpace().Trim();
            return new LetterData(apiLetter.Subject, textBody.RemoveExcessCharacters(80));
        }
    }
}