import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Review } from 'src/app/models/review.model';
import { CartService } from 'src/app/services/cart.service';
import { Router } from '@angular/router';
import { GiftService } from 'src/app/services/gift.service';

@Component({
  selector: 'app-add-review',
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css'],
})
export class AddReviewComponent implements OnInit {
  addReviewForm: FormGroup;
  errorMessage = '';
  gifts: any =[];
  giftTypes: string[] = [];

  // userName: string | null = localStorage.getItem('userName');
  // userName: string | null;
  constructor(private fb: FormBuilder, private cartService: CartService, private router: Router, private giftService: GiftService) {
    this.addReviewForm = this.fb.group({
      userName: [localStorage.getItem('currentUser'), Validators.required],
      subject: ['', Validators.required],
      body: ['', Validators.required],
      rating: ['', Validators.required],
      dateCreated: [this.getCurrentDate(), Validators.required],
    });
  }

  ngOnInit() {
    console.log(localStorage.getItem('currentUser'))
    this.viewAllGifts();
    // this.addReviewForm.patchValue({
    //   userName: this.emailaddress;
    // });
    
  }
  getCurrentDate(): string {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    const day = ('0' + currentDate.getDate()).slice(-2);
    const hours = ('0' + currentDate.getHours()).slice(-2);
    const minutes = ('0' + currentDate.getMinutes()).slice(-2);
    const seconds = ('0' + currentDate.getSeconds()).slice(-2);
  
    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
  }

  viewAllGifts(){
    this.giftService.viewAllGifts().subscribe(

      (data: any) => {
        this.giftTypes = data.$values.map((gift:any) =>gift.giftType);
        console.log(this.giftTypes);
        this.gifts = data;
        console.log(this.gifts);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onSubmit(): void {
    if (this.addReviewForm.valid) {
      const newReview = this.addReviewForm.value;
      const requestObj: Review = {
        userId: Number(localStorage.getItem('userId')),
        subject: newReview.subject,
        body: newReview.body,
        rating: newReview.rating,
        dateCreated: newReview.dateCreated
      };
  
      this.cartService.addReview(requestObj).subscribe(
        (response) => {
          console.log('Review added successfully', response);
          this.addReviewForm.reset(); // Reset the form
          this.router.navigateByUrl('/');
        },
        (error) => {
          console.error('Error adding review', error);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }
}