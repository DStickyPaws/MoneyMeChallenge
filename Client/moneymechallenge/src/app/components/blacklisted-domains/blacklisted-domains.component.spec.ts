import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlacklistedDomainsComponent } from './blacklisted-domains.component';

describe('BlacklistedDomainsComponent', () => {
  let component: BlacklistedDomainsComponent;
  let fixture: ComponentFixture<BlacklistedDomainsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlacklistedDomainsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlacklistedDomainsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
