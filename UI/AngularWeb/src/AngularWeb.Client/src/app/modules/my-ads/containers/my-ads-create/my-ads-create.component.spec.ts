import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAdsCreateComponent } from './my-ads-create.component';

describe('MyAdsCreateComponent', () => {
  let component: MyAdsCreateComponent;
  let fixture: ComponentFixture<MyAdsCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAdsCreateComponent]
    });
    fixture = TestBed.createComponent(MyAdsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
