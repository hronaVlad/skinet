import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Brand } from '../shared/models/brands';
import { Product } from '../shared/models/product';
import { ProductFilterParams } from '../shared/models/productFilterParms';
import { Type } from '../shared/models/types';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search') searchTerm: ElementRef;

  products: Product[] = [];
  types: Type[] = [];
  brands: Brand[] = [];
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ];

  filterParams = new ProductFilterParams();
  totalItems: number = 0;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducs();  
    this.getTypes();
    this.getBrands();
  }

  onBrandIdSelected(id: number): void {
    this.filterParams.brandId = id;
    this.filterParams.pageIndex = 1;
    this.getProducs();
  }

  onTypeIdSelected(id: number): void {
    this.filterParams.typeId = id;
    this.filterParams.pageIndex = 1;
    this.getProducs();
  }

  onSortSelected(sort: string): void {
    this.filterParams.sort = sort;
    this.getProducs();
  }

  onSearch(event: any): void {
    event.preventDefault();
    this.filterParams.search = this.searchTerm.nativeElement.value;
    this.getProducs();
  }

  onReset(event: any): void {
    event.preventDefault();
    this.filterParams = new ProductFilterParams();
    this.searchTerm.nativeElement.value = '';
    this.getProducs();
  }

  onIndexChanged(page: number): void {
    this.filterParams.pageIndex = page;
    this.getProducs();
  }

  private getProducs(): void {
    this.shopService.getProducts(this.filterParams).subscribe (response => {
      this.products = response.data;
      this.totalItems = response.count;
      this.filterParams.pageIndex = response.pageIndex;
      this.filterParams.pageSize = response.pageSize;
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
