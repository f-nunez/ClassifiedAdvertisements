import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicThemeHeaderComponent } from './public-theme-header.component';

describe('PublicThemeHeaderComponent', () => {
  let component: PublicThemeHeaderComponent;
  let fixture: ComponentFixture<PublicThemeHeaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicThemeHeaderComponent]
    });
    fixture = TestBed.createComponent(PublicThemeHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
