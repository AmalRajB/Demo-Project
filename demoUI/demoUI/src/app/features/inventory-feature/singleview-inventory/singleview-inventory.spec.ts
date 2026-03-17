import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleviewInventory } from './singleview-inventory';

describe('SingleviewInventory', () => {
  let component: SingleviewInventory;
  let fixture: ComponentFixture<SingleviewInventory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SingleviewInventory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleviewInventory);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
