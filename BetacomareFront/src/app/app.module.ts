import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

/* Our Components */
import { NavbarComponent } from './core/navbar/navbar.component';
import { HomeComponent } from './features/home/home/home.component';
import { LoginComponent } from './login/login.component';
import { CustomersComponent } from './features/customer/customers/customers.component';
import { AddressComponent } from './features/address/address.component';
import { UserComponent } from './features/user/user.component';
import { ProductComponent } from './features/product/product.component';

/* Our directives */
import { PswValidatorDirective } from './validator/psw-validator.directive';
import { PhoneValidatorDirective } from './validator/phone-validator.directive';

/* Material Modules */
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RegistrazioneComponent } from './features/registrazione/registrazione.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { PswMatchDirective } from './validator/psw-match.directive';
import { MatMenuModule } from '@angular/material/menu';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PswUpLetterDirective } from './validator/psw-up-letter.directive';
import { PswLowLetterDirective } from './validator/psw-low-letter.directive';
import { PswIsDigitDirective } from './validator/psw-is-digit.directive';
import { ContactsComponent } from './features/contacts/contacts.component';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from './features/dialog/dialog.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { PurchaseComponent } from './features/purchase/purchase.component';


@NgModule({
  // our components and directives
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    CustomersComponent,
    AddressComponent,
    ProductComponent,
    UserComponent,
    LoginComponent,
    RegistrazioneComponent,
    PswValidatorDirective,
    PhoneValidatorDirective,
    PswMatchDirective,
    PswUpLetterDirective,
    PswLowLetterDirective,
    PswIsDigitDirective,
    ContactsComponent,
    DialogComponent,
    PurchaseComponent,
  ],
  // external import
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatExpansionModule,
    MatSidenavModule,
    MatProgressBarModule,
    MatSliderModule,
    MatCardModule,
    MatCheckboxModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatMenuModule,
    MatSnackBarModule,
    MatBadgeModule,
    MatTableModule,
    MatTabsModule,
    MatDialogModule,
    MatGridListModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegistrazioneComponent },
      { path: 'login', component: LoginComponent },
      { path: 'customer', component: CustomersComponent, },
      { path: 'addresses', component: AddressComponent },
      { path: 'products', component: ProductComponent },
      { path: 'contacts', component: ContactsComponent },
      { path: 'purchase', component: PurchaseComponent },
      { path: '**', redirectTo: 'home' },
    ]),
  ],
  // gestore errori
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
