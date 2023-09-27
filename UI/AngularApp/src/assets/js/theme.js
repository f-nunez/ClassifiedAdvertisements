(() => {
    'use strict';

    // Theme color
    const initializeThemeColor = () => {
        showActiveTheme(getPreferredTheme());

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value');
                    setStoredTheme(theme);
                    setTheme(theme);
                    showActiveTheme(theme, true);
                })
            });
    }

    const getStoredTheme = () => localStorage.getItem('theme_color');

    const setStoredTheme = theme => localStorage.setItem('theme_color', theme);

    const getPreferredTheme = () => {
        const storedTheme = getStoredTheme();

        if (storedTheme)
            return storedTheme;

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    const setTheme = theme => {
        if (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches)
            document.documentElement.setAttribute('data-bs-theme', 'dark');
        else
            document.documentElement.setAttribute('data-bs-theme', theme);
    }

    const showActiveTheme = (theme, focus = false) => {
        const themeColorButton = document.querySelector('#themeColorButton');

        if (!themeColorButton)
            return

        const themeColorButtonText = document.querySelector('#themeColorButtonText');
        const themeColorButtonIcon = document.querySelector('#themeColorButtonIcon');
        const themeColorButtonOption = document.querySelector(`[data-bs-theme-value="${theme}"]`);
        const iconOfSelectedThemeColorButtonOption = themeColorButtonOption.querySelector('i').getAttribute('data-icon-name');

        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
            element.classList.remove('active');
            element.setAttribute('aria-pressed', 'false');
        });

        themeColorButtonOption.classList.add('active');
        themeColorButtonOption.setAttribute('aria-pressed', 'true');
        themeColorButtonIcon.setAttribute('class', `bi ${iconOfSelectedThemeColorButtonOption} me-2 theme-color-icon`);
        const themeColorButtonLabel = `${themeColorButtonText.textContent} (${themeColorButtonOption.dataset.bsThemeValue})`;
        themeColorButton.setAttribute('aria-label', themeColorButtonLabel);

        if (focus)
            themeColorButton.focus();
    }

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        const storedTheme = getStoredTheme();

        if (storedTheme !== 'light' && storedTheme !== 'dark')
            setTheme(getPreferredTheme());
    });

    // Button toggle
    const initializeButtonToggle = () => {
        setButtonTogglestatus(getButtonToggleStatus());

        const navToggleButton = document.querySelector('#navToggle');

        if (!navToggleButton)
            return;

        navToggleButton.addEventListener('click', () => {
            const mainWrapper = document.querySelector('#mainWrapper');

            if (!mainWrapper)
                return;

            const isToggled = mainWrapper.classList.contains('toggled');
            setStoredToggleStatus(!isToggled);
            setButtonTogglestatus(!isToggled);
        });
    }

    const getStoredToggleStatus = () => localStorage.getItem('toggle_status');

    const setStoredToggleStatus = isToggled => localStorage.setItem('toggle_status', isToggled);

    const getButtonToggleStatus = () => {
        const storedToggleStatus = getStoredToggleStatus();

        return storedToggleStatus && storedToggleStatus == 'true';
    }

    const setButtonTogglestatus = (togglestatus) => {
        const mainWrapper = document.querySelector('#mainWrapper');

        if (!mainWrapper)
            return;

        if (togglestatus) {
            mainWrapper.classList.add('toggled');
        } else {
            mainWrapper.classList.remove('toggled');
        }
    }

    initializeThemeColor();

    initializeButtonToggle();
})();