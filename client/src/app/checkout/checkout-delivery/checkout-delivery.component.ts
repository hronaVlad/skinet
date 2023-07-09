import { Component, Input, OnInit } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { IDeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm: FormGroup;
  deliveryMethods: IDeliveryMethod[] = [];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.loadDeliveryMethods();
  }

  public setDeliveryMethod(deliveryMethod: IDeliveryMethod): void {
    this.checkoutService.setShipping(deliveryMethod);
  }

  private loadDeliveryMethods() {
    this.checkoutService.getDeliveryMethods().subscribe( {
      next: data => this.deliveryMethods = data,
      error: error => console.log(error)
    });
  }
}
