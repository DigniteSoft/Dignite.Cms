import { ModuleWithProviders, NgModule } from '@angular/core';
import { SITE_BUILDING_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class SiteBuildingConfigModule {
  static forRoot(): ModuleWithProviders<SiteBuildingConfigModule> {
    return {
      ngModule: SiteBuildingConfigModule,
      providers: [SITE_BUILDING_ROUTE_PROVIDERS],
    };
  }
}
