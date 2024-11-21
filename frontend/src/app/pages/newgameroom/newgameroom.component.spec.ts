import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewgameroomComponent } from './newgameroom.component';

describe('NewgameroomComponent', () => {
  let component: NewgameroomComponent;
  let fixture: ComponentFixture<NewgameroomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewgameroomComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewgameroomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
