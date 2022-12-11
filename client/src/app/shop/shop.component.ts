import { Component, OnInit } from '@angular/core';
import { Brand } from '../shared/models/brands';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Type } from '../shared/models/types';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  types: Type[] = [];
  brands: Brand[] = [];
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ];
  sortSelected = 'name';
  typeIdSelected = 0;
  brandIdSelected = 0;
  
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducs();  
    this.getTypes();
    this.getBrands();
  }

  onBrandIdSelected(id: number): void {
    this.brandIdSelected = id;
    this.getProducs();
  }

  onTypeIdSelected(id: number): void {
    this.typeIdSelected = id;
    this.getProducs();
  }

  onSortSelected(sort: string): void {
    this.sortSelected = sort;
    this.getProducs();
  }

  private getProducs(): void {
    this.shopService.getProducts(this.typeIdSelected, this.brandIdSelected, this.sortSelected).subscribe (response => {
      this.products = response.data;
    }, error => {
      console.log(error);
    });
  }

  private getTypes() : void {
    this.shopService.getTypes().subscribe (response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  private getBrands(): void {
    this.shopService.getBrands().subscribe (response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }
}
