import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAdsListComponent } from './containers/my-ads-list/my-ads-list.component';
import { MyAdsCreateComponent } from './containers/my-ads-create/my-ads-create.component';
import { MyAdsUpdateComponent } from './containers/my-ads-update/my-ads-update.component';

const routes: Routes = [
  { path: '', component: MyAdsListComponent },
  { path: 'create', component: MyAdsCreateComponent },
  { path: 'update/:id', component: MyAdsUpdateComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyAdsRoutingModule { }
