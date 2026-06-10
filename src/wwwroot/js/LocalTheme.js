window.LocalTheme = {
    setIsDarkMode: function (isDarkMode) {
        if (isDarkMode) {
            window.document.body.classList.add("dark");
        } else {
            window.document.body.classList.remove("dark");
        }
    },
    isBrowserDarkMode: function () {
        const prefersDarkQuery = "(prefers-color-scheme: dark)"
        return window.matchMedia(prefersDarkQuery).matches
    }
}
