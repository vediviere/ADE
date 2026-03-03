import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  email = '';
  password = '';
  rol = 'Estudiante';
  error = signal('');
  loading = signal(false);

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit() {
    this.error.set('');
    this.loading.set(true);

    this.auth.register({ email: this.email, password: this.password, rol: this.rol }).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: () => {
        this.error.set('El email ya está registrado o hubo un error.');
        this.loading.set(false);
      }
    });
  }
}
