// enquiry-list.component.ts
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Enquiry } from 'src/app/models/enquiry.model';
import { AuthService } from 'src/app/services/auth.service';
import { EnquiryService } from 'src/app/services/enquiry.service';

@Component({
  selector: 'app-admin-enquiry-list',
  templateUrl: './admin-enquiry-list.component.html',
  styleUrls: ['./admin-enquiry-list.component.css'],
})
export class AdminEnquiryListComponent implements OnInit {
  // enquiries: Enquiry[] = [];
  enquiries: any[] = [];
  selectedEnquiry: Enquiry;
  isStudent: boolean = false;
  editEnquiryForm: FormGroup;
  editEnquiryModalVisible = false;
  deleteConfirmationState: { [key: number]: boolean } = {};

  constructor(
    private enquiryService: EnquiryService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchAllEnquiries();

    this.authService.userRole$.subscribe((role) => {
      this.isStudent = role === 'Student';
    });

    
    // Initialize the form controls
    this.editEnquiryForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      courseName: ['', Validators.required],
      emailID: ['', Validators.required],
      enquiryType: ['', Validators.required],
      // enquiryDate: ['', Validators.required],
      enquiryDate: [this.getCurrentDateTime(), Validators.required],
    });
  }

  getCurrentDateTime(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }
  
  fetchAllEnquiries(): void {
    this.enquiryService.getAllEnquiries().subscribe(
      (enquiries: Enquiry[]) => {
        this.enquiries = enquiries;
        console.log('Enquiries:', this.enquiries);
      },
      (error) => {
        console.error('Error fetching enquiries:', error);

        // Log additional details if available
        if (error instanceof HttpErrorResponse) {
          console.error('Status:', error.status);
          console.error('Status Text:', error.statusText);
          console.error('Response:', error.error);
        }

        // Log additional details about the error
        console.error('Name:', error.name);
        console.error('Message:', error.message);
      }
    );
  }

  private initializeForm(): void {
    // Initialize the form controls
    this.editEnquiryForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      courseName: ['', Validators.required],
      emailID: ['', Validators.required],
      enquiryType: ['', Validators.required],
      enquiryDate: ['', Validators.required],
    });
  }


}
