import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAdsListComponent } from './containers/my-ads-list/my-ads-list.component';
import { MyAdsCreateComponent } from './containers/my-ads-create/my-ads-create.component';

const routes: Routes = [
  { path: '', component: MyAdsListComponent },
  { path: 'create', component: MyAdsCreateComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyAdsRoutingModule { }
