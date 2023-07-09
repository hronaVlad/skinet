import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBasketItem, IOrderTotals } from '../shared/models/basket';
import { Product } from '../shared/models/product';
import { BasketApiService } from './basket-api.service';
import { v4 as uuidv4 } from 'uuid';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService extends BasketApiService {
    localStorage_basket_name = "basket_id";

    private priceTotalsSource = new BehaviorSubject<IOrderTotals>(null);
    priceTotals$ = this.priceTotalsSource.asObservable();

    shipping: number;

    constructor(client: HttpClient) {
      super(client);
    }

    public init(): void {
      this.get(this.getOrCreateBasketId())
      .subscribe(_ => {
        this.calculateTotals();
        console.log('Basket initialized');
      });
    }

    public getBasketId(): string {
      return this.getValue().id;
    }

    public getTotalSum(): number {
      return this.priceTotalsSource.getValue().total;
    }

    public addItem(product: Product, quantity: number = 1 ): void {
      const basket = this.getValue();
      const index = basket.items.findIndex(_ => _.id === product.id);

      if (index === -1) { // create
        basket.items.push(this.mapProductToBasketItem(product, quantity));
      } else { //update
        basket.items[index].quantity += quantity;
      }

      this.saveBasket();
    }

    public removeItem(item: IBasketItem): void {
      const basket = this.getValue();

      basket.items = basket.items.filter(_ => _.id !== item.id);

      this.saveBasket();
    }

    public increaseItem(item: IBasketItem): void {
      const basket = this.getValue();
      const found = basket.items.find(_ => _.id === item.id);

      if (found) {
        found.quantity++;

        this.saveBasket();
      }
    }

    public decreaseItem(item: IBasketItem): void {
      const basket = this.getValue();
      const found = basket.items.find(_ => _.id === item.id);

      if (found) {

        if (found.quantity > 1) {
          found.quantity--;

          this.saveBasket();
        }
      }
    }

    public clearBasket(): void {
      this.basketSource.next(null);
      this.priceTotalsSource.next(null);
      localStorage.removeItem(this.localStorage_basket_name);

      this.init();
    }

    public deleteBasket(): void {
      const basket = this.getValue();

      if (basket) {
        this.delete(basket.id).subscribe( {
            next: () => {
              this.clearBasket();
            },
            error: error => {
              console.log(error);
              throw error;
            }
        });
      }
    }

    private saveBasket() {
      const basket = this.getValue();

      //save basket id into the localStorage
      localStorage.setItem(this.localStorage_basket_name, basket.id);

      // send an API update request
      this.update(basket);

      this.calculateTotals();
    }

    private getOrCreateBasketId(): string {
      return localStorage.getItem(this.localStorage_basket_name) ?? uuidv4();
    }

    public calculateTotals(): void {
      const basket = this.getValue();

      if (basket) {
        const shipping = this.shipping;
        const subtotal = basket.items.reduce( (val, cur) => (cur.price * cur.quantity) + val, 0);
        const total = shipping + subtotal;
        this.priceTotalsSource.next({shipping, subtotal, total});
      }
    }

    private mapProductToBasketItem(product: Product, quantity: number): IBasketItem{
      return {
        id: product.id,
        productName: product.name,
        pictureUrl: product.pictureUrl,
        price: product.price,
        quantity: quantity,
        brand: product.productBrand,
        type: product.productType
      };
    }
}
