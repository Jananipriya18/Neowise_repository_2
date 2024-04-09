import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { MyCartComponent } from './components/my-cart/my-cart.component';
import { ViewGiftsComponent } from './components/view-gifts/view-gifts.component';
import { EditGiftComponent } from './components/edit-gift/edit-gift.component';
import { AddGiftComponent } from './components/add-gift/add-gift.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CustomerdashboardComponent } from './components/customerdashboard/customerdashboard.component';
import { AuthGuard } from './components/authguard/auth.guard';
import { CustomerViewGiftsComponent } from './components/customer-view-gifts/customer-view-gifts.component';
import { CommonModule } from '@angular/common';
import { AddReviewComponent } from './components/add-review/add-review.component';
import { ViewReviewComponent } from './components/view-review/view-review.component';
import { PlaceOrderComponent } from './components/place-order/place-order.component';
import { MyOrdersComponent } from './components/my-orders/my-orders.component';
import { ViewOrdersComponent } from './components/view-orders/view-orders.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    NavbarComponent,
    HomeComponent,
    ErrorComponent,
    MyCartComponent,
    ViewGiftsComponent,
    EditGiftComponent,
    AddGiftComponent,
    DashboardComponent,
    CustomerdashboardComponent,
    CustomerViewGiftsComponent,
    AddReviewComponent,
    ViewReviewComponent,
    PlaceOrderComponent,
    MyOrdersComponent,
    ViewOrdersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
