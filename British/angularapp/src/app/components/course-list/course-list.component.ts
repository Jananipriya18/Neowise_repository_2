// course-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';


@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css'],
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    this.courseService.getAllCourses().subscribe((data) => {
      this.courses = data;
    });
  }
}
