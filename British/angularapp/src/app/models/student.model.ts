
export interface Student {
    studentId: number;
    studentName: string;
    studentMobileNumber: string;
    enquiries: Enquiry[];
    courses: Course[];
    payments: Payment[];
    userId: number;
    user?: User;
  }
  