import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';
import { IOrderTotals } from '../shared/models/basket';
import { Observable } from 'rxjs';
import { CheckoutService } from './checkout.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  public checkoutForm: FormGroup;
  public priceTotals$: Observable<IOrderTotals>;

  constructor(private fb: FormBuilder, private accountService: AccountService, private basketService: BasketService, private checkoutService: CheckoutService) { }

  ngOnInit(): void {
    this.createCheckoutForm();
    this.getAddressFormValues();
    this.getDeliveryValue();

    this.priceTotals$ = this.basketService.priceTotals$;
  }

  private createCheckoutForm(): void {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        city: [null, Validators.required],
        state: [null, Validators.required],
        street: [null, Validators.required],
        postalCode: [null, Validators.required]
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: [null, Validators.required]
      }),
      paymentForm: this.fb.group({
        cardName: [null, Validators.required]
      }),
    });
  }

  private getAddressFormValues(): void {
    this.accountService.getAddress().subscribe(address => {

      if (address) {
        this.checkoutForm.get('addressForm').patchValue(address);
      }
    });
  }

  private getDeliveryValue(): void {
   const selectedDelivery = this.checkoutService.getShipping();

   if (selectedDelivery) {
    this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(selectedDelivery.id.toString());
   }
  }
}
