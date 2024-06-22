import { Cart } from './cart.model';
import { Order } from './order.model'; 
 
export interface Product {
    productId?: number;
    productType: string;
    productImageUrl: string;
    productDetails: string;
    productPrice: number;
    quantity:number;
    cart?: Cart; 
    order?: Order;
  }