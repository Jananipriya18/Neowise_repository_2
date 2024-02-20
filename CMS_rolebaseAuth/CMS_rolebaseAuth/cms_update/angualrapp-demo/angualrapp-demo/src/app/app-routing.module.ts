import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';
import { UsertaskComponent } from './usertask/usertask.component';
import { CreatetaskComponent } from './createtask/createtask.component';
import { DisplaytaskComponent } from './displaytask/displaytask.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { OperatorDashboardComponent } from './components/operator-dashboard/operator-dashboard.component';
import { AddcontainerComponent } from './components/addcontainer/addcontainer.component';
import { ViewcontainerComponent } from './components/viewcontainer/viewcontainer.component';
import { AddAssignmentComponent } from './components/add-assignment/add-assignment.component';
import { ViewAssignmentComponent } from './components/view-assignment/view-assignment.component';

const routes: Routes = [

  {path : '', redirectTo : '/login', pathMatch : 'full'},
  {path : 'register', component : RegisterComponent},
  {path : 'admin-dashboard', component : AdminDashboardComponent,
  children:[
    {path : 'add-container' , component : AddcontainerComponent},
    {path : 'view-containers' , component : ViewcontainerComponent},
    {path : 'add-assignment' , component : AddAssignmentComponent},
    {path : 'view-assignments' , component : ViewAssignmentComponent},
  ]
},
  {path : 'opertor-dashbaord', component : OperatorDashboardComponent},
  {path : 'login', component : LoginComponent},
  {path : 'error', component : ErrorComponent},
  {path : 'usertask',component :  UsertaskComponent},
  {path : 'createtask', component: CreatetaskComponent},
  {path : 'displaytask', component: DisplaytaskComponent},
  {path : '**', redirectTo : '/login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
