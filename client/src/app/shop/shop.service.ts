import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Brand } from '../shared/models/brands';
import { Type } from '../shared/models/types';
import { Pagination } from '../shared/models/pagination';
import { ProductFilterParams } from '../shared/models/productFilterParms';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  url = 'https://localhost:7154/api/';

  constructor(private client: HttpClient) { }

  getProducts(filterParams: ProductFilterParams): Observable<Pagination> {
    let params = new HttpParams();

    if (filterParams.typeId > 0) {
      params = params.set('typeId', filterParams.typeId);
    }

    if (filterParams.brandId > 0) {
      params = params.set('brandId', filterParams.brandId);
    }

    if (filterParams.search) {
      params = params.set('search', filterParams.search);
    }

    params = params.set('sort', filterParams.sort);
    params = params.set('pageIndex', filterParams.pageIndex);
    params = params.set('pageSize', filterParams.pageSize);

    return this.client.get<Pagination>(this.url + 'products', { observe: 'body', params: params });
  } 

  getTypes(): Observable<Type[]> {
    return this.client.get<Type[]>(this.url + 'products/types');
  }

  getBrands(): Observable<Brand[]> {
    return this.client.get<Brand[]>(this.url + 'products/brands');
  }
}
