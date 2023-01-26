import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'src/app/core/breadcrumb.service';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: Product;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() : void {
    const idParam = this.activateRoute.snapshot.paramMap.get('id');
    const id: number = idParam ? +idParam : 0;

    this.shopService.getProduct(id)
    .subscribe(
      product =>  {
        this.product = product;
        this.updateBreadcrumbs(product)
       },
      error => console.log(error));
  }


  updateBreadcrumbs(product: Product){
     this.breadcrumbService.updateBreadcrumbs(product.id, product);
  }

}
