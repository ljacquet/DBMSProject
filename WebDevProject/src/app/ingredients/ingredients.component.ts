import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-ingredients',
  templateUrl: './ingredients.component.html',
  styleUrls: ['./ingredients.component.css']
})
export class IngredientsComponent implements OnInit {

  roomateId: any = null;
  roomate: any = null;
  ingredients: any[] = [];

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(async (params: Params) => {
      this.roomateId = params['id'];
      this.roomate = await this.api.getRoomateInfo(this.roomateId);
      this.ingredients = await this.api.getUserIngredients(this.roomateId);

      console.log(this.roomate);
      console.log(this.ingredients);
    });
  }
}
