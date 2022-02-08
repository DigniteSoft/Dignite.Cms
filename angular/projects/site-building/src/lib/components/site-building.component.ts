import { Component, OnInit } from '@angular/core';
import { SiteBuildingService } from '../services/site-building.service';

@Component({
  selector: 'lib-site-building',
  template: ` <p>site-building works!</p> `,
  styles: [],
})
export class SiteBuildingComponent implements OnInit {
  constructor(private service: SiteBuildingService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
