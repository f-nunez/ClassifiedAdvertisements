import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ThemeToggleService {
    private isToggled: boolean = false;

    public initializeToggle(): void {
        const isToggled = this.getToggleStatusFromLocalStorage();
        this.isToggled = isToggled;
        this.setToggleStatusHtmlClass(isToggled);
    }

    public toggle(): void {
        this.isToggled = !this.isToggled;
        this.setToggleStatusHtmlClass(this.isToggled);
        this.setToggleStatusToLocalStorage(this.isToggled);
    }

    private setToggleStatusHtmlClass(isToggled: boolean): void {
        const mainWrapper = document.querySelector('#mainWrapper');

        if (!mainWrapper)
            return;

        if (isToggled)
            mainWrapper.classList.add('toggled');
        else
            mainWrapper.classList.remove('toggled');
    }

    private getToggleStatusFromLocalStorage(): boolean {
        const toggleStatus = localStorage.getItem('theme_toggle_status');

        if (toggleStatus)
            return toggleStatus === `${true}`;

        return false;
    }

    private setToggleStatusToLocalStorage(isToggled: boolean): void {
        localStorage.setItem('theme_toggle_status', `${isToggled}`);
    }
}
