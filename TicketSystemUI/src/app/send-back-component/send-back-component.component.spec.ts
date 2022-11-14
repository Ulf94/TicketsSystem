import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendBackComponentComponent } from './send-back-component.component';

describe('SendBackComponentComponent', () => {
  let component: SendBackComponentComponent;
  let fixture: ComponentFixture<SendBackComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SendBackComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SendBackComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
