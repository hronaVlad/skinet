import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {

  validationError: any;

  constructor(private client: HttpClient) { }

  get500() {
    this.makeUrl('buggy/servererror').subscribe( response => console.log(response));
  }

  get400(){
    this.makeUrl('buggy/badrequest').subscribe( response => console.log(response));
  }

  get404(){
    this.makeUrl('buggy/notfound').subscribe( response => console.log(response));
  }


  get404Validation(){
    this.makeUrl('product/aaaabbb').subscribe( response => {
      console.log(response)
    },
    error => {
      this.validationError =error.error.message;
      
    });
  }

  private makeUrl(url: string){
    return this.client.get(environment.apiUrl + url);
  }

}
