import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { IOrderTotals } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {

  order!: IOrder;
  orderTotals: IOrderTotals;

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe( data => {
         this.order = data['order'];

         //console.log('order', this.order);

         this.orderTotals = {
          shipping : this.order.shippingPrice,
          subtotal : this.order.subTotal,
          total : this.order.total
         } as IOrderTotals;
    });
  }

}
