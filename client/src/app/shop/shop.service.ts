import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  url = 'https://localhost:7154/api/';

  constructor(private client: HttpClient) { }

  getProducts(): Observable<Pagination> {
    return this.client.get<Pagination>(this.url + 'products?pageSize=30');
  } 
}
