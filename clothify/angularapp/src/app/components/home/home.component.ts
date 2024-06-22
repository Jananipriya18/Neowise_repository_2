import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  slides: any[] = [
    {
      src: './assets/img/item1.jpg',
      title: 'Item 1',
      description: 'Description of Item 1'
    },
    {
      src: './assets/img/item2.jpg',
      title: 'Item 2',
      description: 'Description of Item 2'
    },
    {
      src: './assets/img/item3.jpg',
      title: 'Item 3',
      description: 'Description of Item 3'
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
