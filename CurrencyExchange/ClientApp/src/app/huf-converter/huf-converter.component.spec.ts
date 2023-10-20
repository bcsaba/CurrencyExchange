import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HufConverterComponent } from './huf-converter.component';

describe('HufConverterComponent', () => {
  let component: HufConverterComponent;
  let fixture: ComponentFixture<HufConverterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HufConverterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HufConverterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
