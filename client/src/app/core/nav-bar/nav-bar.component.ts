import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  user$: Observable<IUser>;

  constructor(private basketServive: BasketService, private accountService: AccountService) { 
    this.basket$ = basketServive.basket$;
    this.user$ = accountService.user$;
  }

  ngOnInit(): void {
  }

  logout(): void {
    this.accountService.logout();
  }
}
