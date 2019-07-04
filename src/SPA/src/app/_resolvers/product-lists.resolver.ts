import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';

@Injectable()
export class ProductListsResolver implements Resolve<Product[]> {
    pageNumber = 1;
    pageSize = 12;
    constructor(private productService: ProductService , private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Product[]> {
        return this.productService.getProducts(this.pageNumber, this.pageSize).pipe(
        catchError(() => {
            this.router.navigate(['/home']);
            return of(null);
        })
    )}
}
