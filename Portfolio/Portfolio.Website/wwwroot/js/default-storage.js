export function setDefaultTheme() {
    const themeKey = "darkTheme";

    if (!localStorage.hasOwnProperty(themeKey)) {
        localStorage.setItem(themeKey, "true");
    }
}