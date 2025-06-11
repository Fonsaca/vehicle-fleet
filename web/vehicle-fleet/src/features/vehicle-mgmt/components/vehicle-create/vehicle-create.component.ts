import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { VehicleCreate } from '../../../../rest-apis/vehicle/vehicle.model';
import { MessageService } from 'primeng/api';
import { VehicleMgmtService } from '../../services/vehicle-mgmt.service';
import { Subject, takeUntil } from 'rxjs';
import {  VEHICLE_TYPES } from '../../constants/type.constant';
import { VEHICLE_COLORS } from '../../constants/color.constant';

@Component({
  selector: 'vehicle-form',
  templateUrl: './vehicle-create.component.html',
  styleUrl: './vehicle-create.component.css'
})
export class VehicleCreateComponent implements OnInit,OnDestroy {

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
  set showing(value){ this.vehicleMgmtService.createModalState.next(value); }

  
  loading = signal(false);

  form = this.fb.group({
    chassisSerie: this.fb.control('', Validators.required),
    chassisNumber: this.fb.control(1, [Validators.required, Validators.min(0), Validators.max(4294967295)]),
    color: this.fb.control('', Validators.required),
    type: this.fb.control('', Validators.required),
  })

  colors = VEHICLE_COLORS;

  types = VEHICLE_TYPES;

  get formChassisSerie() {
    return this.form.controls['chassisSerie'];
  }
  get formChassisNumber() {
    return this.form.controls['chassisNumber'];
  }
  get formType() {
    return this.form.controls['type'];
  }
  get formColor() {
    return this.form.controls['color'];
  }

  public isInvalid(control : FormControl<any>){
    return control?.invalid && (control?.touched || control?.dirty)
  }

  async create() : Promise<void>{

    if(this.form.invalid){
      this.notify.add({ severity: 'error', summary: 'Create Vehicle', detail:`Incomplete form` });
      this.form.markAllAsTouched();
      return;
    }

    var values = this.form.value;

    const vehicleCreate = <VehicleCreate>{
      chassisId: {
        serie: values.chassisSerie!,
        number: values.chassisNumber!
      },
      color: values.color!,
      type: values.type!
    }

    const result = await this.vehicleMgmtService.create(vehicleCreate, this.loading);
    if(result.isSuccess){
      this.vehicleMgmtService.createModalState.next(false);
      this.form.reset();
      await this.vehicleMgmtService.loadVehicles();
    }
  }

  private registerModalState() {
    this.vehicleMgmtService.createModalState
      .pipe(takeUntil(this.lifecycle))
      .subscribe((value) => {
        this._showing = value;
      });
  }
}
