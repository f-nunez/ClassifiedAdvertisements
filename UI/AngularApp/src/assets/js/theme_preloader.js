(() => {
    'use strict';

    const preloadStoredThemeColor = () => {
        const storedTheme = localStorage.getItem('theme_color');

        if (!storedTheme)
            storedTheme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';

        if (storedTheme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches)
            document.documentElement.setAttribute('data-bs-theme', 'dark');
        else
            document.documentElement.setAttribute('data-bs-theme', storedTheme);
    }

    window.addEventListener('DOMContentLoaded', () => {
        preloadStoredThemeColor();
    });
})();