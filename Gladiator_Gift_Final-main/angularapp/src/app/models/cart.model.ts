import { Customer } from "./customer.model";
import { Gift } from "./gift.model";

export class Cart{
    cartId?: number;
    totalAmount?: number;
    gift?: Gift[];
    customer?: Customer;
}