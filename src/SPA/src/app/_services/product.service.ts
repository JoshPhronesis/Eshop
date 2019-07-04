import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../_models/pagination';
import { Product } from '../_models/product';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts(page?, itemsPerPage?): Observable<PaginatedResult<Product[]>>{
    const paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<Product[]>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginatedResult;
      })
    );
  }

  getProduct(id): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  updateProduct(id: number, product: Product) {
    return this.http.put(this.baseUrl + 'products/' + id, product);
  }

  deleteProduct(productId: number) {
    return this.http.delete(this.baseUrl + 'products/' + productId);
  }

  createProduct(product: Product) {
    return this.http.post(this.baseUrl + 'products', product);
  }
}
