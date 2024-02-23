// Import necessary modules
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
  payments: Payment[] = [];
  newPayment: Payment = {
    paymentID: 0,
    userId: 0,
    courseID: 0,
    amountPaid: 0,
    paymentDate: new Date(),
    paymentMethod: '',
    transactionID: '',
  };

  courses: Course[] = [];

  constructor(
    private paymentService: PaymentService,
    private courseService: CourseService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // this.fetchAllPayments();
    this.fetchAllCourses();
    this.newPayment.userId = Number(this.authService.getCurrentUserId());
  }

  // fetchAllPayments(): void {
  //   this.paymentService.getAllPayments().subscribe(
  //     (payments: Payment[]) => {
  //       this.payments = payments;
  //     },
  //     (error) => {
  //       console.error('Error fetching payments:', error);
  //     }
  //   );
  // }

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
    this.paymentService.createPayment(this.newPayment).subscribe(
      (createdPayment: Payment) => {
        console.log('Payment successful:', createdPayment);
        window.alert('Payment successful!');
        this.router.navigate(['/']);
        // this.fetchAllPayments();
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
      
      console.log('Selected Course Information:', {
        courseId: course.courseID,
        courseName: course.courseName,
      });
  
      console.log('User Information:', {
        userId: userId,
      });
  
      this.router.navigate(['/payment', course.courseID], {
        queryParams: {
          courseName: course.courseName,
          userId: userId,
        },
      });
    }
  }
  
}
