import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Brand } from '../shared/models/brands';
import { Type } from '../shared/models/types';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  url = 'https://localhost:7154/api/';

  constructor(private client: HttpClient) { }

  getProducts(typeId?: number, brandId?: number, sortSelected?: string): Observable<Pagination> {
    let params = new HttpParams()
      .set('pageSize', 10);

    if (typeId) {
      params = params.set('typeId', typeId);
    }

    if (brandId) {
      params = params.set('brandId', brandId);
    }

    if (sortSelected) {
      params = params.set('sort', sortSelected);
    }

    return this.client.get<Pagination>(this.url + 'products', { observe: 'body', params: params });
  } 

  getTypes(): Observable<Type[]> {
    return this.client.get<Type[]>(this.url + 'products/types');
  }

  getBrands(): Observable<Brand[]> {
    // return of(['addidas', 'nike', 'newbalance']);
    return this.client.get<Brand[]>(this.url + 'products/brands');
  }
}
