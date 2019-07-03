import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';

@Injectable()
export class ProductEditResolver implements Resolve<Product> {
    constructor(private productService: ProductService , private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Product> {
        return this.productService.getProduct(route.params['id']).pipe(
            catchError(error => {
                this.router.navigate(['/products']);
                return of(null);
        })
    )}
}
