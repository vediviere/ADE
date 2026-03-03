import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  email = '';
  password = '';
  error = signal('');
  loading = signal(false);

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit() {
    this.error.set('');
    this.loading.set(true);

    this.auth.login({ email: this.email, password: this.password }).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: () => {
        this.error.set('Credenciales inválidas. Verifica tu email y contraseña.');
        this.loading.set(false);
      }
    });
  }
}
