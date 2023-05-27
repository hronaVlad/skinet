import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { Observable, map } from 'rxjs';
import { IOrder, IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private baseUrl:string = environment.apiUrl + 'orders/';

  constructor(private client: HttpClient) { }

  public getDeliveryMethods (): Observable<IDeliveryMethod[]> {
    return this.client.get<IDeliveryMethod[]>(this.baseUrl + 'deliveryMethods').pipe(
      map(data => {
        return data.sort((a, b) => b.price - a.price);
      })
    );
  }

  public createOrder(order: IOrderToCreate): Observable<IOrder> {
    return this.client.post<IOrder>(this.baseUrl, order);
  }
}
