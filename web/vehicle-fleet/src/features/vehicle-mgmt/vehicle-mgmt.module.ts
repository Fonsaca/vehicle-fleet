import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { IndexComponent } from './components/index/index.component';
import { VehicleTableComponent } from './components/vehicle-table/vehicle-table.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { VehicleMgmtService } from './services/vehicle-mgmt.service';
import { VehicleCreateComponent } from './components/vehicle-create/vehicle-create.component';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectButtonModule } from 'primeng/selectbutton';
import { DropdownModule } from 'primeng/dropdown';
import { VehicleEditComponent } from './components/vehicle-edit/vehicle-edit.component';
import { VehicleFinderComponent } from './components/vehicle-finder/vehicle-finder.component';
import { LoadingComponent } from '../../components/loading/loading.component';


@NgModule({
  declarations: [
    IndexComponent,
    VehicleTableComponent,
    VehicleCreateComponent,
    VehicleEditComponent,
    VehicleFinderComponent
  ],
  imports: [
    CommonModule,
    TableModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    LoadingComponent
  ],
  providers:[
    VehicleMgmtService
  ]
})
export class VehicleMgmtModule { }
