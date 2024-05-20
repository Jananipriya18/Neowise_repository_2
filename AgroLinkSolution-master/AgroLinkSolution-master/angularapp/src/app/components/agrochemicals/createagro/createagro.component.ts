import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AgroChemicals } from 'src/app/models/agrochemicals';
import { AgrochemicalService } from 'src/app/services/agrochemical.service';

@Component({
  selector: 'app-createagro',
  templateUrl: './createagro.component.html',
  styleUrls: ['./createagro.component.css']
})
export class CreateagroComponent implements OnInit {

  

  id: string;
  formData: AgroChemicals = { // Use the AgroChenical interface to type the formData object
    
   
    Name:"",
    Brand:"string",
    Category:"",
    SuitableCrop:null,
    Description:"",
    Quantity:null,
    PricePerUnit:null,
    Image:""

  };
  errors: any = {};
  errorMessage: string;
  successPopup: boolean = false;

  constructor(private agroService: AgrochemicalService, private router: Router) {}

  ngOnInit(): void {
    // Initialize your component here
  }

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    // Validate your form here and set errors if any
  }

  onSubmit(agroForm: NgForm) {
    console.log('Form Validity:', agroForm.valid);
    if (agroForm.valid) {
      this.agroService.addAgrochemical(this.formData).subscribe(
        (res) => {
          this.successPopup = true;
          console.log('Loan added successfully', res);
          agroForm.resetForm();
        },
        (err) => {
          if (err.status === 500 && err.error.message === 'AgroChemaical with the same type already exists') {
            this.errorMessage = 'AgroChemaical with the same type already exists';
          } else {
            this.errors = err.error;
          }
          console.error('Error adding AgroChemaical:', err);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  handleSuccessMessage() {
    this.successPopup = false;
    this.errorMessage = '';
    this.formData = {
      Name:"",
    Brand:"string",
    Category:"",
    SuitableCrop:null,
    Description:"",
    Quantity:null,
    PricePerUnit:null,
    Image:""
    };
  }

}
