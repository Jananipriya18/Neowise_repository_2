import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { CustomerDashboardComponent } from './components/customer-dashboard/customer-dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CourseListComponent } from './components/course-list/course-list.component';
import { AddCourseComponent } from './components/add-course/add-course.component';
import { EnquiryFormComponent } from './components/enquiry-form/enquiry-form.component';
import { EnquiryListComponent } from './components/enquiry-list/enquiry-list.component';
import { PaymentComponent } from './components/payment/payment.component';
import { PaymentListComponent } from './payment-list/payment-list.component';
import { CommonModule } from '@angular/common';
import { AdminCourseListComponent } from './components/admin-course-list/admin-course-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    ErrorComponent,
    HomeComponent,
    AdminDashboardComponent,
    CustomerDashboardComponent,
    NavbarComponent,
    CourseListComponent,
    AddCourseComponent,
    EnquiryFormComponent,
    EnquiryListComponent,
    PaymentComponent,
    PaymentListComponent,
    AdminCourseListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
