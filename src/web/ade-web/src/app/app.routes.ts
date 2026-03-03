import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

// Si tus componentes son STANDALONE (lo más común en Angular 17+)
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './features/auth/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },

  // Fallback
  { path: '**', redirectTo: 'login' },
];