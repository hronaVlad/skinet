import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit{
  title = 'Hello, Кате';
  products: any[] = [];

  constructor(private client: HttpClient) {

  }

  ngOnInit(): void {
    this.client.get('https://localhost:7154/api/products?pageSize=30').subscribe( (response: any) => {
      this.products = response.data;
      console.log(response);
    },
    error => console.log(error));
  }
}
