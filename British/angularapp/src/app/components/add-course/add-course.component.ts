// // add-course.component.ts
// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { Course } from 'src/app/models/course.model';
// import { CourseService } from 'src/app/services/course.service';

// @Component({
//   selector: 'app-add-course',
//   templateUrl: './add-course.component.html',
//   styleUrls: ['./add-course.component.css']
// })
// export class AddCourseComponent {

//   newCourse: Course = {
//     courseID: 0,
//     courseName: '',
//     description: '',
//     duration: '',
//     amount: 0
//   };

//   constructor(private courseService: CourseService, private router: Router) { }

//   addCourse(): void {
//     this.courseService.createCourse(this.newCourse).subscribe(
//       (createdCourse: Course) => {
//         // Handle successful creation, e.g., show a success message or navigate to another page
//         console.log('Course created successfully:', createdCourse);
//         this.router.navigate(['/courses']); // Navigate to the courses page after successful creation
//       },
//       (error) => {
//         // Handle error, e.g., display an error message
//         console.error('Error creating course:', error);
//       }
//     );
//   }
// }
