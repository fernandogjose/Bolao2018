import { TestBed, inject } from '@angular/core/testing';

import { OficialGameService } from './oficial-game.service';

describe('OficialGameService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OficialGameService]
    });
  });

  it('should be created', inject([OficialGameService], (service: OficialGameService) => {
    expect(service).toBeTruthy();
  }));
});
