const themeKey = 'darkTheme';

export function setTheme(theme) {
    localStorage.setItem(themeKey, theme);
}

export function getTheme() {
    var theme = localStorage.getItem(themeKey);

    return theme;
}