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
    this.checkoutService.getDeliveryMethods().subscribe( _ => {
      this.deliveryMethods = _;

       this.selectRadioOption();
    });
  }

  setDeliveryMethod(deliveryMethod: IDeliveryMethod): void {
    this.basketService.setShipping(deliveryMethod.price);
  }

  public selectRadioOption(): void {
    const first = this.deliveryMethods[0];

    console.log(first.id)

    const control = this.checkoutForm.get('deliveryForm').get('deliveryMethod');
    control.setValue(first.id);
    control.markAsTouched();


  }
}
