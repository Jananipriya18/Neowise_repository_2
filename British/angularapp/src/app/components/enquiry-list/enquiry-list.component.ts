// enquiry-list.component.ts
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Enquiry } from 'src/app/models/enquiry.model';
import { EnquiryService } from 'src/app/services/enquiry.service';

@Component({
  selector: 'app-enquiry-list',
  templateUrl: './enquiry-list.component.html',
  styleUrls: ['./enquiry-list.component.css'],
})
export class EnquiryListComponent implements OnInit {
  enquiries: Enquiry[] = [];
  newEnquiry: Enquiry = {
    enquiryID: 0,
    userID: 0,
    courseID: 0,
    title: '',
    description: '',

  }

  constructor(private enquiryService: EnquiryService) {}

  ngOnInit(): void {
    this.fetchAllEnquiries();
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
}
