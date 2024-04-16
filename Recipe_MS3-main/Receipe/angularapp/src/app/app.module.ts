import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// import { RecipeFormComponent } from './property-form/property-form.component';
import { HeaderComponent } from './header/header.component';
import { PropertyFormComponent } from './property-form/property-form.component';




@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    PropertyFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
