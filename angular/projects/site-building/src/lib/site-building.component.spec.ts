import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SiteBuildingComponent } from './site-building.component';

describe('SiteBuildingComponent', () => {
  let component: SiteBuildingComponent;
  let fixture: ComponentFixture<SiteBuildingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteBuildingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteBuildingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
