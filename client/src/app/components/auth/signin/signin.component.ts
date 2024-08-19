import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class SignInComponent {
  signInForm: FormGroup;
  errorMessage: string | null = null;
  isSubmitting: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.signInForm.valid) {
      this.isSubmitting = true;
      this.authService.signIn(this.signInForm.value).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);  // Store the token in local storage
          this.router.navigate(['/']);  // Redirect to home or dashboard after successful sign-in
          this.isSubmitting = false;
        },
        error: (err) => {
          this.errorMessage = err.error.message || 'Sign-in failed. Please try again.';
          this.isSubmitting = false;
        }
      });
    }
  }
}
