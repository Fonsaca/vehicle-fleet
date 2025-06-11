import { HttpInterceptorFn } from "@angular/common/http";


export const globalInterceptor: HttpInterceptorFn = (req, next) => {
    let headers;

    headers = req.headers.set('Content-Type', 'application/json');
    headers = req.headers.set('Accept', 'application/json');

    return next(req.clone({ headers }));
}