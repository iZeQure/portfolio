window.setTitle = (newTitle) => {
    document.title = newTitle;
}

window.setColorTheme = (isDarkTheme) => {
    localStorage.setItem("dark-mode", isDarkTheme);
}

window.getColorTheme = () => {
    var x = localStorage.getItem("dark-mode");

    return x;
}