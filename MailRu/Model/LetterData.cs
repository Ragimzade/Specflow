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

        public override bool Equals(object obj)
        {
            if (!(obj is LetterData letterData))
            {
                return false;
            }

            return Subject.Equals(letterData.Subject) && Text.Equals(letterData.Text);
        }
    }
}