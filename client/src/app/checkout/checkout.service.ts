import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { IOrder, IOrderToCreate } from '../shared/models/order';
import { BasketService } from '../basket/basket.service';
import { IIntent } from '../shared/models/intent';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  private localStorage_delivery_id = "selected_delivery_id";
  private localStorage_intent = "selected_intent";

  private baseOrderUrl:string = environment.apiUrl + 'orders/';
  private basePaymentUrl:string = environment.apiUrl + 'payments/';

  private deliveryMethodSource = new  BehaviorSubject<IDeliveryMethod>(null);
  deliveryMethod$ = this.deliveryMethodSource.asObservable();

  constructor(private client: HttpClient, private basketService: BasketService) { }

  public init(): void {
    const deliveryId = localStorage.getItem(this.localStorage_delivery_id);

    if (deliveryId) {
      this.getDeliveryMethods().subscribe ({
        next: data => {
          const found = data.find(_ => _.id === +deliveryId);

          if (found) {
            this.deliveryMethodSource.next(found);

            this.basketService.shipping = found.price;
            this.basketService.calculateTotals();
          }
        },
        error: error => console.log(error)
      });
    }
  }

  public getDeliveryMethods (): Observable<IDeliveryMethod[]> {
    return this.client.get<IDeliveryMethod[]>(this.baseOrderUrl + 'deliveryMethods').pipe(
      map(data => {
        return data.sort((a, b) => b.price - a.price);
      })
    );
  }

  public createOrder(order: IOrderToCreate): Observable<IOrder> {
    return this.client.post<IOrder>(this.baseOrderUrl, order);
  }

  public createPaymentIntent(intentId: string, total: number): Observable<IIntent> {
    return this.client.post<IIntent>(this.basePaymentUrl, {intentId, total});
  }

  public setShipping(method: IDeliveryMethod): void {
    localStorage.setItem(this.localStorage_delivery_id, method.id.toString());

    this.deliveryMethodSource.next(method);

    this.basketService.shipping = method.price;
    this.basketService.calculateTotals();
  }

  public getShipping(): IDeliveryMethod {
    return this.deliveryMethodSource.getValue();
  }


  public setIntent(intnet: IIntent): void {
    const intnetString = JSON.stringify(intnet);

    localStorage.setItem(this.localStorage_intent, intnetString);
  }

  public getIntent(): IIntent {
    const intentString = localStorage.getItem(this.localStorage_intent);

    const intent = JSON.parse(intentString) as IIntent;

    return intent;
  }

  public clearPayments(): void {
    localStorage.removeItem(this.localStorage_intent);
  }
}
