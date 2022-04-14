using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Portfolio.Website.Extensions;

namespace Portfolio.Website.Shared.Modules
{
    public class ThemeLayoutModule : LayoutComponentBase
    {
        [Inject] private ILogger<ThemeLayoutModule> Logger { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        
        private Task<IJSObjectReference> _themeModule;

        protected string ColorThemeCssClass => IsDarkTheme ? "dark-theme" : null;

        private Task<IJSObjectReference> Module => _themeModule ??= JsRuntime.InjectJsObjectReference("import", "./js/theme-settings.js");

        protected bool IsDarkTheme { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await JsRuntime.InvokeVoidAsync("setMediaReference", DotNetObjectReference.Create(this));
        }

        protected async Task ToggleTheme()
        {
            try
            {
                IsDarkTheme = !IsDarkTheme;

                var module = await Module;
                await module.InvokeVoidAsync("setTheme", IsDarkTheme);
            }
            catch (Exception)
            {
                Logger.LogError("Error occurred while toggling theme.");
            }
        }

        protected async Task GetTheme()
        {
            try
            {
                var module = await Module;

                var value = await module.InvokeAsync<string>("getTheme");

                IsDarkTheme = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                Logger.LogError("Error occurred while getting theme.");
            }
        }

        [JSInvokable]
        public async Task SystemThemeChanged(bool value)
        {
            try
            {
                IsDarkTheme = value;

                var module = await Module;
                await module.InvokeVoidAsync("setTheme", IsDarkTheme);

                StateHasChanged();
            }
            catch (Exception)
            {
                Logger.LogError("Error while updating theme with system changes.");
            }
        }
    }
}
