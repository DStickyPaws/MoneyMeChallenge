import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlackListedMobileNumbersComponent } from './black-listed-mobile-numbers.component';

describe('BlackListedMobileNumbersComponent', () => {
  let component: BlackListedMobileNumbersComponent;
  let fixture: ComponentFixture<BlackListedMobileNumbersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlackListedMobileNumbersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlackListedMobileNumbersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
