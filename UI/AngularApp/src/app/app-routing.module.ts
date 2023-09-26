import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppThemeComponent } from '@theme/app-theme/app-theme.component';
import { PublicThemeComponent } from '@theme/public-theme/public-theme.component';

const routes: Routes = [
  {
    path: '',
    component: PublicThemeComponent,
    loadChildren: () => import('@theme/public-theme/public-theme.module').then(module => module.PublicThemeModule)
  },
  {
    path: 'app',
    component: AppThemeComponent,
    loadChildren: () => import('@theme/app-theme/app-theme.module').then(module => module.AppThemeModule)
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
