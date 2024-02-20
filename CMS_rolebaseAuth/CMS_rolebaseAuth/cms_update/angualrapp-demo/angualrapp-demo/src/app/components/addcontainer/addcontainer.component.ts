import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ContainerService } from 'src/app/services/container.service';
import { Container } from 'src/app/model/container.model';

@Component({
  selector: 'app-addcontainer',
  templateUrl: './addcontainer.component.html',
  styleUrls: ['./addcontainer.component.css']
})
export class AddcontainerComponent implements OnInit {

  showModal = false;
  today = new Date().toISOString().split('T')[0];

  addContainerForm = new FormGroup({
    Type: new FormControl('', Validators.required),
    Status: new FormControl('', Validators.required),
    Capacity: new FormControl('', Validators.required),
    Location: new FormControl('', Validators.required),
    Weight: new FormControl('', Validators.required),
    Owner: new FormControl('', Validators.required),
    CreationDate: new FormControl('', Validators.required),

  });

  constructor(private containerService: ContainerService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.addContainerForm.valid) {
      const newContainer: Container = this.addContainerForm.value;
      this.containerService.addContainer(newContainer).subscribe(response => {
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