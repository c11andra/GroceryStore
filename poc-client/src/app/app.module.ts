import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SideNavComponent } from './side-nav/side-nav.component';
import { MatSidenavModule, MatIconModule, MatListModule, MatSelectModule, MatCardModule, MatDividerModule, MatButtonModule } from '@angular/material';
import { ManageItemsComponent } from './manage-items/manage-items.component';
import { SellItemsComponent } from './sell-items/sell-items.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReportComponent } from './report/report.component';
@NgModule({
  declarations: [
    AppComponent,
    SideNavComponent,
    ManageItemsComponent,
    SellItemsComponent,
    ReportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    ReactiveFormsModule,
    MatSelectModule,
    HttpClientModule,
    MatCardModule,
    MatDividerModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
