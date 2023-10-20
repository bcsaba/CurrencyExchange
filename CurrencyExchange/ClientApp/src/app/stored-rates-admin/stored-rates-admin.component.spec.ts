import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoredRatesAdminComponent } from './stored-rates-admin.component';

describe('StoredRatesAdminComponent', () => {
  let component: StoredRatesAdminComponent;
  let fixture: ComponentFixture<StoredRatesAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StoredRatesAdminComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StoredRatesAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
