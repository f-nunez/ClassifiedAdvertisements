import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Role } from '@core/enums/role';
import { roleGuard } from '@core/guards/role.guard';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadChildren: () => import('../../modules/home/home.module').then(module => module.HomeModule),
    canActivate: [roleGuard],
    data: { roles: [Role.Customer, Role.Manager, Role.Staff] }
  },
  {
    path: 'my-ads',
    loadChildren: () => import('../../modules/my-ads/my-ads.module').then(module => module.MyAdsModule),
    canActivate: [roleGuard],
    data: { roles: [Role.Customer, Role.Manager, Role.Staff] }
  },
  {
    path: 'users',
    loadChildren: () => import('../../modules/users/users.module').then(module => module.UsersModule),
    canActivate: [roleGuard],
    data: { roles: [Role.Staff] }
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AppThemeRoutingModule { }
