import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ThemeColorDropdownItem } from '@theme/models/theme-color-dropdown-item';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-theme-color-dropdown',
  templateUrl: './theme-color-dropdown.component.html',
  styleUrls: ['./theme-color-dropdown.component.css']
})
export class ThemeColorDropdownComponent implements OnInit {
  @Input() dropdownItems$?: Observable<ThemeColorDropdownItem[]>;
  @Input() selectedDropdownItem$?: Observable<ThemeColorDropdownItem>;
  @Output() selectColor = new EventEmitter<ThemeColorDropdownItem>();
  dropdownItems?: ThemeColorDropdownItem[];
  selectedDropdownItem?: ThemeColorDropdownItem;

  ngOnInit(): void {
    this.dropdownItems$?.subscribe(val => this.dropdownItems = val);
    this.selectedDropdownItem$?.subscribe(val => this.selectedDropdownItem = val);
  }

  onSelectColor(item: ThemeColorDropdownItem) {
    this.selectColor.emit(item);
  }
}
