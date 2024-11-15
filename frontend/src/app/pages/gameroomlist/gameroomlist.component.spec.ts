import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameroomlistComponent } from './gameroomlist.component';

describe('GameroomlistComponent', () => {
  let component: GameroomlistComponent;
  let fixture: ComponentFixture<GameroomlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameroomlistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameroomlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
