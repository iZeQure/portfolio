export function setDefaultTheme() {
    const themeKey = "darkTheme";

    if (window.matchMedia &&
        window.matchMedia("(prefers-color-scheme: dark)").matches) {
        localStorage.setItem(themeKey, "true");
        return;
    }

    localStorage.setItem(themeKey, "false");
}