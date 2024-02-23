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
    courseID: 0,
    title: '',
    description: '',
    enquiryDate: new Date(),
    emailID: '',
    enquiryType: '',
    userId: 0
  }
  editEnquiryModalVisible: boolean = false;

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

  updateEnquiry(enquiryID: number): void {
    // Fetch the selected enquiry from the list
    if (this.authService.isAdmin()) {
    this.selectedEnquiry = this.enquiries.find((enquiry) => enquiry.enquiryID === enquiryID);

    // Check if the selected enquiry exists
    if (selectedEnquiry) {
      // Implement the logic to navigate to the update form or perform inline update
      console.log('Updating Enquiry with ID:', enquiryID);

      this.editEnquiryForm.reset();
      // Example of calling the service method to update the enquiry
      this.editEnquiryForm.patchValue({
        courseName: this.selectedEnquiry.courseName,
        description: this.selectedEnquiry.description,
        duration: this.selectedEnquiry.duration,
        amount: this.selectedEnquiry.amount,
      });
      this.editEnquiryModalVisible = true;
    }
   } else {
      console.error('Enquiry not found');
    }
  }
  else
  {
    console.error('Only student can update enquiries');
  }

  deleteEnquiry(enquiryID: number): void {
    // Example of calling the service method to delete the enquiry
    this.enquiryService.deleteEnquiry(enquiryID).subscribe(
      () => {
        console.log('Enquiry deleted successfully');
        // Refresh the list after successful deletion
        this.fetchAllEnquiries();
      },
      (error) => {
        console.error('Error deleting enquiry:', error);
      }
    );
  }

  
}
