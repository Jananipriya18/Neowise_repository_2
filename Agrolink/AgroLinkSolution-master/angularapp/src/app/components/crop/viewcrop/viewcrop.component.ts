import { Component, OnInit } from '@angular/core';
import { CropService } from 'src/app/services/crop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-viewcrop',
  templateUrl: './viewcrop.component.html',
  styleUrls: ['./viewcrop.component.css']
})
export class ViewcropComponent implements OnInit {

  availableCrops: any[] = [];
  showDeletePopup = false;
  cropToDelete: number | null = null;
  searchValue = '';
  sortValue = 0;
  page: number = 1;
  limit = 5;
  maxRecords = 1;
  totalPages = 1;
  status: string = ''; // Add this line
  filteredcrops = [];
  searchField = '';
  errorMessage: string = '';
  allcrops: any[] = []; // Declare the allcrops property

  constructor(private router: Router, private cropService: CropService) {}

  ngOnInit(): void {
    this.fetchAvailableCrops();
  }

  fetchAvailableCrops() {
    this.cropService.getAllCrops().subscribe(
      (data: any) => {
        this.availableCrops = data;
        this.maxRecords = this.availableCrops.length;
        this.allcrops = data; // Populate allcrops with the initial list of crops
        this.totalPages = Math.ceil(this.maxRecords / this.limit);        
        console.log('Available Crops:', this.availableCrops);
        this.filteredcrops=this.availableCrops;
      },
      (error) => {
        // Handle error
        console.error('Error fetching crops:', error);
      }
    );
  }




  handleDeleteClick(cropId: number) {
    this.cropToDelete = cropId;
    this.showDeletePopup = true;
  }

  navigateToEditLoan(id: string) {
    // this.router.navigate(['/admin/editloan', id]);
  }

  handleConfirmDelete() {
    if (this.cropToDelete) {
      this.cropService.deleteCrop(this.cropToDelete).subscribe(
        (response) => {
          console.log('Crop deleted successfully', response);
          this.closeDeletePopup();
          this.fetchAvailableCrops();
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error deleting Crop:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }



  logout() {
    // Implement logout logic
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('userRole');
    this.router.navigate(['/login']);
  }

  closeDeletePopup() {
    this.cropToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }

  updateAvailableLoans(newLoans: any[]) {
    this.availableCrops = newLoans;
  }

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    if (searchValue) {
      this.availableCrops = this.filterLoans(searchValue);
    } else {
      this.availableCrops = this.allcrops;
    }
  }

  filterLoans(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') return this.availableCrops;
    return this.availableCrops.filter(
      (crop) =>
        crop.cropType.toLowerCase().includes(searchLower) ||
        crop.Description.toLowerCase().includes(searchLower)
    );
  }

}
