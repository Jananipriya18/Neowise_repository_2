import { Course } from "./course.model";
import { User } from "./user.model";

export interface Payment {
    paymentID: number;
    userId: number;
    courseID: number;
    amountPaid: number;
    paymentDate: Date;
    modeOfPayment: string;
    // transactionID: string;
    user?: User;
    course?: Course;
  }