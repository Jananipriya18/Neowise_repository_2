import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { ProductService } from 'src/app/services/product.service';
import { User } from 'src/app/models/user.model';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { Customer } from 'src/app/models/customer.model';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-customer-view-products',
  templateUrl: './customer-view-products.component.html',
  styleUrls: ['./customer-view-products.component.css']
})
export class CustomerViewProductsComponent implements OnInit {
  name: string;
  address: string;
  products: any[] = [];
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

  constructor(private customerService: CustomerService, private productService: ProductService, private router: Router, private cartService: CartService) { }
  product: Product[]
  ngOnInit(): void {
    this.viewAllProducts();
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
        // this.viewAllProducts();
        this.formSubmitted = true;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  viewAllProducts(): void {
    this.productService.viewAllProducts().subscribe(
      (response: any) => { // Assuming the response is of type any
        console.log(response);
        console.log("response--------------------",response);

        // Assuming the response contains $id and $values properties
        const id = response.$id;
        const values = response.$values;

        // Assuming this.products is an array where you want to store the products
        this.products = values


        console.log(this.products, "this.products");
      },
      (error) => {
        console.error(error);
      }
    );
  }


  goToCart(product): void {
    let cartId = localStorage.getItem('cartId');
    if (cartId === null) {
      this.addCart(product.productId, product);
    } else {
      this.updateCart(product, cartId);
    }
    product.addedToCart = true;
  }

  customers: Customer = {
    customerId: Number(localStorage.getItem('customerId')),
    user: { id: Number(localStorage.getItem('userId')) } as User,
  };

  addCart(productId: any, updatedProduct: any): void {
    this.totalAmount = updatedProduct.productPrice;
    console.log(this.totalAmount);
    console.log(this.customer);
    this.product = updatedProduct;
    console.log(this.product);

    let cart = {
      customer: this.customers,
      products: [this.product],
      totalAmount: this.totalAmount,
    };

    console.log('check cart', cart);

    this.productService.updateProduct(productId, updatedProduct).subscribe(
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

  calculateTotalAmount(product) {
    if (product && product.productPrice && product.quantity) {
      return product.productPrice * product.quantity;
    }
    return 0;
  }

  updateCart(product, cartId) {
    // Implement this method to update the cart
    this.totalAmount = this.calculateTotalAmount(product);
    console.log(this.totalAmount);
    console.log(this.customerId);
    this.product = product;
    console.log(this.product);

    let cart = {
      cartId: cartId,
      customerId:localStorage.getItem('customerId'),
      // customer: this.customers,
      products: [this.product],
      totalAmount: this.totalAmount,
      updatedCart: true
    };

    console.log('check cart', cart);

    this.productService.updateProduct(product.productId, product).subscribe(
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