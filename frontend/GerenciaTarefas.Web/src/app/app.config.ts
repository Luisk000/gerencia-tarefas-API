import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

import { provideToastr } from 'ngx-toastr';
import { AuthInterceptor } from './auth-interceptor.interceptor';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';


export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideToastr({
      positionClass: 'toast-center-center'
    }),
    provideHttpClient(withInterceptorsFromDi()), {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
    
  ]
};
