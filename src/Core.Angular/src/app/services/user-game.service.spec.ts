import { TestBed, inject } from '@angular/core/testing';

import { UserGameService } from './user-game.service';

describe('UserGameService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserGameService]
    });
  });

  it('should be created', inject([UserGameService], (service: UserGameService) => {
    expect(service).toBeTruthy();
  }));
});
