using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Portfolio.Website.Extensions;
using Portfolio.Website.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Portfolio.Website.Shared
{
    public partial class LanguageSelector : ComponentBase
    {
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }
        [Inject] private IStringLocalizer<LanguageSelector> Localizer { get; set; }

        private Task<IJSObjectReference> cultureModule;
        private List<CultureInfo> supportedCultures;

        public CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (!CultureInfo.CurrentCulture.Equals(value))
                {
                    _ = Task.Run(async () =>
                    {
                        Console.WriteLine("Test");

                        var module = await CultureModule;

                        await module.InvokeVoidAsync("setCulture", value.Name);

                        NavManager.NavigateTo(NavManager.Uri, true);
                    });
                }
            }
        }

        public Task<IJSObjectReference> CultureModule =>
            cultureModule ??= JSRuntime.InjectJSObjectReference("import", "./js/culture-settings.js");

        protected override async Task OnInitializedAsync()
        {
            supportedCultures = new List<CultureInfo>()
            {
                CultureInfo.CreateSpecificCulture("da-DK"),
                CultureInfo.CreateSpecificCulture("en-US")
            };

            var module = await CultureModule;

            Culture = new CultureInfo(await module.InvokeAsync<string>("getCulture"));
        }
    }
}
