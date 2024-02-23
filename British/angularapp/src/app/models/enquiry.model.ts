import { Course } from "./course.model";
import {User} from "./user.model";

export interface Enquiry {
    enquiryID: number;
    enquiryDate: Date;
    userId: number;
    title: string;
    description: string;
    emailID: string;
    enquiryType: string;
    courseID: number;
    course?: Course; 
}
