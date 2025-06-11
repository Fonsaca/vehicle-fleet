import { Component, signal } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { VehicleEdit } from '../../../../rest-apis/vehicle/vehicle.model';
import { MessageService } from 'primeng/api';
import { VehicleMgmtService } from '../../services/vehicle-mgmt.service';
import { Subject, takeUntil } from 'rxjs';
import { VEHICLE_COLORS } from '../../constants/color.constant';

@Component({
  selector: 'vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrl: './vehicle-edit.component.css'
})
export class VehicleEditComponent {

  constructor(private fb: FormBuilder, private notify: MessageService
    , private vehicleMgmtService: VehicleMgmtService){}


  ngOnInit(): void {
    this.registerModalState()
  }

  ngOnDestroy(): void {
    this.lifecycle.next();
    this.lifecycle.complete();
  }

  private lifecycle = new Subject<void>()

  private _showing = false;
  get showing(){ return this._showing; }
  set showing(value){ this.vehicleMgmtService.editModalState.next(
    {
      showing: value, 
      vehicle: this.vehicleMgmtService.editModalState.getValue().vehicle
    }); 

  }

  
  loading = signal(false);

  form = this.fb.group({
    chassisSerie: this.fb.control('', Validators.required),
    chassisNumber: this.fb.control(1, [Validators.required, Validators.min(0), Validators.max(4294967295)]),
    color: this.fb.control('', Validators.required)
  })

  
  colors = VEHICLE_COLORS;
  

  get formColor() {
    return this.form.controls['color'];
  }

  isInvalid(control : FormControl<any>){
    return control?.invalid && (control?.touched || control?.dirty)
  }

  async edit() : Promise<void>{

    if(this.form.invalid){
      this.notify.add({ severity: 'error', summary: 'Edit Vehicle', detail:`Incomplete form` });
      this.form.markAllAsTouched();
      return;
    }

    var values = this.form.value;

    const vehicleEdit = <VehicleEdit>{
      chassisId: {
        serie: values.chassisSerie!,
        number: values.chassisNumber!
      },
      color: values.color!
    }

    const result = await this.vehicleMgmtService.edit(vehicleEdit, this.loading);
    if(result.isSuccess){
      this.vehicleMgmtService.editModalState.next({showing:false});
      this.form.reset();
      await this.vehicleMgmtService.loadVehicles();
    }
  }

  private registerModalState() {
    this.vehicleMgmtService.editModalState
      .pipe(takeUntil(this.lifecycle))
      .subscribe((state) => {
        this._showing = state.showing;
        this.form.patchValue({
          chassisSerie: state.vehicle!.chassisId.serie,
          chassisNumber: state.vehicle!.chassisId.number,
          color: state.vehicle!.color
        });
      });
  }
}
