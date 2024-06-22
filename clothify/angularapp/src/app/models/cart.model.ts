import { Customer } from "./customer.model";
import { Product } from "./product.model";

export class Cart{
    cartId?: number;
    totalAmount?: number;
    product?: Product[];
    customer?: Customer;
}