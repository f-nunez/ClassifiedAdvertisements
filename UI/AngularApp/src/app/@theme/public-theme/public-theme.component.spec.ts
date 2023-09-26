import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicThemeComponent } from './public-theme.component';

describe('PublicThemeComponent', () => {
  let component: PublicThemeComponent;
  let fixture: ComponentFixture<PublicThemeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicThemeComponent]
    });
    fixture = TestBed.createComponent(PublicThemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
