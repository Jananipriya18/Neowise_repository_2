import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ContainerService } from 'src/app/services/container.service';


@Component({
  selector: 'app-viewcontainer',
  templateUrl: './viewcontainer.component.html',
  styleUrls: ['./viewcontainer.component.css']
})
export class ViewcontainerComponent implements OnInit {
  containers: any[] = [];
  showModal: boolean = false;
  editModal: boolean = false;
  updateSuccessModal: boolean = false; 
  containerToDelete: number | null = null; // Add this line
  containerToEdit: any;
  containerForm: FormGroup;


  constructor(private containerService: ContainerService) { 
    this.containerForm = new FormGroup({
      // Define your form controls here. For example:
      Type: new FormControl(''),
      Status: new FormControl(''),
      Capacity: new FormControl(''),
      Location: new FormControl(''),
      Weight: new FormControl(''),
      Owner: new FormControl('')
    });
  }

  ngOnInit() {
    this.containerService.getContainers().subscribe((data: any[]) => {
      this.containers = data;
    });
  }

  editContainer(id: number) {
    
    this.containerService.getContainerById(id).subscribe(container => {
      console.log(container);
      this.containerToEdit = container;
      this.containerForm.setValue({
        Type: container.Type,
        Status: container.Status,
        Capacity: container.Capacity,
        Location: container.Location,
        Weight: container.Weight,
        Owner: container.Owner
      });
      // Show the edit modal
      this.editModal = true;
    });
  }

  deleteContainer(containerId: number) {
    this.containerService.deleteContainer(containerId).subscribe(
      response => {
        console.log(response);
        // Remove the container from the containers array
        this.containers = this.containers.filter(container => container.ContainerId !== containerId);
      },
      error => {
        console.error(error);
      }
    );
  }

  toggleModal(containerId: number) {

    this.showModal = !this.showModal;
    this.containerToDelete = containerId; // Save the containerId for later
  }

onSubmit() {
  this.containerService.updateContainer(this.containerToEdit.ContainerId, this.containerForm.value).subscribe(response => {
    // Handle successful update
    if (response.ok) {
      this.updateSuccessModal = true;
      setTimeout(() => {
        this.updateSuccessModal = false;
      }, 2000); // Close the modal after 5 seconds
    }
    this.editModal = false;
  });
}
}