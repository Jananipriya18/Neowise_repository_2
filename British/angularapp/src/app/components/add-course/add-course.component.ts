import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent {

  newCourseForm: FormGroup;
  isNewCourse: boolean = true;
  courses: Course[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private courseService: CourseService,
    private authService: AuthService,
    private router: Router
  ) {
    this.newCourseForm = this.formBuilder.group({
      courseName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      duration: ['', [Validators.required]],
      amount: [0, [Validators.required, Validators.min(0)]]
    });
  }

  addCourse(): void {
    if (this.authService.isAdmin() && this.newCourseForm.valid) {
      const newCourse: Course = this.newCourseForm.value as Course;

      this.courseService.createCourse(newCourse).subscribe(
        (createdCourse: Course) => {
          console.log('Course created successfully:', createdCourse);
          this.router.navigate(['/admincourselist']);
          // Fetch all courses after successful creation
          this.fetchAllCourses();
        },
        (error) => {
          console.error('Error creating course:', error);
        }
      );
    } else {
      console.error('Only admins can create courses or form is not valid');
    }
  }

  private fetchAllCourses(): void {
    this.courseService.getAllCourses().subscribe(
      (courses: Course[]) => {
        this.courses = courses; // Store the fetched courses
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }
  
  // Add this method to your component class
viewCourseDetails(courseId: number): void {
  // Implement logic to navigate or display details for the specific course
  console.log('View details for course with ID:', courseId);
}

  getCourseDetails(courseId: number): void {
    if (this.authService.isAdmin()) {
      // Implement logic to get course details using courseService.getCourseById()
      this.courseService.getCourseById(courseId).subscribe(
        (courseDetails: Course) => {
          console.log('Course details:', courseDetails);
        },
        (error) => {
          console.error('Error fetching course details:', error);
        }
      );
    } else {
      console.error('Only admins can view course details');
    }
  }

  deleteCourse(courseId: number): void {
    if (this.authService.isAdmin()) {
      // Implement delete logic here using courseService.deleteCourse()
      this.courseService.deleteCourse(courseId).subscribe(
        () => {
          console.log('Course deleted successfully');
          // Fetch all courses after successful deletion
          this.fetchAllCourses();
        },
        (error) => {
          console.error('Error deleting course:', error);
        }
      );
    } else {
      console.error('Only admins can delete courses');
    }
  }

  navigateToAdminCourseList(): void {
    this.router.navigate(['/admincourselist']);
  }
}
