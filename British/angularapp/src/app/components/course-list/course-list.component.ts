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

  constructor(
    private courseService: CourseService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllCourses();
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

  updateCourse(courseID: number): void {
    if (this.authService.isAdmin()) {
      this.selectedCourse = this.courses.find(course => course.courseID === courseID);
  
      if (this.selectedCourse) {
        console.log('Selected Course:', this.selectedCourse);
        // Reset the form to clear any previous values
        this.editCourseForm.reset();
        // Patch the form values with the selected course
        this.editCourseForm.patchValue({
          courseName: this.selectedCourse.courseName,
          description: this.selectedCourse.description,
          duration: this.selectedCourse.duration,
          amount: this.selectedCourse.amount,
        });
        this.editCourseModalVisible = true;
      } else {
        console.error('Course not found');
      }
    } else {
      console.error('Only admins can update courses');
    }
  }
  
  

  saveChanges(): void {
    if (this.authService.isAdmin()) {
      // Update the selected course with form values
      const updatedCourse: Course = {
        ...this.selectedCourse,
        ...this.editCourseForm.value,
      };

      // Update the course in the database
      this.courseService.updateCourse(this.selectedCourse.courseID, updatedCourse).subscribe(
        (updatedCourse: Course) => {
          console.log('Course updated successfully:', updatedCourse);
          // Fetch all courses after successful update
          this.fetchAllCourses();
          // Close the edit modal
          this.closeEditModal();
        },
        (error) => {
          console.error('Error updating course:', error);
        }
      );
    } else {
      console.error('Only admins can update courses');
    }
  }

  closeEditModal(): void {
    // Reset form and hide the edit modal
    this.editCourseForm.reset();
    this.editCourseModalVisible = false;
  }
}
