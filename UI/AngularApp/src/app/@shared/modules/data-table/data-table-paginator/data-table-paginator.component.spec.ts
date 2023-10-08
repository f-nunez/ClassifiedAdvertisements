import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataTablePaginatorComponent } from './data-table-paginator.component';

describe('DataTablePaginatorComponent', () => {
  let component: DataTablePaginatorComponent;
  let fixture: ComponentFixture<DataTablePaginatorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DataTablePaginatorComponent]
    });
    fixture = TestBed.createComponent(DataTablePaginatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
