import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAdsListComponent } from './containers/my-ads-list/my-ads-list.component';

const routes: Routes = [{ path: '', component: MyAdsListComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyAdsRoutingModule { }
