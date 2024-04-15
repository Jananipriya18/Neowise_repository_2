import { Cart } from './cart.model';
import { Order } from './order.model'; 
 
export interface Gift {
    giftId?: number;
    giftType: string;
    giftImageUrl: string;
    giftDetails: string;
    giftPrice: number;
    quantity:number;
    cart?: Cart; 
    order?: Order;
  }