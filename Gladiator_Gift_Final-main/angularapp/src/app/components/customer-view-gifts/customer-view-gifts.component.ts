import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { GiftService } from 'src/app/services/gift.service';
import { User } from 'src/app/models/user.model';
import { Router } from '@angular/router';
import { Gift } from 'src/app/models/gift.model';
import { Customer } from 'src/app/models/customer.model';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-customer-view-gifts',
  templateUrl: './customer-view-gifts.component.html',
  styleUrls: ['./customer-view-gifts.component.css']
})
export class CustomerViewGiftsComponent implements OnInit {
  name: string;
  address: string;
  gifts: any[] = [];
  user: User;
  totalAmount: number;
  customer: Customer;
  formSubmitted: boolean = false;
  customerId = localStorage.getItem('customerId');
  customerIdRegistered: boolean = false;
  addedToCart = false;
  popupMessage: string = '';
  showPopup: boolean = false;
  message = "";

  constructor(private customerService: CustomerService, private giftService: GiftService, private router: Router, private cartService: CartService) { }
  gift: Gift[]
  ngOnInit(): void {
    this.viewAllGifts();
    this.customerId = localStorage.getItem('customerId');
    console.log(this.customerId);
  }

  hasCustomerId(): boolean {
    // if(localStorage.getItem('customerId') === null;
  // }

  // console.log("localStorage.getItem('customerId')",localStorage.getItem('customerId')=="null",localStorage.getItem('customerId')==null)
if(localStorage.getItem('customerId')=="0")
{
  
  return true
}
return false



  }



  onSubmit(): void {
    const newCustomer= {

      customerName: this.name,
      address: this.address,
      //             // userId: Number(localStorage.getItem('userId')),
      userId:  localStorage.getItem('userId'),
    };
    // const userId = Number(localStorage.getItem('userId'));

    this.customerService.registerCustomer(newCustomer).subscribe(
      (response) => {
        console.log(response, "customerId")
        localStorage.setItem('customerId', response.registeredCustomer.customerId);
        localStorage.setItem('cartId', response.cartId);
        const customerId = localStorage.getItem("customerId");
        console.log(customerId);
        const cardId = localStorage.getItem("cartId")
        console.log(cardId)
        // this.viewAllGifts();
        this.formSubmitted = true;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  viewAllGifts(): void {
    this.giftService.viewAllGifts().subscribe(
      (response: any) => { // Assuming the response is of type any
        console.log(response);
        console.log("response--------------------",response);

        // Assuming the response contains $id and $values properties
        const id = response.$id;
        const values = response.$values;

        // Assuming this.gifts is an array where you want to store the gifts
        this.gifts = values


        console.log(this.gifts, "this.gifts");
      },
      (error) => {
        console.error(error);
      }
    );
  }


  goToCart(gift): void {
    let cartId = localStorage.getItem('cartId');
    if (cartId === null) {
      this.addCart(gift.giftId, gift);
    } else {
      this.updateCart(gift, cartId);
    }
    gift.addedToCart = true;
  }

  customers: Customer = {
    customerId: Number(localStorage.getItem('customerId')),
    user: { id: Number(localStorage.getItem('userId')) } as User,
  };

  addCart(giftId: any, updatedGift: any): void {
    this.totalAmount = updatedGift.giftPrice;
    console.log(this.totalAmount);
    console.log(this.customer);
    this.gift = updatedGift;
    console.log(this.gift);

    let cart = {
      customer: this.customers,
      gifts: [this.gift],
      totalAmount: this.totalAmount,
    };

    console.log('check cart', cart);

    this.giftService.updateGift(giftId, updatedGift).subscribe(
      (response) => {
        console.log(response, "cart updated succesfully");
        this.addedToCart = true;
        localStorage.setItem('cartId', response.cartId);
        // this.router.navigate(['/customer/mycart']);

      },
      (error) => {
        console.error(error);
      }
    );
  }

  calculateTotalAmount(gift) {
    if (gift && gift.giftPrice && gift.quantity) {
      return gift.giftPrice * gift.quantity;
    }
    return 0;
  }

  updateCart(gift, cartId) {
    // Implement this method to update the cart
    this.totalAmount = this.calculateTotalAmount(gift);
    console.log(this.totalAmount);
    console.log(this.customerId);
    this.gift = gift;
    console.log(this.gift);

    let cart = {
      cartId: cartId,
      customerId:localStorage.getItem('customerId'),
      // customer: this.customers,
      gifts: [this.gift],
      totalAmount: this.totalAmount,
      updatedCart: true
    };

    console.log('check cart', cart);

    this.giftService.updateGift(gift.giftId, gift).subscribe(
      (response) => {
        console.log(response);
        this.addedToCart = true;
        // this.router.navigate(['/customer/mycart']);
      },
      (error) => {
        console.error(error);
      }
    );


    //   viewCustomerById(customerId: number): void {
    //     this.customerService.viewCustomerById(this.customerId).subscribe(
    //       (response) => {
    //         console.log(response);
    //         this.customers = response;
    //       },
    //       (error) => {
    //         console.error(error);
    //       }
    //     );
    //   }
  }
}