// // course-list.component.ts
// import { Component, OnInit } from '@angular/core';
// import { Course } from 'src/app/models/course.model';
// import { CourseService } from 'src/app/services/course.service';
// import { AuthService } from 'src/app/services/auth.service';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 

// @Component({
//   selector: 'app-course-list',
//   templateUrl: './course-list.component.html',
//   styleUrls: ['./course-list.component.css'],
// })
// export class CourseListComponent implements OnInit {
//   courses: Course[] = [];
//   newCourseForm: FormGroup; 

//   constructor(private courseService: CourseService,
//     private authService: AuthService,
//     private formBuilder: FormBuilder 
//     ) {}

//   ngOnInit(): void {
//     this.getAllCourses();
//   }

//   getAllCourses(): void {
//     this.courseService.getAllCourses().subscribe((data) => {
//       this.courses = data;
//     });
//   }

//   deleteCourse(courseId: number): void {
//     if (this.authService.isAdmin()) {
//       // Implement delete logic here using courseService.deleteCourse()
//       this.courseService.deleteCourse(courseId).subscribe(
//         () => {
//           console.log('Course deleted successfully');
//           // Fetch all courses after successful deletion
//           this.fetchAllCourses();
//         },
//         (error) => {
//           console.error('Error deleting course:', error);
//         }
//       );
//     } else {
//       console.error('Only admins can delete courses');
//     }
//   }

//   private fetchAllCourses(): void {
//     this.courseService.getAllCourses().subscribe(
//       (courses: Course[]) => {
//         this.courses = courses; // Store the fetched courses
//       },
//       (error) => {
//         console.error('Error fetching courses:', error);
//       }
//     );
//   }

//   updateCourse(courseId: number): void {
//     if (this.authService.isAdmin()) {
//       // Implement update logic here using courseService.updateCourse()
//       // For example, assuming there is a method updateCourse in CourseService:
//       const updatedCourse: Course = this.newCourseForm.value as Course;

//       this.courseService.updateCourse(courseId, updatedCourse).subscribe(
//         (updatedCourse: Course) => {
//           console.log('Course updated successfully:', updatedCourse);
//           // Fetch all courses after successful update
//           this.fetchAllCourses();
//         },
//         (error) => {
//           console.error('Error updating course:', error);
//         }
//       );
//     } else {
//       console.error('Only admins can update courses');
//     }
//   }
// }

// course-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';  
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css'],
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];

  constructor(
    private courseService: CourseService,
    private authService: AuthService,
    private router: Router
  ) {}

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
      this.courseService.deleteCourse(courseId).subscribe(
        () => {
          console.log('Course deleted successfully');
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

  updateCourse(courseId: number): void {
    if (this.authService.isAdmin()) {
      // Assuming you have an EditCourseComponent for updating course details
      this.router.navigate(['/courselist/edit', courseId]);
    } else {
      console.error('Only admins can update courses');
    }
  }
}
