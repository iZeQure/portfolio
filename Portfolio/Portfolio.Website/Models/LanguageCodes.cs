namespace Portfolio.Website.Models
{
    public class LanguageCodes
    {
        public LanguageCodes()
        {
        }

        public LanguageCodes(string code, string displayName)
        {
            Code = code;
            DisplayName = displayName;
        }

        public string DisplayName { get; init; }

        public string Code { get; init; }
    }
}
