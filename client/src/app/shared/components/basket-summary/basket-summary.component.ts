import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket, IBasketItem } from '../../models/basket';
import { IOrderItem } from '../../models/order';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {

  @Input() isBasket: boolean;
  @Input() items: IBasketItem[] | IOrderItem[];

  @Output() increase = new EventEmitter<IBasketItem>();
  @Output() decrease = new EventEmitter<IBasketItem>();
  @Output() remove = new EventEmitter<IBasketItem>();

  constructor() { }

  ngOnInit(): void {
  }

  increaseQuantity(item: IBasketItem): void {
    this.increase.emit(item);
  }

  decreaseQuantity(item: IBasketItem): void {
    this.decrease.emit(item);
  }

  removeItem(item: IBasketItem): void {
    this.remove.emit(item);
  }

}
