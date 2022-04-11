let helper = null;

window.setMediaReference = (dotNetHelper) => {
    helper = dotNetHelper;
}

window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", event => {
    if (helper == null) {
        return;
    }

    const isDarkTheme = event.matches ? true : false;

    try {
        helper.invokeMethodAsync("SystemThemeChanged", isDarkTheme)
            .then(data => {
                console.info(`System theme was changed, website was updated!`);
            });
    } catch (e) {
        console.error(`Failed to update website theme, on system changes.`);
    }
});