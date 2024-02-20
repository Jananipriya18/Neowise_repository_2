import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AssignmentService } from 'src/app/services/assignment.service';
import { Assignment } from 'src/app/model/assignment.model';

@Component({
  selector: 'app-addassignment',
  templateUrl: './addassignment.component.html',
  styleUrls: ['./addassignment.component.css']
})
export class AddAssignmentComponent implements OnInit {

  showModal = false;

  addAssignmentForm = new FormGroup({
    ContainerId: new FormControl('', Validators.required),
    UserId: new FormControl('', Validators.required),
    Status: new FormControl('', Validators.required),
    Route: new FormControl('', Validators.required),
    Shipment: new FormControl('', Validators.required),
    Destination: new FormControl('', Validators.required),
  });

  constructor(private assignmentService: AssignmentService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.addAssignmentForm.valid) {
      const newAssignment: Assignment = this.addAssignmentForm.value;
      this.assignmentService.addAssignment(newAssignment).subscribe(response => {
        if (response.status === 201) {
          this.showModal = true;
        }
        console.log(response);
        // Handle response here
      }, error => {
        console.log(error);
        // Handle error here
      });
    }
  }

  closeModal() {
    this.showModal = false;
  }
}