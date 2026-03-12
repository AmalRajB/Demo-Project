import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFileComponent } from './edit-file-component';

describe('EditFileComponent', () => {
  let component: EditFileComponent;
  let fixture: ComponentFixture<EditFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditFileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditFileComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
