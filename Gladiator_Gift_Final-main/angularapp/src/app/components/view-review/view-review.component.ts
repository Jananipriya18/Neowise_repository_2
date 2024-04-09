import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/review.model';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-view-review',
  templateUrl: './view-review.component.html',
  styleUrls: ['./view-review.component.css']
})
export class ViewReviewComponent implements OnInit {
  reviews: Review[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.getAllReviews();
  }

  // getAllReviews() {
  //   this.cartService.getAllReviews().subscribe(
  //     (response: Review[]) => { // Specify the type of response as Review[]
  //       this.reviews = response;
  //     },
  //     (error) => {
  //       console.error('Error fetching reviews', error);
  //       // Handle error, display an error message, etc.
  //     }
  //   );
  // }
  getAllReviews() {
    this.cartService.getAllReviews().subscribe(
      (response: any) => {
        // Check if response has $values property and it's an array
        if (response && response.$values && Array.isArray(response.$values)) {
          // Assuming $values contains the array of Review objects
          this.reviews = response.$values;
          console.log(this.reviews);
        } else {
          console.error('Invalid response format:', response);
        }
      },
      (error) => {
        console.error('Error fetching reviews', error);
        // Handle error, display an error message, etc.
      }
    );
  }
  

}
