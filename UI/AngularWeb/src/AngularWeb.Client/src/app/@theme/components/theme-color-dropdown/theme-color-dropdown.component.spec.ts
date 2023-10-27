import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemeColorDropdownComponent } from './theme-color-dropdown.component';

describe('ThemeColorDropdownComponent', () => {
  let component: ThemeColorDropdownComponent;
  let fixture: ComponentFixture<ThemeColorDropdownComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ThemeColorDropdownComponent]
    });
    fixture = TestBed.createComponent(ThemeColorDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
