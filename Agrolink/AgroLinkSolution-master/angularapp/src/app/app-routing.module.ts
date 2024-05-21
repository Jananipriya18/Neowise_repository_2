import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './components/error/error.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './components/authguard/auth.guard';
import { NewcropComponent } from './components/crop/newcrop/newcrop.component';
import { ViewcropComponent } from './components/crop/viewcrop/viewcrop.component';
import { UpdatecropComponent } from './components/crop/updatecrop/updatecrop.component';
import { DeletecropComponent } from './components/crop/deletecrop/deletecrop.component';
import { ViewagroComponent } from './components/agrochemicals/viewagro/viewagro.component';
import { CreateagroComponent } from './components/agrochemicals/createagro/createagro.component';
import { UpdateagroComponent } from './components/agrochemicals/updateagro/updateagro.component';
import { DeleteagroComponent } from './components/agrochemicals/deleteagro/deleteagro.component';
import { CreaterequestComponent } from './components/request/createrequest/createrequest.component';
import { ViewrequestComponent } from './components/request/viewrequest/viewrequest.component';
import { UpdaterequestComponent } from './components/request/updaterequest/updaterequest.component';
import { DeleterequestComponent } from './components/request/deleterequest/deleterequest.component';
import { CreatefeedbackComponent } from './components/feedback/createfeedback/createfeedback.component';
import { ViewfeedbackComponent } from './components/feedback/viewfeedback/viewfeedback.component';
import { FormerviewfeedbackComponent } from './components/feedback/formerviewfeedback/formerviewfeedback.component';


const routes: Routes = [

  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegistrationComponent },
  { path: 'error', component: ErrorComponent },
  { path: 'farmer/crop/add', component: NewcropComponent },
  { path: 'farmer/crop/view', component: ViewcropComponent },
  { path: 'farmer/crop/update', component: UpdatecropComponent },
  { path: 'farmer/crop/delete', component: DeletecropComponent },

  { path: 'farmer/agro/view', component: ViewagroComponent },
  { path: 'farmer/request/create', component: CreaterequestComponent },

  { path: 'seller/agro/view', component: ViewagroComponent },

  { path: 'seller/agro/create', component: CreateagroComponent },
  { path: 'seller/agro/update', component: UpdateagroComponent },
  { path: 'seller/agro/delete', component: DeleteagroComponent },

  { path: 'farmer/request/create', component: CreaterequestComponent },
  { path: 'farmer/request/view', component: ViewrequestComponent },
  { path: 'farmer/request/update', component: UpdaterequestComponent },
  { path: 'farmer/request/delete', component: DeleterequestComponent },

  { path: 'farmer/feedback/create', component: CreatefeedbackComponent },
  { path: 'farmer/feedback/myfeedback', component: FormerviewfeedbackComponent },

  { path: 'seller/feedback/myfeedback', component: ViewfeedbackComponent },
{path:'seller/formerrequest/viewrequest', component:ViewrequestComponent},
{path:'seller/formerrequest/changeStatus', component:UpdaterequestComponent},

  { path: '**', redirectTo: '/error' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
