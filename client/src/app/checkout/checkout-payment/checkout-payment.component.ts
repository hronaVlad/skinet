import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { IOrder, IOrderToCreate } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { environment } from 'src/environments/environment';
import { Observable, lastValueFrom } from 'rxjs';
import { IIntent } from 'src/app/shared/models/intent';

declare var Stripe: any;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit, AfterViewInit, OnDestroy {

  @Input() checkoutForm: FormGroup;

  @ViewChild('cardNumber', {static: true}) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', {static: true}) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', {static: true}) cardCvcElement: ElementRef;

  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;
  cardErrorHandler = this.onChange.bind(this);

  private intent: IIntent;

  constructor(
    private router: Router,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private basketService: BasketService) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.stripe = Stripe(environment.StripePublishableKey);

    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber'); // 4242 4242 4242 4242
    this.cardExpiry = elements.create('cardExpiry');
    this.cardCvc = elements.create('cardCvc');


    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardCvc.mount(this.cardCvcElement.nativeElement);

    this.cardNumber.addEventListener('change', this.cardErrorHandler);
    this.cardExpiry.addEventListener('change', this.cardErrorHandler);
    this.cardCvc.addEventListener('change', this.cardErrorHandler);
  }

  ngOnDestroy(): void {
      this.cardNumber.destroy();
      this.cardExpiry.destroy();
      this.cardCvc.destroy();
  }

  onChange(event: any): void {
    console.log(event);

    if (event.error) {
      this.cardErrors = event.error.message;
    }
    else {
      this.cardErrors = null;
    }
  }

  async submitOrder() {
    try {
      this.intent = this.checkoutService.getIntent();
      const paymentResult = await this.confirmPayment();

      if (paymentResult.paymentIntent) {
        const order = await lastValueFrom(this.createOrder());
        this.toastr.success('Order created successfully');

        this.basketService.clearBasket();
        this.checkoutService.clearPayments();

        this.router.navigate(['/checkout/success'], {state: order} );
      }else {
        this.toastr.error(paymentResult.error.message, 'Payment error');
      }
    }catch (err: any) {
      this.toastr.error(err);
      console.log(err);
    }
  }

  private createOrder(): Observable<IOrder> {
    const model = this.getOrderToCreate(this.intent.intentId);

    return this.checkoutService.createOrder(model);
  }

  private async confirmPayment() {
      return this.stripe
      .confirmCardPayment(this.intent.clientSecret, {
        payment_method: {
          card: this.cardNumber,
          billing_details: {
            name: this.checkoutForm.get('paymentForm').get('cardName').value
          }
        }
    });
  }

  private getOrderToCreate(intentId: string): IOrderToCreate {
    return {
      basketId: this.basketService.getBasketId(),
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value,
      paymentIntentId: intentId
    };
  }
}
