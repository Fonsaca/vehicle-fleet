import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehicleMgmtModule } from './vehicle-mgmt.module';
import { IndexComponent } from './components/index/index.component';

const routes: Routes = [{
  path: '',
  component: IndexComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes), VehicleMgmtModule],
  exports: [RouterModule]
})
export class VehicleMgmtRoutingModule { }
