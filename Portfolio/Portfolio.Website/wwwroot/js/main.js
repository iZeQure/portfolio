window.setDotNetReferenceForThemeSelector = (dotNetReference) => {
    const ref = dotNetReference;
    window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", event => {
        if (ref == null) {
            return;
        }

        const isDarkTheme = event.matches ? true : false;

        try {
            ref.invokeMethodAsync("SystemThemeChanged", isDarkTheme)
                .then(() => {
                    console.info(`System theme was changed, website was updated!`);
                });
        } catch (e) {
            console.error(`Failed to update website theme, on system changes.`);
        }
    });
}

window.setDotNetReferenceForLanguageSelector = (dotNetReference) => {
    const ref = dotNetReference;
    window.matchMedia("(min-width: 768px)").addEventListener('change', event => {
        if (ref == null) {
            console.warn("Dotnet help reference is null.");
            return;
        }

        try {
            ref.invokeMethodAsync("InitializeMobileDeviceMode", event.matches ? false : true)
                .then(() => {
                    console.info("Updated device mode.");
                });
        } catch (e) {
            console.error("Unsupported error occurred: {0}", e);
        }
    });
}