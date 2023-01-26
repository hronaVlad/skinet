import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Data, NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject, filter } from 'rxjs';
import { Breadcrumb, BreadcrumbMeta } from '../shared/models/breadcrumb';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {

   // Subject emitting the breadcrumb hierarchy 
   private readonly _breadcrumbs$ = new BehaviorSubject<Breadcrumb[]>([]); 
 
   // Observable exposing the breadcrumb hierarchy 
   readonly breadcrumbs$ = this._breadcrumbs$.asObservable(); 
  
   constructor(private router: Router) { 
     this.router.events.pipe( 
       // Filter the NavigationEnd events as the breadcrumb is updated only when the route reaches its end 
       filter((event) => event instanceof NavigationEnd) 
     ).subscribe(event => { 
       // Construct the breadcrumb hierarchy 
       const root = this.router.routerState.snapshot.root; 
       const breadcrumbs: Breadcrumb[] = []; 
       this.addBreadcrumb(root, [], breadcrumbs); 
  
       // Emit the new hierarchy 
       this._breadcrumbs$.next(breadcrumbs); 
     }); 
   } 
  
   private addBreadcrumb(route: ActivatedRouteSnapshot  | null, parentUrl: string[], breadcrumbs: Breadcrumb[]) { 
     if (route) { 
       // Construct the route URL 
       const routeUrl = parentUrl.concat(route.url.map(url => url.path)); 
  
       // Add an element for the current route part 
       const dataAlias = route.data?.['alias'];
       const dataBreadcrumb = route.data?.['breadcrumb'];

       if (dataBreadcrumb) { 
        let breadcrumb: Breadcrumb;

        if (dataAlias) {
          breadcrumb = { 
            label: undefined,
            url: '/' + routeUrl.join('/'),
            meta: {
              alias: dataAlias
            }
          }; 

          const isFunction = typeof dataBreadcrumb === 'function';
          if (isFunction) {
              if (breadcrumb) {
                if (breadcrumb.meta) {
                    breadcrumb.meta.id = route.params?.['id'];
                    breadcrumb.meta.function = dataBreadcrumb;
                }
              }
          }
        }
        else {
          breadcrumb = { 
            label: this.getLabel(route), 
            url: '/' + routeUrl.join('/') 
          }; 
        }
         
         breadcrumbs.push(breadcrumb); 
       } 
  
       // Add another element for the next route part 
       this.addBreadcrumb(route.firstChild, routeUrl, breadcrumbs); 
     } 
   } 
  
   private getLabel(route: ActivatedRouteSnapshot) { 
      const data: Data = route.data;
      const o = data?.['breadcrumb'];
     return o;
   }

   public updateBreadcrumbs(id: number, object: any) {
      const values = this._breadcrumbs$.value;

      values.forEach(v => {
        if (v.meta && 
            v.meta.alias === 'ProductDetails' && 
            v.meta.id == id && 
            v.meta.function &&
            !v.label) {
              v.label = v.meta.function(object);
        }
      });

      this._breadcrumbs$.next(values);
  }
}
