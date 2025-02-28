import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError(response => {
      if (response) {
        switch (response.status) {
          case 400:
            if (response.error.errors) {
              const modalStateErrors = [];
              for (const key in response.error.errors)
              {
                if (response.error.errors[key]) {
                  modalStateErrors.push(response.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            } else {
              toastr.error(response.error, response.status);
            }
            break;
          
          case 401:
            toastr.error("Unauthorized", response.status);
            break;
          
          case 404:
            router.navigateByUrl('/not-found');
            break;

          case 500:
            const navigationExtras: NavigationExtras = {state: {error: response.error}};
            router.navigateByUrl('/server-error', navigationExtras);
            break;

          default:
            toastr.error('Something unexcepted occurred');
            break;
        }
      }

      throw response;
    })
  )
};
