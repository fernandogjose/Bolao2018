import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BetByGameComponent } from './bet-by-game.component';

describe('BetByGameComponent', () => {
  let component: BetByGameComponent;
  let fixture: ComponentFixture<BetByGameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BetByGameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BetByGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
