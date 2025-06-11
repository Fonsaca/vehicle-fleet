import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('../features/vehicle-mgmt/vehicle-mgmt-routing.module').then(m => m.VehicleMgmtRoutingModule)
    }
];
