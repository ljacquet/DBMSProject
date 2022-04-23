import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-house',
  templateUrl: './house.component.html',
  styleUrls: ['./house.component.css'],
})
export class HouseComponent implements OnInit {
  house: any;

  constructor(private api: ApiService) {
    house = api.getUserHouse();
  }

  ngOnInit(): void {}
}
