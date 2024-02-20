import { Course } from "./course.model";

export interface Enquiry {
    enquiryID: number;
    enquiryDate: Date;
    userId: string;
    title: string;
    description: string;
    emailID: string;
    enquiryType: string;
    course: Course;
  }
  