import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SigninComponent } from './signin/signin.component';
import { SignupComponent } from './signup/signup.component';
import { SignoutComponent } from './signout/signout.component';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
  declarations: [
    
  ],
  imports: [
    SigninComponent,
    SignupComponent,
    SignoutComponent,
    CommonModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
