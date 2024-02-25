import { Course } from "./course.model";
import { Enquiry } from "./enquiry.model";
import { Student } from "./student.model";
import { User } from "./user.model";

export interface Payment {
  paymentID?: number;
  enquiryID?: number;
  amountPaid: number;
  paymentDate: Date;
  modeOfPayment: string;
  userId: number;
  courseID: number;
  studentId?: number;
  users?: User;
  courses?: Course;
  students?: Student;
  }