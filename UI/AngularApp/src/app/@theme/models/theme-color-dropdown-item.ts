export class ThemeColorDropdownItem {
    iconClass: string = '';
    isSelected: boolean = false;
    text: string = '';
    themeColor: string = '';

    constructor(iconClass: string, isSelected: boolean, text: string, themeColor: string) {
        this.iconClass = iconClass;
        this.isSelected = isSelected;
        this.text = text;
        this.themeColor = themeColor;
    }
}