const themeKey = "darkTheme";

export function setTheme(theme) {
    localStorage.setItem(themeKey, theme);
}

export function getTheme() {
    const theme = localStorage.getItem(themeKey);

    return theme;
}

export function updateTooltip() {
    const theme = document.getElementById('theme-tooltip');
    const tooltip = bootstrap.Tooltip.getOrCreateInstance(theme);

    tooltip.hide();
    tooltip.update();
}