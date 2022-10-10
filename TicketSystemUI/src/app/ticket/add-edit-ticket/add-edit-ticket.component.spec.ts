import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditTicketComponent } from './add-edit-ticket.component';

describe('AddEditTicketComponent', () => {
  let component: AddEditTicketComponent;
  let fixture: ComponentFixture<AddEditTicketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditTicketComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
