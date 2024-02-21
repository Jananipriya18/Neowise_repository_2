// enquiry-list.component.ts
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

  constructor(private enquiryService: EnquiryService) {}

  ngOnInit(): void {
    this.fetchAllEnquiries();
  }

  fetchAllEnquiries(): void {
    this.enquiryService.getAllEnquiries().subscribe(
      (enquiries: Enquiry[]) => {
        this.enquiries = enquiries;
      },
      (error) => {
        console.error('Error fetching enquiries:', error);
      }
    );
  }
}
