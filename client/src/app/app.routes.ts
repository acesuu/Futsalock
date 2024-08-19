import { Routes } from '@angular/router';
import { SignUpComponent } from './components/auth/signup/signup.component';
import { SignInComponent } from './components/auth/signin/signin.component';
import { GroundListComponent } from './components/ground/components/ground-list/ground-list.component';

export const routes: Routes = [
    { path: 'signup', component: SignUpComponent },
    { path: 'signin', component: SignInComponent },
    { path: 'ground-list', component: GroundListComponent },
];
