import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppThemeHeaderComponent } from './app-theme-header.component';

describe('AppThemeHeaderComponent', () => {
  let component: AppThemeHeaderComponent;
  let fixture: ComponentFixture<AppThemeHeaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppThemeHeaderComponent]
    });
    fixture = TestBed.createComponent(AppThemeHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
