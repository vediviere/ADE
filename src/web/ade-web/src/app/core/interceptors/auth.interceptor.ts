import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const token = auth.token();

  const isAuthEndpoint =
    req.url.includes('/api/Auth/login') ||
    req.url.includes('/api/Auth/register') ||
    req.url.includes('/api/auth/login') ||
    req.url.includes('/api/auth/register');

  if (isAuthEndpoint) {
    return next(req);
  }

  if (token) {
    const cloned = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` },
    });
    return next(cloned);
  }

  return next(req);
};
