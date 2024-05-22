import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LaptopFormComponent} from './laptop-form/laptop-form.component';
import { LaptopListComponent } from './laptop-list/laptop-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';


const routes: Routes = [
  { path: 'addNewLaptop', component: LaptopFormComponent },
  { path: 'viewLaptops', component: LaptopListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
