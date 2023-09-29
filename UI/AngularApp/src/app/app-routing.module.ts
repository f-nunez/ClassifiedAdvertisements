import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppThemeComponent } from '@theme/app-theme/app-theme.component';
import { PublicThemeComponent } from '@theme/public-theme/public-theme.component';

import { appThemeGuard } from '@core/guards/app-theme.guard';
import { publicThemetGuard } from '@core/guards/public-theme.guard';

import { SigninCallbackComponent } from './signin-callback/signin-callback.component';

const routes: Routes = [
  {
    path: '',
    component: PublicThemeComponent,
    loadChildren: () => import('@theme/public-theme/public-theme.module').then(module => module.PublicThemeModule),
    canActivate: [publicThemetGuard]
  },
  {
    path: 'app',
    component: AppThemeComponent,
    loadChildren: () => import('@theme/app-theme/app-theme.module').then(module => module.AppThemeModule),
    canActivate: [appThemeGuard]
  },
  { path: 'signin-callback', component: SigninCallbackComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
