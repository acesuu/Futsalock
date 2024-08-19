import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class SignUpComponent {
  signUpForm: FormGroup;
  errorMessage: string | null = null;
  isSubmitting: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.signUpForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]], // Phone validation for 10 digits
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.signUpForm.valid) {
      this.isSubmitting = true;
      
      const formData = {
        ...this.signUpForm.value,
        isAdmin: true  // Automatically set isAdmin to true
      };
  
      this.authService.signUp(formData).subscribe({
        next: () => {
          this.router.navigate(['/signin']);  // Redirect to sign-in page after successful sign-up
          this.isSubmitting = false;
        },
        error: (err) => {
          // Check if err and err.error exist before accessing err.error.message
          if (err && err.error && err.error.message) {
            this.errorMessage = err.error.message;
          } else {
            // Provide a generic error message in case err.error.message doesn't exist
            this.errorMessage = 'Sign-up failed. Please try again.';
          }
          this.isSubmitting = false;
        }
      });
    }
  }
}
