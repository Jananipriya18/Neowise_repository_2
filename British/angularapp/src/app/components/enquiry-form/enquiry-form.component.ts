// enquiry-form.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Enquiry } from 'src/app/models/enquiry.model';
import { AuthService } from 'src/app/services/auth.service';
import { EnquiryService } from 'src/app/services/enquiry.service';
import { Course } from 'src/app/models/course.model';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-enquiry-form',
  templateUrl: './enquiry-form.component.html',
  styleUrls: ['./enquiry-form.component.css'],
})
export class EnquiryFormComponent implements OnInit {
  newEnquiryForm: FormGroup;
  isNewEnquiry: boolean = true;
  courses: Course[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private enquiryService: EnquiryService,
    private courseService: CourseService,
    private router: Router
  ) {
    this.newEnquiryForm = this.formBuilder.group({
      enquiryDate: ['', Validators.required],
      // userId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      emailID: ['', [Validators.required, Validators.email]],
      enquiryType: ['', Validators.required],
      courseName: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchAllCourses(); // Fetch courses on component initialization
  }

  fetchAllCourses(): void {
    this.courseService.getAllCourses().subscribe(
      (courses: Course[]) => {
        this.courses = courses;
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }

  

createEnquiry(): void {
  console.log(this.newEnquiryForm);
  console.log(this.authService.isCustomer(),this.newEnquiryForm.valid)
  if (this.authService.isCustomer() && this.newEnquiryForm.valid) {
    const newEnquiry: Enquiry = this.newEnquiryForm.value as Enquiry;

    this.enquiryService.createEnquiry(newEnquiry).subscribe(
      (createdEnquiry: Enquiry) => {
        console.log('Enquiry created successfully:', createdEnquiry);
        // Log the response from the server
        console.log('Response:', createdEnquiry);

        // Navigate to the enquiries page after successful creation
        this.router.navigate(['/enquiries']);
      },
      (error) => {
        console.error('Error creating enquiry:', error);

        // Log the error response from the server
         console.error('Error Response:', error);
         // You can also handle specific error cases here if needed
        }
    );
   } else {
     console.error('Only customers can create enquiries or form is not valid');
   }
}


}
