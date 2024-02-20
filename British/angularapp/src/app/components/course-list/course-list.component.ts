// course-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css'],
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.getAllCourses();
  }

  getAllCourses(): void {
    this.courseService.getAllCourses().subscribe((data) => {
      this.courses = data;
    });
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
