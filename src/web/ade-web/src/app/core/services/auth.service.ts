import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  email: string;
  password: string;
  rol: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  rol: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly API = `${environment.apiUrls.seguridad}/auth`;

  private _token = signal<string | null>(localStorage.getItem('token'));
  private _email = signal<string | null>(localStorage.getItem('email'));
  private _rol = signal<string | null>(localStorage.getItem('rol'));

  readonly token = this._token.asReadonly();
  readonly email = this._email.asReadonly();
  readonly rol = this._rol.asReadonly();
  readonly isAuthenticated = computed(() => !!this._token());

  constructor(private http: HttpClient, private router: Router) {}

  login(dto: LoginDto) {
    return this.http.post<AuthResponse>(`${this.API}/login`, dto).pipe(
      tap(res => this.saveSession(res))
    );
  }

  register(dto: RegisterDto) {
    return this.http.post<AuthResponse>(`${this.API}/register`, dto).pipe(
      tap(res => this.saveSession(res))
    );
  }

  logout() {
    localStorage.clear();
    this._token.set(null);
    this._email.set(null);
    this._rol.set(null);
    this.router.navigate(['/login']);
  }

  private saveSession(res: AuthResponse) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('email', res.email);
    localStorage.setItem('rol', res.rol);
    this._token.set(res.token);
    this._email.set(res.email);
    this._rol.set(res.rol);
  }
}
