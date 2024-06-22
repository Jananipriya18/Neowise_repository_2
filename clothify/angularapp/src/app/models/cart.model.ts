import { Customer } from "./customer.model";
import { Product } from "./gift.model";

export class Cart{
    cartId?: number;
    totalAmount?: number;
    gift?: Product[];
    customer?: Customer;
}