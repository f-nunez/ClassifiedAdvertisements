import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppThemeSidebarComponent } from './app-theme-sidebar.component';

describe('AppThemeSidebarComponent', () => {
  let component: AppThemeSidebarComponent;
  let fixture: ComponentFixture<AppThemeSidebarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppThemeSidebarComponent]
    });
    fixture = TestBed.createComponent(AppThemeSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
