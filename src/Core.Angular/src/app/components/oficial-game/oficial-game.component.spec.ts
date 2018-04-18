import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OficialGameComponent } from './oficial-game.component';

describe('OficialGameComponent', () => {
  let component: OficialGameComponent;
  let fixture: ComponentFixture<OficialGameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OficialGameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OficialGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
