import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAdsComponent } from './my-ads.component';

const routes: Routes = [
  { path: '', component: MyAdsComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MyAdsRoutingModule { }
