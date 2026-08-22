import { HttpInterceptorFn } from '@angular/common/http';
import { ToastService } from '../services/toast-service';
import { inject, model } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const toastService = inject(ToastService);
  const routerService = inject(Router);

  return next(req).pipe(
    catchError(error => {
      if (error) {
        switch (error.status) {
          case 400:
            if (error.error.errors) {
              const modelStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key])
                  modelStateErrors.push(error.error.errors[key]);   
              }
              throw modelStateErrors.flat();
            } else {
              toastService.error(`Bad request: ${error.error}`);
            }
            break;
          case 401:
              toastService.error('Not authorized');
              break;
          case 404:
              routerService.navigateByUrl('/not-found');
              break;
          case 500:
              const navigationExtras: NavigationExtras = { state: { error: error.errors }};
              routerService.navigateByUrl('/server-error', navigationExtras);
              break;
          default:
              toastService.error(`Other error: ${error.status} ${error.error}`);
            break;
        }
      }
      throw error;
    })
  )
};
