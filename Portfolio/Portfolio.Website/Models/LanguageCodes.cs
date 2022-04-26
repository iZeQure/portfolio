namespace Portfolio.Website.Models
{
    public class LanguageCodes
    {
        public LanguageCodes()
        {
        }

        public LanguageCodes(string code, string displayName, string country)
        {
            Code = code;
            DisplayName = displayName;
            Country = country;
        }

        public string DisplayName { get; init; }

        public string Code { get; init; }

        public string Country {get; init; }
    }
}
