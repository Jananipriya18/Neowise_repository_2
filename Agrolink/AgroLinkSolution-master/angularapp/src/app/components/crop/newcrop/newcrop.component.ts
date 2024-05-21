import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Crop } from 'src/app/models/crop.model';
import { CropService } from 'src/app/services/crop.service';

@Component({
  selector: 'app-newcrop',
  templateUrl: './newcrop.component.html',
  styleUrls: ['./newcrop.component.css']
})
export class NewcropComponent implements OnInit {
  id: string;
  formData: Crop = { // Use the Loan interface to type the formData object
   CropName:"",
   CropType:"",
   Description:"",
   PlantingDate:null

  };
  errors: any = {};
  errorMessage: string;
  successPopup: boolean = false;

  constructor(private cropService: CropService, private router: Router) {}

  ngOnInit(): void {
    // Initialize your component here
  }

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    // Validate your form here and set errors if any
  }

  onSubmit(cropForm: NgForm) {
    console.log('Form Validity:', cropForm.valid);
    if (cropForm.valid) {
      this.cropService.addCrop(this.formData).subscribe(
        (res) => {
          this.successPopup = true;
          console.log('Loan added successfully', res);
          cropForm.resetForm();
        },
        (err) => {
          if (err.status === 500 && err.error.message === 'Crop with the same type already exists') {
            this.errorMessage = 'Crop with the same type already exists';
          } else {
            this.errors = err.error;
          }
          console.error('Error adding Crop:', err);
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
      CropName:"",
      CropType:"",
      Description:"",
      PlantingDate:null
    };
  }
}
