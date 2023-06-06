import { OrderService } from './orders.service';
import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { IOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderResolver implements Resolve<IOrder> {

  constructor(private orderSerivice: OrderService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IOrder> {

    const id = +route.paramMap.get('id');

    if (id) {
      return this.orderSerivice.getOrder(id);
    }

    return of(null);
  }
}
