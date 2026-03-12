import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCountry } from './add-country';

describe('AddCountry', () => {
  let component: AddCountry;
  let fixture: ComponentFixture<AddCountry>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCountry]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCountry);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
