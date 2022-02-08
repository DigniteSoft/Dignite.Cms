import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { SiteBuildingComponent } from './components/site-building.component';
import { SiteBuildingRoutingModule } from './site-building-routing.module';

@NgModule({
  declarations: [SiteBuildingComponent],
  imports: [CoreModule, ThemeSharedModule, SiteBuildingRoutingModule],
  exports: [SiteBuildingComponent],
})
export class SiteBuildingModule {
  static forChild(): ModuleWithProviders<SiteBuildingModule> {
    return {
      ngModule: SiteBuildingModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<SiteBuildingModule> {
    return new LazyModuleFactory(SiteBuildingModule.forChild());
  }
}
