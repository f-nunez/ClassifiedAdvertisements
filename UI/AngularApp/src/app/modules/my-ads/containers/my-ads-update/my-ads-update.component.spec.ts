import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAdsUpdateComponent } from './my-ads-update.component';

describe('MyAdsUpdateComponent', () => {
  let component: MyAdsUpdateComponent;
  let fixture: ComponentFixture<MyAdsUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAdsUpdateComponent]
    });
    fixture = TestBed.createComponent(MyAdsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
