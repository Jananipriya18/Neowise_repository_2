import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthService } from 'src/app/services/auth.service'; // Import your AuthService


@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent {

  newCourseForm: FormGroup; 

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
          this.router.navigate(['/']);
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

  // Fetch all courses and log them
  private fetchAllCourses(): void {
    this.courseService.getAllCourses().subscribe(
      (courses: Course[]) => {
        console.log('Courses:', courses);
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }

  updateCourse(courseId: number): void {
    if (this.authService.isAdmin()) {
      // Implement update logic here using courseService.updateCourse()
      // For example, assuming there is a method updateCourse in CourseService:
      const updatedCourse: Course = this.newCourseForm.value as Course;

      this.courseService.updateCourse(courseId, updatedCourse).subscribe(
        (updatedCourse: Course) => {
          console.log('Course updated successfully:', updatedCourse);
          // Fetch all courses after successful update
          this.fetchAllCourses();
        },
        (error) => {
          console.error('Error updating course:', error);
        }
      );
    } else {
      console.error('Only admins can update courses');
    }
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
}
