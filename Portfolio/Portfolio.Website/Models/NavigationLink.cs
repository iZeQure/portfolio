using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Portfolio.Website.Models
{
    public class NavigationLink
    {
        public NavigationLink(NavLinkMatch linkMatch, string uri, string displayText, MarkupString icon)
        {
            LinkMatch = linkMatch;
            Uri = uri;
            DisplayText = displayText;
            HtmlIcon = icon;
        }

        public NavLinkMatch LinkMatch { get; init; }

        public string Uri { get; init; }

        public string DisplayText { get; init; }

        public MarkupString HtmlIcon { get; init; }
    }
}
