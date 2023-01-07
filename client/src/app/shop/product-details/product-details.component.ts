import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: Product;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() : void {
    const idParam = this.activateRoute.snapshot.paramMap.get('id');
    const id: number = idParam ? +idParam : 0;

    this.shopService.getProduct(id)
    .subscribe(
      product => this.product = product,
      error => console.log(error));
  }

}
