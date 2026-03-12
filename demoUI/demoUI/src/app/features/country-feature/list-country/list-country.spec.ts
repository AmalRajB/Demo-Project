import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCountry } from './list-country';

describe('ListCountry', () => {
  let component: ListCountry;
  let fixture: ComponentFixture<ListCountry>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListCountry]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListCountry);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
