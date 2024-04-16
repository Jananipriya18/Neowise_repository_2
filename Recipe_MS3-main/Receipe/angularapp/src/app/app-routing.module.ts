import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PropertyFormComponent } from './property-form/property-form.component';
// import { PropertyListComponent } from './property-list/property-list.component';


const routes: Routes = [
  { path: 'addNewProperty', component: PropertyFormComponent }
  // { path: 'viewProperties', component: PropertyListComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
