import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './components/authguard/auth.guard';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CustomerdashboardComponent } from './components/customerdashboard/customerdashboard.component';
import { ViewProductsComponent } from './components/view-products/view-products.component';
import { CustomerViewProductsComponent } from './components/customer-view-products/customer-view-products.component';
import { MyCartComponent } from './components/my-cart/my-cart.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { AddReviewComponent } from './components/add-review/add-review.component';
import { ViewReviewComponent } from './components/view-review/view-review.component';
import { PlaceOrderComponent } from './components/place-order/place-order.component';
import { ViewOrdersComponent } from './components/view-orders/view-orders.component';
import { MyOrdersComponent } from './components/my-orders/my-orders.component';


const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'signup', component:RegistrationComponent},
  {path: 'login', component:LoginComponent},
  {path: 'admin/dashboard', component:DashboardComponent, canActivate: [AuthGuard]},
  {path: 'customer/dashboard', component:CustomerdashboardComponent, canActivate: [AuthGuard]},
  {path: 'admin/products/view', component:ViewProductsComponent, canActivate: [AuthGuard]},
  {path: 'customer/products/view', component:CustomerViewProductsComponent, canActivate: [AuthGuard]},
  {path: 'customer/mycart', component:MyCartComponent, canActivate: [AuthGuard]},
  {path: 'admin/editproduct/:productId', component:EditProductComponent, canActivate: [AuthGuard]},
  {path: 'admin/addproduct', component:AddProductComponent, canActivate: [AuthGuard]},
  {path:'customer/addreview', component:AddReviewComponent, canActivate: [AuthGuard]},
  {path: 'admin/viewreview', component:ViewReviewComponent, canActivate: [AuthGuard]},
  {path: 'customer/placeorder', component:PlaceOrderComponent, canActivate: [AuthGuard]},
  {path: 'customer/myorders', component:MyOrdersComponent, canActivate: [AuthGuard]},
  {path: 'admin/vieworders', component:ViewOrdersComponent, canActivate: [AuthGuard]},
  { path: 'error', component: ErrorComponent, data: { message: 'Oops! Something went wrong.' }},
  { path: '**', redirectTo: '/error', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
