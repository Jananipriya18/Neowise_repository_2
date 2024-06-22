import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GiftService } from 'src/app/services/gift.service';
 
interface Gift {
  giftType: string;
  giftImageUrl: string;
  giftDetails: string;
  giftPrice: number;
  quantity: number;
}
 
@Component({
  selector: 'app-add-gift',
  templateUrl: './add-gift.component.html',
  styleUrls: ['./add-gift.component.css']
})
export class AddGiftComponent implements OnInit {
  addGiftForm: FormGroup;
  errorMessage = '';
  selectedFile: File | null = null;
  photoImage="";
 
  constructor(private fb: FormBuilder, private giftService: GiftService, private route: Router) {
    this.addGiftForm = this.fb.group({
      giftType: ['', Validators.required],
      giftImageUrl: [null, Validators.required],
      giftDetails: ['', Validators.required],
      giftPrice: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      quantity: ['', [Validators.required, Validators.min(1)]],
    });
  }
 
  ngOnInit(): void {
  }
 
  onSubmit(): void {
    if (this.addGiftForm.valid) {
      const newGift = this.addGiftForm.value;
      const requestObj: Gift = {
        giftType: newGift.giftType,
        giftImageUrl: this.photoImage,
        giftDetails: newGift.giftDetails,
        giftPrice: newGift.giftPrice,
        quantity: newGift.quantity,
      };

      this.giftService.addGift(requestObj).subscribe(
        (response) => {
          console.log('Gift added successfully', response);
          this.route.navigate(['/admin/gifts/view']);
          this.addGiftForm.reset(); // Reset the form
        },
        (error) => {
          console.error('ErrorAddingGift', error);
        }
      );
    } else {
      this.errorMessage = "All fields are required";
    }
  }
  onFileSelected(event: any) {
    const files: FileList = event.target.files;
    if (files.length > 0) {
      this.selectedFile = files[0];
    }
  }

  handleFileChange(event: any): void {
    const file = event.target.files[0];
 
    if (file) {
      this.convertFileToBase64(file).then(
        (base64String) => {
          this.photoImage=base64String
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
}