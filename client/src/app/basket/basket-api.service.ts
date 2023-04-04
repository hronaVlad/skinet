import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, IBasket } from '../shared/models/basket';

export class BasketApiService {
  url = environment.apiUrl + 'basket';

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  
  constructor(private client: HttpClient) { }

    protected get(id: string) {
      return this.client.get<IBasket>(this.url + `?id=${id}`)
        .pipe(
          map((response: IBasket) => {
              this.basketSource.next(response);
              console.log(this.getValue());
          })
        );
    }

    protected update(item: Basket) {
      return this.client.post<IBasket>(this.url, item)
        .subscribe( (response: IBasket) => {
          this.basketSource.next(response);
          console.log('Basket is updated');
          console.log(this.getValue());
        }, error => {
            console.log(error);
        });
    }
    
    protected delete(id: string) {
      return this.client.delete<boolean>(this.url + `?id=${id}`)
        .subscribe( _ => {
          this.basketSource.next(null);
        });
    }

    protected getValue(): IBasket {
      return this.basketSource.value;
    }
}
