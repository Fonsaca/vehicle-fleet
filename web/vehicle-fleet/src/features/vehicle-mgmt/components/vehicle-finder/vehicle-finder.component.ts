import { Component, signal } from '@angular/core';
import { MessageService } from 'primeng/api';
import { VehicleMgmtApiService } from '../../../../rest-apis/vehicle/vehicle-mgmt-api.service';
import { VehicleMgmtService } from '../../services/vehicle-mgmt.service';

@Component({
  selector: 'vehicle-finder',
  templateUrl: './vehicle-finder.component.html',
  styleUrl: './vehicle-finder.component.css'
})
export class VehicleFinderComponent {


  constructor(private notify: MessageService, private service: VehicleMgmtService) {}
  
  loading = signal(false);

  chassisNumber = 0;
  chassisSerie: string = '';

  async search(serie: string, number: number): Promise<void>{

    if(serie?.length == 0){
      this.notify.add({ severity: 'error', summary: 'Missing Chassis Serie', detail:`Chassis Serie is empty` });
      return;
    }

    if(Number.isNaN(number)){
      this.notify.add({ severity: 'error', summary: 'Missing Chassis Number', detail:`Chassis Number is empty` });
      return;
    }

    const vehicle = await this.service.search({serie, number}, this.loading);

    if(!vehicle)
      this.notify.add({ severity: 'error', summary: 'Find Vehicle', detail: 'Vehicle was not found' });
    else
      this.service.editModalState.next({ showing: true, vehicle });
  }


}
