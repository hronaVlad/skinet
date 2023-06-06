import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { OrderService } from './orders.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: IOrder[];

  constructor(private orderService: OrderService, private toastService: ToastrService) { }

  ngOnInit(): void {
   this.getOrders();
  }

  private getOrders(): void {
    this.orderService.getOrders().subscribe ({
      next: data => {
        this.orders = data;
        console.log(this.orders);
      },
      error: err => {
        console.log(err);
        this.toastService.error(err);
      }
    });
  }
}
