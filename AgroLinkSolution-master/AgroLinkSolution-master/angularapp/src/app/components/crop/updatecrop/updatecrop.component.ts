import { Component, OnInit } from '@angular/core';
import { Crop } from 'src/app/models/crop.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CropService } from 'src/app/services/crop.service';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-updatecrop',
  templateUrl: './updatecrop.component.html',
  styleUrls: ['./updatecrop.component.css']
})
export class UpdatecropComponent implements OnInit {

  id: number;
  errorMessage: string = '';
  formData: Crop = { // Use the Crop interface to type the formData object
    CropName: '',
    CropType: '',
    Description: null,
    PlantingDate: null
    
  };
  errors: any = {};
  successPopup: boolean; // Add this line to declare the successPopup property

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cropService: CropService
  ) { }

  ngOnInit(): void {
    // this.id = this.route.snapshot.paramMap.get('id');
    this.id = 1
    this.getCropById();
  }

  getCropById() {
    this.cropService.getCropById(this.id).subscribe(
      (response) => {
        console.log('Crop details:', response);
        this.formData = {
          CropName: response.CropName,
          CropType: response.CropType,
          Description: response.Description,
          PlantingDate: response.PlantingDate
        };
      },
      (error) => {
        console.error('Error fetching Crop details:', error);
        this.router.navigate(['/error']);
      }
    );
  }

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    this.errors[field] = ''; // Clear error when the user makes a change
  }

  handleUpdateCrop(cropForm: NgForm) {
    if (cropForm.valid) {
      this.cropService.updateCrop(1, this.formData).subscribe(
        (response) => {
          console.log('Crop updated successfully', response);
          this.successPopup = true;
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error updating Crop:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }

  handleOkClick() {
    // Close the success popup
    this.successPopup = false;
    // this.router.navigate(['/admin/view/viewloan']);
  }

  navigateToDashboard() {
    // this.router.navigate(['/admin/view/viewloan']);
  }

}
