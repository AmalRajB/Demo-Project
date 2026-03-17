import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListInventory } from './list-inventory';

describe('ListInventory', () => {
  let component: ListInventory;
  let fixture: ComponentFixture<ListInventory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListInventory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListInventory);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
