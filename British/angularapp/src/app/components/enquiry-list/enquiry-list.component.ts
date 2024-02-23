// enquiry-list.component.ts
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Enquiry } from 'src/app/models/enquiry.model';
import { AuthService } from 'src/app/services/auth.service';
import { EnquiryService } from 'src/app/services/enquiry.service';

@Component({
  selector: 'app-enquiry-list',
  templateUrl: './enquiry-list.component.html',
  styleUrls: ['./enquiry-list.component.css'],
})
export class EnquiryListComponent implements OnInit {
  enquiries: Enquiry[] = [];
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
      enquiryDate: ['', Validators.required],
    });
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
    if (this.authService.isStudent()) {
      this.selectedEnquiry = this.enquiries.find((enquiry) => enquiry.enquiryID === enquiryID);

      // Check if the selected enquiry exists
      if (this.selectedEnquiry) {
        // Initialize the form controls if not already initialized
        if (!this.editEnquiryForm) {
          this.initializeForm();
        }

        console.log('Updating Enquiry with ID:', enquiryID);

        this.editEnquiryForm.reset();
        // Example of calling the service method to update the enquiry
        this.editEnquiryForm.patchValue({
          title: this.selectedEnquiry.title,
          description: this.selectedEnquiry.description,
          courseName: this.selectedEnquiry.course.courseName,
          emailID: this.selectedEnquiry.emailID,
          enquiryType: this.selectedEnquiry.enquiryType,
          enquiryDate: this.selectedEnquiry.enquiryDate,
        });
        this.editEnquiryModalVisible = true;
      } else {
        console.error('Enquiry not found');
      }
    } else {
      console.error('Only student can update enquiries');
    }
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


  saveChanges(): void {
    console.log('Save changes method called');
    if (this.authService.isStudent()) {
      const updatedEnquiry: Enquiry = {
        ...this.selectedEnquiry,
        ...this.editEnquiryForm.value,
      };
  
      console.log('Updated enquiry:', updatedEnquiry);
  
      // Update the enquiry in the database
      this.enquiryService
        .updateEnquiry(this.selectedEnquiry.enquiryID, updatedEnquiry)
        .subscribe(
          (updatedEnquiry: Enquiry) => {
            console.log('Enquiry updated successfully:', updatedEnquiry);
            // Fetch all enquiries after successful update
            this.fetchAllEnquiries();
            // Close the edit modal
            this.closeEditModal();
          },
          (error) => {
            console.error('Error updating enquiries:', error);
          }
        );
    } else {
      console.error('Only students can update enquiries');
    }
  }
  
  showDeleteConfirmation(enquiry: Enquiry): void {
    // Set deleteConfirmationState for the specific enquiry to true
    this.deleteConfirmationState[enquiry.enquiryID] = true;
  }

  deleteEnquiry(enquiryID: number): void {
    if (this.authService.isStudent()) {
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
    } else {
      console.error('Only students can delete enquiries');
    }
    this.deleteConfirmationState[enquiryID] = false;
  }

  cancelDelete(enquiry: Enquiry): void {
    // Reset deleteConfirmationState for the specific enquiry
    this.deleteConfirmationState[enquiry.enquiryID] = false;
  }

  closeEditModal(): void {
    // Reset form and hide the edit modal
    this.editEnquiryForm.reset();
    this.editEnquiryModalVisible = false;
  }
}
