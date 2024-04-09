import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GiftService } from 'src/app/services/gift.service';
import { Gift } from 'src/app/models/gift.model';

@Component({
  selector: 'app-edit-gift',
  templateUrl: './edit-gift.component.html',
  styleUrls: ['./edit-gift.component.css']
})
export class EditGiftComponent implements OnInit {
  editGiftForm: FormGroup;
  gift: Gift;
  errorMessage = '';

  editedGift: any;
  giftId: any;
  photoImage="";
  constructor(
    private fb: FormBuilder,
    private giftService: GiftService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.editGiftForm = this.fb.group({
      giftType: ['', Validators.required],
      giftImageUrl: ['', Validators.required],
      giftDetails: ['', Validators.required],
      giftPrice: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      quantity: ['', [Validators.required, Validators.min(1)]],
    });
  }
  ngOnInit(): void {
    // Retrieve the serialized gift object from the query parameters
    const gift = JSON.parse(this.route.snapshot.queryParamMap.get('gift'));
  
    if (gift) {
      // Populate the form with the existing gift details
      this.editGiftForm.patchValue({
        giftType: gift.giftType,
        giftImageUrl: this.photoImage,
        giftDetails: gift.giftDetails,
        quantity: gift.quantity, // Uncomment if 'quantity' is part of your gift object
        giftPrice: gift.giftPrice,
      });
      console.log(this.photoImage);
    } else {
      // Handle the case when the gift object is not available
    }
  }
  onSubmit(): void {
    if (this.editGiftForm.valid) {
      const updatedGift = this.editGiftForm.value;
   
      // Include the 'quantity' field in the request object
      const requestObj: Gift = {
        giftType: updatedGift.giftType,
        giftImageUrl: this.photoImage,
        giftDetails: updatedGift.giftDetails,
        giftPrice: updatedGift.giftPrice,
        quantity: updatedGift.quantity, // Add this line
      };

      // Fetch giftId from the route params
      const giftId = this.route.snapshot.paramMap.get('giftId');
      console.log(requestObj, "photo");
      this.giftService.updateGift(giftId, requestObj).subscribe(
        (response) => {
          console.log('Gift updated successfully', response);
          this.gift = response;

          this.router.navigate(['/admin/gifts/view']);
        },
        (error) => {
          console.error('Error updating gift', error);
        }
      );
    } else {
      // this.errorMessage = 'All fields are required';
    }
  }
  
  handleFileChange(event: any): void {
    const file = event.target.files[0];
 
    if (file) {
      this.convertFileToBase64(file).then(
        (base64String) => {
          this.photoImage=base64String;
          console.log(this.photoImage, "final");
          
        },
        
        (error) => {
          console.error('Error converting file to base64:', error);
          // Handle error appropriately
        }
      );
    }
  }
 
  convertFileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
 
      reader.onload = () => {
        resolve(reader.result as string);
      };
 
      reader.onerror = (error) => {
        reject(error);
      };
 
      reader.readAsDataURL(file);
    });
  }

  onCancel(){
    this.router.navigate(['/admin/gifts/view'])
  }

}
