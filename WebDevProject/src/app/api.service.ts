import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  readonly APIURL = '/api';

  token: { token: string; expiration: string } | null = null;
  username: string = '';

  constructor(private http: HttpClient, private router: Router) {
    let localData = localStorage.getItem('jwt');

    if (localData != null) {
      this.token = JSON.parse(localData);
    }

    let localUserName = localStorage.getItem('username');
    if (localUserName != null) {
      this.username = localUserName;
    }
  }

  private getAPIRequest(url: string): Promise<{ success: boolean, data: any }> {
    return new Promise((resolve, reject) => {
      if (this.token == null) {
        this.router.navigate(['login']);
      }
      else {
        this.http.get(url, {
          headers: { 'Authorization': 'Bearer ' + this.token.token }
        }).toPromise().then((data) => {
          resolve({ success: true, data });
        }).catch((err) => {
          resolve({ success: false, data: err })
        })
      }
    });
  }

  private postAPIRequest(url: string, data: any): Promise<{ success: boolean, data: any }> {
    return new Promise((resolve, reject) => {
      if (this.token == null) {
        this.router.navigate(['login']);
      }
      else {
        this.http.post(url, data, {
          headers: { 'Authorization': 'Bearer ' + this.token.token }
        }).toPromise().then((data) => {
          resolve({success: true, data});
        }).catch((err) => {
          resolve({success: false, data: err});
        })
      }
    });
  }

  private deleteAPIRequest(url: string): Promise<{ success: boolean, data: any }> {
    return new Promise((resolve, reject) => {
      if (this.token == null) {
        this.router.navigate(['login']);
        reject("not logged in");
        return;
      }
      else {
        this.http.delete(url, {
          headers: {
            'Authorization': 'Bearer ' + this.token.token
          }
        }).toPromise().then(
          (data) => {
            resolve({success: true, data});
          }
        ).catch(
          (err) => {
            resolve({success: false, data: err});
          }
        )
      }
    });
  }

  isLoggedIn() {
    // Token valid if we have one and has not expired.
    return this.token != null && new Date(this.token.expiration) > new Date();
  }

  signOut() {
    this.token = null;
    localStorage.removeItem('jwt');

    this.router.navigate(['login']);
  }

  trySignIn(username: string, password: string) {
    return new Promise((resolve, reject) => {
      let response = this.http
        .post(
          this.APIURL + '/account/login',
          { username, password },
          { responseType: 'json' }
        )
        .toPromise();
      response
        .then((data) => {
          console.log('set token');
          console.log(data);
          this.token = data as { token: string; expiration: string };
          localStorage.setItem('jwt', JSON.stringify(this.token));
          localStorage.setItem('username', username);

          resolve({
            success: true,
            data: data,
          });
          resolve(data);
        })
        .catch((err) => {
          resolve({
            success: false,
            data: err,
          });
        });
    });
  }

  tryRegister(username: string, email: string, password: string) {
    return new Promise((resolve, reject) => {
      let response = this.http
        .post(
          '/api/account/register',
          {
            username,
            email,
            password,
          },
          { responseType: 'json' }
        )
        .toPromise()
        .then((data) => {
          resolve({ success: true, data });
        })
        .catch((err) => {
          resolve({ success: false, data: err });
        });
    });
  }

  async getUserHouse(): Promise<any> {
    let houseResp: { success: boolean, data: any } = await this.getAPIRequest('/api/house/my');

    if (houseResp.success) {
      console.log("House");
      console.log(houseResp.data);

      return houseResp.data;
    }
    else {
      console.log("Error Getting User House");
      console.log(houseResp.data);
      return null;
    }
  }

  async createHouse(name: string) {
    let createResp = await this.postAPIRequest('/api/house/create', { houseName: name });

    if (createResp.success) {
      return createResp.data;
    }
    else {
      console.log("Error Creating House");
      console.log(createResp.data);
      return null;
    }
  }

  async joinHouse(id: number) {
    let joinResp = await this.postAPIRequest('/api/house/join/' + id, {});

    if (joinResp.success) {
      return joinResp.data;
    }
    else {
      console.log("Error Joining House");
      console.log(joinResp.data);
      return null;
    }
  }

  async getUserRoomates(): Promise<any[]> {
    return new Promise((resolve, reject) => {
      this.getAPIRequest('/api/house/roomates')
      .then((data) => { 
        if (data.success) 
        {
          resolve(data.data);
        }
        else {
          console.log("Error Updating Roomates");
          console.log(data);
          resolve([]);
        }
      })
      .catch((err) => {
        console.log("Error Updating Roomates");
        console.log(err);

        resolve([]);
      })
    })
  }

  async getValidIngredients(): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/ingredient');

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error getting valid ingredients");
        console.log(resp);
        resolve([]);
      }
    })
  }

  async getMyIngredients(): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/roomate/ingredients');

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error getting user ingredients");
        console.log(resp);
        reject("Server Error");
      }
    })
  }

  async addUserIngredient(ingredientId: number, amount: number, amountUnit: string, price: number): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/roomate/addingredient', { ingredientId, amount, amountUnit, price });

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Adding Ingredient");
        console.log(resp);

        reject("Error Adding Ingredient");
      }
    })
  }

  async updateUserIngredient(ingredientId: number, amount: number, amountUnit: string, price: number): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/roomate/updateIngredient/' + ingredientId, { amount, amountUnit, price });

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Updating Ingredient");
        console.log(resp);

        reject("Error Updating Ingredient");
      }
    })
  }

  async deleteUserIngredient(ingredientId: number): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.deleteAPIRequest('/api/roomate/removeIngredient/' + ingredientId);

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Deleting Ingredient");
        console.log(resp);

        reject("Error deleting ingredient");
      }
    })
  }

  async createIngredientType(ingredientName: string): Promise<any> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/ingredient/', { ingredientName });

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Creating Ingredient");
        console.log(resp);

        reject("Error Creating Ingredient");
      }
    })
  }

  async deleteIngredientType(ingredientId: number): Promise<void> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.deleteAPIRequest('/api/ingredient/' + ingredientId);

      if (resp.success) {
        resolve();
      }
      else {
        console.log("Error Creating Ingredient");
        console.log(resp);

        reject("Error Creating Ingredient");
      }
    })
  }

  async getHouseIngredients(houseId: number): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/house/ingredients/' + houseId);

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Getting House Ingredients");
        console.log(resp);

        resolve([]);
      }
    })
  }

  async getUserIngredients(roomateId: number): Promise<any> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/roomate/ingredients/' + roomateId);

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Getting User Ingredients");
        console.log(resp);

        resolve(null);
      }
    })
  }

  async getRecipes(): Promise<any[]> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/recipe');

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Getting Recipes");
        console.log(resp);
        resolve([]);
      }
    })
  }

  async createRecipe(name: string, description: string): Promise<any> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/recipe', { name, description });

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Creating Recipe");
        console.log(resp);

        resolve(null);
      }
    })
  }

  async deleteRecipe(recipeId: number) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.deleteAPIRequest('/api/recipe/' + recipeId);

      if (resp.success) {
        resolve(true);
      }
      else {
        console.log("Error Deleting Recipe");
        console.log(resp);

        resolve(null);
      }
    })
  }

  getRecipe(recipeId: number): Promise<any> {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/recipe/' + recipeId);

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Getting Recipe");
        console.log(resp);

        resolve(null);
      }
    })
  }

  updateRecipe(recipeId: number, name: string, description: string) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/recipe/' + recipeId, {
        name, description
      });

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Updating Recipe Metadata");
        console.log(resp);

        resolve(null);
      }
    })
  }

  addRecipeIngredient(recipeId: number, ingredientId: number, amount: string, amountUnit: string) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.postAPIRequest('/api/recipe/addingredient/' + recipeId, {
        ingredientId, amount, unit: amountUnit
      });

      if (resp.success) {
        resolve(true);
      }
      else {
        console.log("Error Adding Ingredient");
        console.log(resp);

        resolve(false);
      }
    })
  }

  deleteRecipeIngredient(recipeId: number, ingredientId: number) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.deleteAPIRequest('/api/recipe/removeIngredient/' + recipeId + "/" + ingredientId);

      if (resp.success) {
        resolve(true);
      }
      else {
        console.log("Error Deleting Ingredient");
        console.log(resp);

        resolve(false);
      }
    })
  }

  getRoomateInfo(roomateId: number) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/roomate/' + roomateId);

      if(resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error getting user data");
        console.log(resp);

        resolve(null);
      }
    })
  }

  getPossibleRecipes(houseId: number) {
    return new Promise(async (resolve, reject) => {
      let resp = await this.getAPIRequest('/api/house/possiblerecipes/' + houseId);

      if (resp.success) {
        resolve(resp.data);
      }
      else {
        console.log("Error Getting Possible Recipes");
        console.log(resp);

        resolve(null);
      }
    })
  }
}
