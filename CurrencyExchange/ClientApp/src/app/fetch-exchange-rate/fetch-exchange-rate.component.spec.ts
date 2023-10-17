import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchExchangeRateComponent } from './fetch-exchange-rate.component';

describe('FetchExchangeRateComponent', () => {
  let component: FetchExchangeRateComponent;
  let fixture: ComponentFixture<FetchExchangeRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FetchExchangeRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FetchExchangeRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
