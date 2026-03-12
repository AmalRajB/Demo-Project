import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditState } from './edit-state';

describe('EditState', () => {
  let component: EditState;
  let fixture: ComponentFixture<EditState>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditState]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditState);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
