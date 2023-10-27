import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAdsDetailComponent } from './my-ads-detail.component';

describe('MyAdsDetailComponent', () => {
  let component: MyAdsDetailComponent;
  let fixture: ComponentFixture<MyAdsDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAdsDetailComponent]
    });
    fixture = TestBed.createComponent(MyAdsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
