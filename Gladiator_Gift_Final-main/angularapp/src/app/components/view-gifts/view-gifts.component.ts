import { Component } from '@angular/core';
import { Router } from '@angular/router';
// import { Gift } from 'src/app/models/gift.model';
import { GiftService } from 'src/app/services/gift.service';

interface Gift {
  giftId: string;
}

@Component({
  selector: 'app-view-gifts',
  templateUrl: './view-gifts.component.html',
  styleUrls: ['./view-gifts.component.css']
})
export class ViewGiftsComponent {
  gifts: Gift[] = [];
  constructor(private router: Router,private giftService: GiftService) { }
  
  // ngOnInit(): void {
  //   this.giftService.viewAllGifts().subscribe((gifts: Gift[]) => {
  //     console.log(gifts); // Add this line
  //     this.gifts = gifts;
  //   }, (error) => {
  //     console.error(error);
  //   });
  // }

  ngOnInit(): void {
    this.giftService.viewAllGifts().subscribe((response: any) => {
      this.gifts = response.$values; // assuming $values is the array of gifts
    }, (error) => {
      console.error(error);
    });
  }

  editGift(gift: Gift): void {
    // Serialize the gift object and pass it as a query parameter
    const queryParams = {
      gift: JSON.stringify(gift)
    };

    // Navigate to the edit page with the serialized gift object
    this.router.navigate(['/admin/editgift', gift.giftId], { queryParams: queryParams });
  }

  // deleteGift(gift: Gift): void {
  //   this.giftService.deleteGift(gift.giftId).subscribe(() => {
  //     console.log('Gift deleted successfully');
  //     this.gifts = this.gifts.filter(g => g.giftId !== gift.giftId);
  //   }, (error) => {
  //     console.error(error);
  //   });
  // }
  deleteGift(gift: any): void {
    const userConfirmed = window.confirm(`Are you sure you want to delete the gift '${gift.giftType}'?`);

    if (userConfirmed) {
      this.giftService.deleteGift(gift.giftId).subscribe(() => {
            console.log('Gift deleted successfully');
            this.gifts = this.gifts.filter(g => g.giftId !== gift.giftId);
          }, (error) => {
            console.error(error);
          });
    }
  }

}
