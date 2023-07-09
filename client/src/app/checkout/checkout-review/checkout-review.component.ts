import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket, IOrderTotals } from 'src/app/shared/models/basket';
import { CheckoutService } from '../checkout.service';
import { IIntent } from 'src/app/shared/models/intent';
import { CdkStepper } from '@angular/cdk/stepper';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {

  @Input() appStepper: CdkStepper;

  basket$: Observable<IBasket>;
  priceTotal$: Observable<IOrderTotals>;
  intent: IIntent;

  constructor(private basketService: BasketService, private toast: ToastrService, private checkoutService: CheckoutService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.priceTotal$ = this.basketService.priceTotals$;
    this.intent = this.checkoutService.getIntent();
  }

  goToPayment(): void {
    const total = this.basketService.getTotalSum();
    const intentId = this.intent != null ? this.intent.intentId : null;

    this.checkoutService.createPaymentIntent(intentId, total).subscribe({
      next: data => {
        this.checkoutService.setIntent(data);
        this.toast.success('Intent is created', data.status);

        this.appStepper.next();
      }
    })
  }
}
