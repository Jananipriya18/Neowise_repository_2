import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthGuard } from 'src/app/guards/auth.guard'; // Import your AuthGuard

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
    private router: Router,
    private authGuard: AuthGuard  // Inject the AuthGuard
  ) {
    this.newCourseForm = this.formBuilder.group({
      courseName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      duration: ['', [Validators.required]],
      amount: [0, [Validators.required, Validators.min(0)]]
    });
  }

  addCourse(): void {
    if (this.newCourseForm.valid) {
      const newCourse: Course = this.newCourseForm.value as Course;

      // Get user role from AuthGuard
      const userRole = this.authGuard.getUserRole();

      // Include authentication headers
      const httpOptions = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authGuard.getAuthToken()  // Use the AuthGuard method to get the token
      };

      this.courseService.createCourse(newCourse, httpOptions).subscribe(
        (createdCourse: Course) => {
          console.log('Course created successfully:', createdCourse);
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('Error creating course:', error);
        }
      );
    } else {
      console.error('Form is not valid');
    }
  }
}
