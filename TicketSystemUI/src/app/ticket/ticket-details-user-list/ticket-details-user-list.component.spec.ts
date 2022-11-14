import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketDetailsUserListComponent } from './ticket-details-user-list.component';

describe('TicketDetailsUserListComponent', () => {
  let component: TicketDetailsUserListComponent;
  let fixture: ComponentFixture<TicketDetailsUserListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TicketDetailsUserListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketDetailsUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
