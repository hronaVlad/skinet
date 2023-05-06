import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  url = environment.apiUrl + 'account/';
  localstorageTokenItem = 'token'

  private userSource = new ReplaySubject<IUser>(1);
  user$: Observable<IUser> = this.userSource.asObservable();

  constructor(private client: HttpClient, private router: Router) { }

  public login(data: any): Observable<void> {
    return this.client.post<IUser>(this.url + 'login', data).pipe(
      map( (user: IUser) => {
        this.setTokenInLocalStorage(user.token);
        this.userSource.next(user);
      }));
  }

  public register(data:any) {
    return this.client.post<IUser>(this.url + 'register', data).pipe(
      map( (user: IUser) => {
        this.setTokenInLocalStorage(user.token);
        this.userSource.next(user);
      }));
  }

  public getAddress(): Observable<IAddress> {
    return this.client.get<IAddress>(this.url + 'address');
  }

  public updateAddress(address: IAddress): void {
    this.client.put<IAddress>(this.url + 'address', address)
      .subscribe(_ => {

      }, error => {
        console.log(error);
    });
  }


  public checkEmailExists(email: string): Observable<boolean> {
    return this.client.get<boolean>( this.url + `emailexists?email=${email}`);
  }

  public logout(): void {
    localStorage.removeItem(this.localstorageTokenItem);
    this.userSource.next(null);

    this.router.navigate(['/']);
  }

  public loadUser() {
    const token = this.getTokenFromLocalStorage();

      if(!token){
        this.userSource.next(null);
        return of(null);
      }

      let headers = new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);

      return this.client.get<IUser>(this.url, { headers }).subscribe (user => {
        this.setTokenInLocalStorage(user.token);
        this.userSource.next(user);
      }, error => {
        console.log(error);
      });
    }

  private setTokenInLocalStorage(token: string): void {
    localStorage.setItem(this.localstorageTokenItem, token);
  }

  private getTokenFromLocalStorage(): string {
    return localStorage.getItem(this.localstorageTokenItem);
  }

}
