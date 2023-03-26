import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject,  map,  Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, IBasket } from '../shared/models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  url = environment.apiUrl + 'basket';

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private client: HttpClient) { }
  
  public get(id: string) {
    return this.client.get<IBasket>(this.url + `?id=${id}`)
      .pipe(
        map((basket: IBasket) => {
            this.basketSource.next(basket);
        })
      );
  }

  public delete(id: string): Observable<boolean> {
    return this.client.delete<boolean>(this.url + `?id=${id}`);
  }

  public update(item: Basket) {
    return this.client.put<IBasket>(this.url, item)
      .subscribe( (response: IBasket) => {
        this.basketSource.next(response);
      }, error => {
          console.log(error);
      });
    }

    public getCurrentBasketValue(): IBasket {
      return this.basketSource.value;
    }
}
