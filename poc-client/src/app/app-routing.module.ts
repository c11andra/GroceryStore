import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageItemsComponent } from './manage-items/manage-items.component';

const routes: Routes = [
    { path: 'manage-items', component: ManageItemsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
