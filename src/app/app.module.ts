import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule}from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './core/navbar/navbar.component';
import { HomeComponent } from './features/home/home/home.component';
import { CustomersComponent } from './features/customer/customers/customers.component';
import {MatPaginatorModule} from '@angular/material/paginator'; 

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    CustomersComponent
  ],
  imports: [
    BrowserModule,MatPaginatorModule,
    BrowserAnimationsModule,HttpClientModule, RouterModule.forRoot([

      {path:'customers',component:CustomersComponent},
      {path:'**',redirectTo:"home"},
      {path:'home',component:HomeComponent}
     
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
}
