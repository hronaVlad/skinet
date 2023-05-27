import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { IOrder, IOrderToCreate } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  constructor(private router: Router, private checkoutService: CheckoutService, private toastr: ToastrService, private basketService: BasketService) { }

  ngOnInit(): void {
  }

  submitOrder(): void {
    const order = this.getOrderToCreate();

    this.checkoutService.createOrder(order).subscribe ({
      next: (order: IOrder) => {
        console.log(order);
        this.basketService.clearBasket();

        this.router.navigate(['/checkout/success'], {state: order} );
      },
      error: error => {
        this.toastr.error(error);
        console.log(error);
      }
    });
  }

  private getOrderToCreate(): IOrderToCreate {
    return {
      basketId: this.basketService.getBasketId(),
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    };
  }
}
