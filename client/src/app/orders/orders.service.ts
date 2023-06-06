import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseUrl = environment.apiUrl + 'orders/';

  constructor(private client: HttpClient) { }

  public getOrder(id: number): Observable<IOrder> {
    return this.client.get<IOrder>(this.baseUrl + id);
  }

  public getOrders(): Observable<IOrder[]> {
    return this.client.get<IOrder[]>(this.baseUrl);
  }
}
