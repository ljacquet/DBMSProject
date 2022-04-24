import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

type roomate = { house: any, houseId: number, ingredients: any, isOwner: boolean, roomateId: number, roomateIngredients: any, username: string }
type roomateIngredient = { ingredientId: number, ingredientName: string, price: number, quantity: number, quantityUnit: string, ownerName: string, roomateId: number }

@Component({
  selector: 'app-house',
  templateUrl: './house.component.html',
  styleUrls: ['./house.component.css'],
})
export class HouseComponent implements OnInit {
  house: any;
  roomates: roomate[];
  ingredients: roomateIngredient[];
  possibleRecipes: any[] = [];

  constructor(private api: ApiService) {
    this.roomates = [];
    this.ingredients = [];
  }

  async ngOnInit(): Promise<void> {
    this.house = await this.api.getUserHouse();

    if (this.house != null) {
      this.updateRoomates();

      // get possible recipes
      this.getPossibleRecipes();
    }
  }

  private async updateRoomates() {
    let roomateResp = await this.api.getUserRoomates();

    if (roomateResp != null) {
      this.roomates = roomateResp as [];
      this.updateHouseIngredientsArray();
    }
  }

  private async updateHouseIngredientsArray() {
    this.ingredients = await this.api.getHouseIngredients(this.house.houseId);

    console.log(this.ingredients);
  }

  private async getPossibleRecipes() {
    let resp = await this.api.getPossibleRecipes(this.house.houseId) as any[];

    this.possibleRecipes = resp;
  }

  async createHouse(houseName: string) {
    let createdHouse = await this.api.createHouse(houseName);

    // This should be shown in detail in the GUI, if I have time I'll swing back
    if (createdHouse == null) {
      alert("Error Creating House, see console for details");
    }

    this.house = createdHouse;
    this.updateRoomates();
  }

  async joinHouse(houseID: string) {
    let joinedHouse = await this.api.joinHouse(Number.parseInt(houseID));

    // This should be shown in detail in the GUI, if I have time I'll swing back
    if (joinedHouse == null) {
      alert("Error Joining House, see console for details");
    }

    this.house = joinedHouse;
    this.updateRoomates();
  }
}
