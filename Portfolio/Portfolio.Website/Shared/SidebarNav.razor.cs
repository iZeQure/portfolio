using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Portfolio.Website.Extensions;
using Portfolio.Website.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Website.Shared
{
    public partial class SidebarNav : ComponentBase
    {
        [Inject] protected internal IStringLocalizer<MainLayout> LocalizerMainLayout { get; set; }
        [Inject] protected internal IStringLocalizer<SidebarNav> LocalizerSidebarNav { get; set; }
        [Inject] internal ILogger<SidebarNav> Logger { get; set; }
        [Inject] internal IJSRuntime JSRunTime { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        private Task<IJSObjectReference> SidebarNavModule =>
            JSRunTime?.InjectJsObjectReference("import", "./js/sidebar.js");

        private string GetSiteName => LocalizerMainLayout["Name"];

        private IEnumerable<NavigationLink> NavigationLinks => new List<NavigationLink>()
        {
            new NavigationLink(NavLinkMatch.All,
                "", LocalizerSidebarNav["Home"], new MarkupString("<i class='fa-solid fa-terminal'></i>")),
            new NavigationLink(NavLinkMatch.Prefix,
                "projects", LocalizerSidebarNav["Projects"], new MarkupString("<i class='fa-solid fa-code'></i>"))
        };

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _ = await SidebarNavModule;
            }
            catch (JSException ex)
            {
                Logger.LogWarning("Could not inject sidebar module: {Message}", ex.Message);
            }
            catch (Exception)
            {
                Logger.LogCritical("Unhandled exception occurred.");
            }
        }
    }
}
