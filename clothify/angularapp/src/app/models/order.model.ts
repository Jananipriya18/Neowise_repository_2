import { Customer } from './customer.model';
import { Product } from './product.model';

export class Order{
    orderId?: number;
    orderPrice?: number;
    quantity?: number;
    customer?: Customer;
    product?: Product[];
}