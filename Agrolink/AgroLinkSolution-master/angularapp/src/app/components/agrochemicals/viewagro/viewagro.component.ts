import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AgrochemicalService } from 'src/app/services/agrochemical.service';

@Component({
  selector: 'app-viewagro',
  templateUrl: './viewagro.component.html',
  styleUrls: ['./viewagro.component.css']
})
export class ViewagroComponent implements OnInit {

  availableAgroChemicals: any[] = [];
  showDeletePopup = false;
  agroToDelete: number | null = null;
  searchValue = '';
  sortValue = 0;
  page: number = 1;
  limit = 5;
  maxRecords = 1;
  totalPages = 1;
  status: string = ''; // Add this line
  filteredagro = [];
  searchField = '';
  errorMessage: string = '';
  allagrochemicals: any[] = []; // Declare the allcrops property

  constructor(private router: Router, private agroService: AgrochemicalService) {}

  ngOnInit(): void {
    this.fetchAvailableAgroChemicals();
  }

  fetchAvailableAgroChemicals() {
    this.agroService.getAllAgrochemicals().subscribe(
      (data: any) => {
        this.availableAgroChemicals = data;
        this.maxRecords = this.availableAgroChemicals.length;
        this.allagrochemicals = data; // Populate allcrops with the initial list of crops
        this.totalPages = Math.ceil(this.maxRecords / this.limit);        
        console.log('Available Crops:', this.availableAgroChemicals);
        this.filteredagro=this.availableAgroChemicals;
      },
      (error) => {
        // Handle error
        console.error('Error fetching AgroChenicals:', error);
      }
    );
  }




  handleDeleteClick(agroId: number) {
    this.agroToDelete = agroId;
    this.showDeletePopup = true;
  }

  navigateToEditLoan(id: string) {
    // this.router.navigate(['/admin/editloan', id]);
  }

  handleConfirmDelete() {
    if (this.agroToDelete) {
      this.agroService.deleteAgrochemical(this.agroToDelete).subscribe(
        (response) => {
          console.log('Crop deleted successfully', response);
          this.closeDeletePopup();
          this.fetchAvailableAgroChemicals();
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error deleting AgroChemicals:', error);
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
    this.agroToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }

  updateAvailableAgro(newAgro: any[]) {
    this.availableAgroChemicals = newAgro;
  }

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    if (searchValue) {
      this.availableAgroChemicals = this.filterAgroCheniacal(searchValue);
    } else {
      this.availableAgroChemicals = this.allagrochemicals;
    }
  }

  filterAgroCheniacal(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') return this.availableAgroChemicals;
    return this.availableAgroChemicals.filter(
      (agroChemical) =>
        agroChemical.cropType.toLowerCase().includes(searchLower) ||
      agroChemical.Description.toLowerCase().includes(searchLower)
    );
  }


}
