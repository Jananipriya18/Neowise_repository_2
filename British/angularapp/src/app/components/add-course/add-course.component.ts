import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent {

  newCourseForm: FormGroup; // Define a form group

  constructor(private formBuilder: FormBuilder, private courseService: CourseService, private router: Router) {
    this.newCourseForm = this.formBuilder.group({
      courseName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      duration: ['', [Validators.required]],
      amount: [0, [Validators.required, Validators.min(0)]]
    });
  }

  addCourse(): void {
    if (this.newCourseForm.valid) { // Check if the form is valid
      const newCourse: Course = this.newCourseForm.value as Course;

      this.courseService.createCourse(newCourse).subscribe(
        (createdCourse: Course) => {
          // Handle successful creation, e.g., show a success message or navigate to another page
          console.log('Course created successfully:', createdCourse);
          this.router.navigate(['/']); // Navigate to the courses page after successful creation
        },
        (error) => {
          // Handle error, e.g., display an error message
          console.error('Error creating course:', error);
        }
      );
    } else {
      // Form is not valid, display an error message or handle it as needed
      console.error('Form is not valid');
    }

    this.courseService.getAllCourses().subscribe(
  (courses: Course[]) => {
    // Handle the retrieved courses
    console.log('Courses:', courses);
  },
  (error) => {
    // Handle the error
    console.error('Error fetching courses:', error);
  }
);
  }
}
