import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchExchangeRateComponent } from './fetch-exchange-rate.component';
import {HttpClientModule} from "@angular/common/http";

describe('FetchExchangeRateComponent', () => {
  let component: FetchExchangeRateComponent;
  let fixture: ComponentFixture<FetchExchangeRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        { provide: 'BASE_URL', useValue: 'http://localhost' }
      ],
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
