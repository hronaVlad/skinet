import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';
import { CheckoutService } from './checkout/checkout.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit{

  private enableInitialization = true;

  constructor(private basketService: BasketService, private accountService: AccountService, private checkoutService: CheckoutService) {}

  ngOnInit(): void {

    if(this.enableInitialization) {
      this.basketService.init();
      this.checkoutService.init();
      this.accountService.loadUser();
    }


  }
}
