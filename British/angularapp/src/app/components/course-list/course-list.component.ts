// course-list.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';  

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css'],
})
export class CourseListComponent implements OnInit {
  editCourseModalVisible = false;
  editCourseForm: FormGroup;
  courses: Course[] = [];
  selectedCourse: Course;
  deleteConfirmationState: { [key: number]: boolean } = {};
  isAdmin: boolean = false;
  

  constructor(
    private courseService: CourseService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllCourses();

    this.authService.userRole$.subscribe(role => {
      this.isCustomer = role === 'Customer';

    });
    // Initialize the form controls
    this.editCourseForm = this.formBuilder.group({
      courseName: ['', Validators.required],
      description: ['', Validators.required],
      duration: ['', Validators.required],
      amount: ['', Validators.required],
    });
  }

  getAllCourses(): void {
    this.courseService.getAllCourses().subscribe((data) => {
      this.courses = data;
    });
  }

  private fetchAllCourses(): void {
    this.courseService.getAllCourses().subscribe(
      (courses: Course[]) => {
        this.courses = courses;
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }

     
  redirectToPayment(course: Course): void {
    // Navigate to the payment page using Router
    this.router.navigate(['/payment'], {
      queryParams: {
        courseId: course.courseID,
        courseName: course.courseName,
      },
    });
  }
  
}