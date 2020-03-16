namespace MailRu.Model
{
    public class LetterData
    {
        public string Subject { get; }
        public string Text { get; }

        public LetterData(string subject, string text)
        {
            Subject = subject;
            Text = text;
        }
        
        public override string ToString()
        {
            return $"subject:{Subject}, text:{Text}";
        }

    }
}