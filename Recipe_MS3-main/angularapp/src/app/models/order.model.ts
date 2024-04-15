import { Customer } from './customer.model';
import { Gift } from './gift.model';

export class Order{
    orderId?: number;
    orderPrice?: number;
    quantity?: number;
    customer?: Customer;
    gift?: Gift[];
}