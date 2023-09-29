import { Injectable } from '@angular/core';
import { ThemeColorDropdownItem } from '@theme/interfaces/theme-color-dropdown-item';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ThemeColorService {
    private readonly defaultDropdownItem: ThemeColorDropdownItem = { iconClass: 'bi bi-circle-half', isSelected: true, text: 'Auto', themeColor: 'auto' };
    private dropdownItems: ThemeColorDropdownItem[];
    private dropdownItems$: BehaviorSubject<ThemeColorDropdownItem[]>;
    private selectedDropdownItem: ThemeColorDropdownItem;
    private selectedDropdownItem$: BehaviorSubject<ThemeColorDropdownItem>;

    constructor() {
        this.dropdownItems = [];
        this.dropdownItems$ = new BehaviorSubject<ThemeColorDropdownItem[]>([]);
        this.selectedDropdownItem = this.defaultDropdownItem;
        this.selectedDropdownItem$ = new BehaviorSubject<ThemeColorDropdownItem>(this.defaultDropdownItem)
        this.initializeThemeColorDropdown();
    }

    public setSelectedColor(selectedItem: ThemeColorDropdownItem): void {
        selectedItem.isSelected = true;

        this.dropdownItems.forEach(item => {
            if (item.themeColor === selectedItem.themeColor)
                item = selectedItem;
            else
                item.isSelected = false;
        });

        this.selectedDropdownItem = selectedItem;
        this.setSelectedDropdownItemObservable(this.selectedDropdownItem);
        this.setDropdownItemsObservable(this.dropdownItems);
        this.setThemeColorToLocalStore(selectedItem.themeColor);
        document.documentElement.setAttribute('data-bs-theme', selectedItem.themeColor);
    }

    public getDropdownItemsObservable(): Observable<ThemeColorDropdownItem[]> {
        return this.dropdownItems$.asObservable();
    }

    private setDropdownItemsObservable(items: ThemeColorDropdownItem[]) {
        this.dropdownItems$.next(items);
    }

    public getSelectedDropdownItemObservable(): Observable<ThemeColorDropdownItem> {
        return this.selectedDropdownItem$.asObservable();
    }

    private setSelectedDropdownItemObservable(selectedItem: ThemeColorDropdownItem) {
        this.selectedDropdownItem$.next(selectedItem);
    }

    private initializeThemeColorDropdown(): void {
        const themeColor = this.getThemeColorFromLocalStore();

        this.dropdownItems = [
            { iconClass: 'bi bi-sun-fill', isSelected: false, text: 'Light', themeColor: 'light' },
            { iconClass: 'bi bi-moon-stars-fill', isSelected: false, text: 'Dark', themeColor: 'dark' },
            { iconClass: 'bi bi-circle-half', isSelected: false, text: 'Auto', themeColor: 'auto' }
        ];

        let selectedThemeColorDropdownItem = this.defaultDropdownItem;

        this.dropdownItems.forEach(item => {
            if (item.themeColor === themeColor) {
                item.isSelected = true;
                selectedThemeColorDropdownItem = item;
                return;
            }
        });

        this.setDropdownItemsObservable(this.dropdownItems);
        this.setSelectedDropdownItemObservable(selectedThemeColorDropdownItem);
    }

    private getThemeColorFromLocalStore(): string {
        const storedThemeColor = localStorage.getItem('theme_color');

        if (storedThemeColor)
            return storedThemeColor;

        return 'auto';
    }

    private setThemeColorToLocalStore(themeColor: string): void {
        localStorage.setItem('theme_color', themeColor);
    }
}
