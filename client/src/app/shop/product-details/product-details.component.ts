import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { BreadcrumbService } from 'src/app/core/breadcrumb.service';
import { OnBreadcrumbUpdate } from 'src/app/core/breadcrumb/breadcrumbUpdater';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit, OnBreadcrumbUpdate {

  product: Product;
  quantity: number = 1;
  added: boolean;

  constructor(
    private shopService: ShopService, 
    private activateRoute: ActivatedRoute, 
    private breadcrumbService: BreadcrumbService,
    private basketService: BasketService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() : void {
    const id: number = +this.activateRoute.snapshot.paramMap.get('id');

    this.shopService.getProduct(id)
    .subscribe(
      product =>  {
        this.product = product;
        
        this.updateBreadcrumb(product)
       },
      error => console.log(error));
  }


  updateBreadcrumb(product:any): void {
    this.breadcrumbService.update(product);
  }

  addToCart(): void {
    this.added = true;

    this.basketService.addItem(this.product, this.quantity);
  }

  increaseQty(): void {
    this.quantity++;
  }

  decreaseQty(): void {
    this.quantity--;
  }
}
