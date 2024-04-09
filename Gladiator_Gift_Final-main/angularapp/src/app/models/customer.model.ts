import { User } from "./user.model";
import { Order } from "./order.model";

export class Customer{
    customerId?: number;
    customerName?: string;
    address?: string;
    // userId?:number;
    user?: User;
    order?: Order;
} 