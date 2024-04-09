import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.css'],
})
export class MyCartComponent implements OnInit {
  maxQuantity: number; // Define maxQuantity
  customerData = { totalAmount: 0 };
  gifts = [];
  totalAmount = 0;
  giftsCart: any;
  
  cartId = Number(localStorage.getItem('cartId'));
  userQuantity: number = 1;
  
  constructor(private cartService: CartService, private router: Router) {}
  
  // giftsCart = {};
  ngOnInit(): void {
    this.updateTotalAmount();
    this.getAllGiftsFromCart();
  }

  getAllGiftsFromCart() {
    this.cartService.getAllGiftsFromCart().subscribe(
      (response) => {
        console.log(response);
        if (response && response.gifts) {
          // Check if the gifts property is an object with $values array
          if (Array.isArray(response.gifts.$values)) {
            // Extract the array from the nested object
            this.gifts = response.gifts.$values;
          } else {
            // If $values array is not present, consider gifts itself as the array
            this.gifts = response.gifts;
          }
          this.totalAmount = response.totalAmount;
          this.gifts.forEach(gift => {
            gift.userQuantity = 1;
          });
        } else {
          console.error('Invalid response format:', response);
        }
      },
      (error) => {
        console.error(error);
      }
    );
    //new
  }
  
  calculateTotalAmount(): number {
    let totalAmount = 0;
    for (const gift of this.gifts) {
      totalAmount += gift.userQuantity * gift.giftPrice;
    }
    return totalAmount;
  }
  validateQuantity(giftData: any): void {
    // Your validation logic here, using giftData.userQuantity
  }
  
  // initializeQuantity() {
  //   this.gifts.forEach(giftData => {
  //     giftData.maxQuantity = 1;
  //   });
  // }


  placeOrder() {
    const totalAmount = this.calculateTotalAmount();
    this.router.navigate(['/customer/placeorder'], { queryParams: { totalAmount } });
  }


  updateQuantity(giftData: any): void {
    if (giftData.quantity > giftData.userQuantity) {
      giftData.quantity = giftData.userQuantity;
    }
    
    giftData.totalAmount = giftData.userQuantity * giftData.giftPrice; // Update the total amount for the specific gift
    
    const customerId = localStorage.getItem('customerId'); // Get the customerId from local storage
    
    // Call getTotalAmount method from CartService with customerId as argument
    this.cartService.getTotalAmount(customerId).subscribe(
      response => {
        console.log(response); // Handle the response as per your requirement
      },
      error => {
        console.error('Error:', error); 
      }
    );
    this.updateTotalAmount();
    console.log('Total amount updated:', this.totalAmount);
  }
  updateTotalAmount(): void {
    this.totalAmount = this.gifts.reduce((total, gift) => total + (this.userQuantity * gift.giftPrice), 0);
    console.log('Total amount:', this.totalAmount);
  }

  // removeFromCart(giftId: number) {
  //   this.gifts = this.gifts.filter(gift => gift.giftId !== giftId);
  //   this.giftsCart = {
  //     cartId: Number(localStorage.getItem('cartId')),
  //     customer: { customerId: Number(localStorage.getItem('customerId')) },
  //     gifts: [...this.gifts],
  //     totalAmount: this.totalAmount - this.gifts.filter(gift => gift.giftId === giftId)[0].giftPrice
  //   } 
  //   console.log(this.giftsCart);
  //   this.cartService.updateCart(this.giftsCart).subscribe(
  //     response => {
  //       console.log(response);
  //       console.log('Gift removed from cart successfully');
  //       this.getAllGiftsFromCart();
  //     },
  //     error => {
  //       console.error(error);
  //     }
  //   );
  // }
  removeFromCart(giftId: number) {
    const index = this.gifts.findIndex(gift => gift.giftId === giftId);
    if (index !== -1) {
      const removedGift = this.gifts.splice(index, 1)[0]; // Remove the gift from the array
      this.totalAmount -= removedGift.giftPrice; // Adjust the total amount
      const giftsCart = {
        cartId: this.cartId,
        // giftId:this.giftId,
        customer: { customerId: Number(localStorage.getItem('customerId')) },
        gifts: [...this.gifts],
        totalAmount: this.totalAmount,
        updatedCart: true
      }; 
      console.log(giftsCart);
      this.cartService.removeGiftsFromCart(giftsCart, giftId).subscribe(
        response => {
          console.log(response);
          console.log('Gift removed from cart successfully');
          this.getAllGiftsFromCart();
        },
        error => {
          console.error(error);
        }
      );
    } else {
      console.error('Gift not found in the cart');
    }
  }

}
