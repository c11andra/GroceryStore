import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageItemsComponent } from './manage-items/manage-items.component';
import { SellItemsComponent } from './sell-items/sell-items.component';
import { ReportComponent } from './report/report.component';

const routes: Routes = [
    { path: 'manage-items', component: ManageItemsComponent },
    { path: 'sell-items', component: SellItemsComponent },
    { path: 'report', component: ReportComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
