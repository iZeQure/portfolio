using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Portfolio.Website.Extensions;
using Portfolio.Website.Models;

namespace Portfolio.Website.Shared
{
    public partial class LanguageSelector : ComponentBase, IDisposable
    {
        private event EventHandler OnCultureChanged;

        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }
        [Inject] private IStringLocalizer<LanguageSelector> Localizer { get; set; }

        private Task<IJSObjectReference> _cultureModule;

        private IEnumerable<LanguageCodes> _supportedLanguages;

        public CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture.Equals(value)) return;

                _ = Task.Run(async () =>
                {
                    Console.WriteLine(@"Test");

                    var module = await CultureModule;

                    await module.InvokeVoidAsync("setCulture", value.Name);

                    OnCultureChanged?.Invoke(this, EventArgs.Empty);
                });
            }
        }

        public Task<IJSObjectReference> CultureModule =>
            _cultureModule ??= JsRuntime.InjectJsObjectReference("import", "./js/culture-settings.js");

        protected override async Task OnInitializedAsync()
        {
            OnCultureChanged += RefreshPage;

            var danishCulture = Localizer["DanishCulture"].Value.Split(";", StringSplitOptions.TrimEntries);
            var englishCulture = Localizer["EnglishCulture"].Value.Split(";", StringSplitOptions.TrimEntries);

            _supportedLanguages  = new List<LanguageCodes>
            {
                new (code: danishCulture[0], displayName: danishCulture[1], country: "🇩🇰"),
                new (code: englishCulture[0], displayName: englishCulture[1], country: "🇺🇸")
            };

            var module = await CultureModule;

            Culture = new CultureInfo(await module.InvokeAsync<string>("getCulture"));
        }

        private void RefreshPage(object? sender, EventArgs e)
        {
            NavManager.NavigateTo(NavManager.Uri, true);
        }

        public void Dispose()
        {
            OnCultureChanged -= RefreshPage;
        }
    }
}
