import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import {provideAnimations} from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MessageService} from 'primeng/api';
import { globalInterceptor } from '../infra/global-interceptor.function';
import { BASE_URL } from '../rest-apis/shared/base-url.token';
import { IVehicleMgmtApiService } from '../rest-apis/vehicle/ivehicle-mgmt-api.service';
import { VehicleMgmtApiService } from '../rest-apis/vehicle/vehicle-mgmt-api.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([globalInterceptor])),
    provideAnimations(),
    MessageService,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: BASE_URL, useValue: 'http://localhost:25001' },
    { provide: IVehicleMgmtApiService, useClass: VehicleMgmtApiService }
  ]
};
