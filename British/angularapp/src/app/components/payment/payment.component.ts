// // Import necessary modules
// import { Component, OnInit } from '@angular/core';
// import { PaymentService } from '../../services/payment.service';
// import { Payment } from '../../models/payment.model';
// import { ActivatedRoute, Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';
// import { CourseService } from '../../services/course.service';
// import { Course } from '../../models/course.model';

// @Component({
//   selector: 'app-payment',
//   templateUrl: './payment.component.html',
//   styleUrls: ['./payment.component.css']
// })
// export class PaymentComponent implements OnInit {
//   courseName: string=''
//   courseID: string=''
//   amount: number = 0
//   payments: Payment[] = [];
//   newPayment: Payment = {
//     paymentID: 0,
//     userId: 0,
//     courseID: 0,
//     amountPaid: 0,
//     paymentDate: new Date(),
//     modeOfPayment: '',
//   };

//   courses: Course[] = [];
//   createdPayment: Payment | null = null;

//   constructor(
//     private paymentService: PaymentService,
//     private courseService: CourseService,
//     private route: ActivatedRoute,
//     private router: Router,
//     private authService: AuthService
//   ) {}

//   ngOnInit(): void {
//     this.route.queryParams.subscribe(params => {
//       this.courseID = params['courseId'];
//       this.courseName = params['courseName'];
//       this.amount = params['amount'];
//       // console.log(courseId);
//       // console.log(courseName);
      
//     })
//     this.newPayment.userId = Number(this.authService.getCurrentUserId());
//     this.fetchAllCourses();
//     // this.makePayment();
//     console.log("New payment", this.newPayment);
//   }

//   // fetchAllPayments(): void {
//   //   this.paymentService.getAllPayments().subscribe(
//   //     (payments: Payment[]) => {
//   //       this.payments = payments;
//   //     },
//   //     (error) => {
//   //       console.error('Error fetching payments:', error);
//   //     }
//   //   );
//   // }

//   fetchAllCourses(): void {
//     this.courseService.getAllCourses().subscribe(
//       (courses: Course[]) => {
//         console.log("Courses", courses, JSON.stringify(courses));
//         this.courses = courses;
//       },
//       (error) => {
//         console.error('Error fetching courses:', error);
//       }
//     );
//   }

//   makePayment(): void {
//     console.log(this.newPayment);
//     this.newPayment.courseID = parseInt(this.courseID);
//     this.newPayment.amountPaid = this.amount;
//     this.paymentService.createPayment(this.newPayment).subscribe(
//       (createdPayment: Payment) => {
//         console.log('Payment successful:', createdPayment);
//         window.alert('Payment successful!');
//         this.router.navigate(['/']);
//         // this.fetchAllPayments();
//       },
//       (error) => {
//         console.error('Error making payment:', error);
//         window.alert('Error making Payment');
//       }
//     );
//   }

//   redirectToPayment(course: Course): void {
//     if (this.authService.isStudent()) {
//       const userId = this.authService.getCurrentUserId();
      
//       // console.log('Selected Course Information:', {
//       //   courseId: course.courseID,
//       //   courseName: course.courseName,
//       // });
//       console.log("Data check", JSON.stringify(course));
  
//       console.log('User Information:', {
//         userId: userId,
//       });
  
//       this.router.navigate(['/payment', course.courseID], {
//         queryParams: {
//           courseName: course.courseName,
//           userId: userId,
//         },
//       });
//     }
//   }
  
// }
import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/payment.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CourseService } from '../../services/course.service';
import { Course } from '../../models/course.model';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  courseName: string = '';
  courseID: string = '';
  amount: number = 0;
  payments: any[] = [];
  newPayment: Payment = {
    paymentID: 0,
    userId: 0,
    courseID: 0,
    amountPaid: 0,
    paymentDate: new Date(),
    modeOfPayment: '',
  };

  courses: Course[] = [];
  createdPayment: Payment | null = null;

  constructor(
    private paymentService: PaymentService,
    private courseService: CourseService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.courseID = params['courseId'];
      this.courseName = params['courseName'];
      this.amount = params['amount'];
    })
    this.newPayment.userId = Number(this.authService.getCurrentUserId());
    this.fetchAllCourses();
  }

  fetchAllCourses(): void {
    this.courseService.getAllCourses().subscribe(
      (courses: Course[]) => {
        this.courses = courses;
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }

  makePayment(): void {
    this.newPayment.courseID = parseInt(this.courseID);
    this.newPayment.amountPaid = this.amount;
    this.paymentService.createPayment(this.newPayment).subscribe(
      (createdPayment: Payment) => {
        console.log('Payment successful:', createdPayment);
        window.alert('Payment successful!');
        this.router.navigate(['/']);
      },
      (error) => {
        console.error('Error making payment:', error);
        window.alert('Error making Payment');
      }
    );
  }

  redirectToPayment(course: Course): void {
    if (this.authService.isStudent()) {
      const userId = this.authService.getCurrentUserId();
      this.router.navigate(['/payment', course.courseID], {
        queryParams: {
          courseName: course.courseName,
          userId: userId,
        },
      });
    }
  }
}
