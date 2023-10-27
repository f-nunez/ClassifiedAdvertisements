import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeColorDropdownComponent } from './theme-color-dropdown/theme-color-dropdown.component';

export const themeComponents: Type<any>[] = [ThemeColorDropdownComponent];

@NgModule({
  imports: [CommonModule],
  declarations: [...themeComponents],
  exports: [...themeComponents]
})
export class ThemeComponentsModule { }
