using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Portfolio.Website.Extensions;

namespace Portfolio.Website.Shared.Modules
{
    public class ThemeLayoutModule : LayoutComponentBase
    {
        [Inject] private IStringLocalizer<MainLayout> LayoutLocalization { get; set; }
        [Inject] private ILogger<ThemeLayoutModule> Logger { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private Task<IJSObjectReference> _themeModule;
        private bool _isDarkTheme;

        private Task<IJSObjectReference> Module => _themeModule ??= JsRuntime.InjectJsObjectReference("import", "./js/theme-settings.js");

        protected bool IsDarkTheme { get => _isDarkTheme; private set => _isDarkTheme = value; }

        protected string ColorThemeCssClass => _isDarkTheme ? "dark-theme" : null;

        protected string TooltipTitle =>
            _isDarkTheme ? LayoutLocalization["LightTheme"] : LayoutLocalization["DarkTheme"];

        protected override async Task OnInitializedAsync()
        {
            await JsRuntime.InvokeVoidAsync("setMediaReference", DotNetObjectReference.Create(this));
        }

        protected async Task ToggleTheme()
        {
            try
            {
                _isDarkTheme = !_isDarkTheme;

                var module = await Module;
                await module.InvokeVoidAsync("setTheme", _isDarkTheme);
                await module.InvokeVoidAsync("updateTooltip");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error occurred while toggling theme: {Message}", ex.Message);
            }
        }

        protected async Task GetTheme()
        {
            try
            {
                var module = await Module;

                var value = await module.InvokeAsync<string>("getTheme");

                _isDarkTheme = Convert.ToBoolean(value);
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
                _isDarkTheme = value;

                var module = await Module;
                await module.InvokeVoidAsync("setTheme", _isDarkTheme);

                StateHasChanged();
            }
            catch (Exception)
            {
                Logger.LogError("Error while updating theme with system changes.");
            }
        }
    }
}
