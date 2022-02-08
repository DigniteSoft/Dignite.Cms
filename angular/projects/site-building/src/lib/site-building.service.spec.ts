import { TestBed } from '@angular/core/testing';

import { SiteBuildingService } from './site-building.service';

describe('SiteBuildingService', () => {
  let service: SiteBuildingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteBuildingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
