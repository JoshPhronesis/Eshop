import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { Product } from 'src/app/_models/product';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[];
  pagination: Pagination;
  baseUrl = environment.apiUrl;
  userParams: any = {};
  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.products = data['products'].result;
      this.pagination = data['products'].pagination;
      this.userParams.orderBy = 'leastExpensive';
      this.userParams.searchTerm = '';
      this.resetFilters();
    });
  }

  resetFilters() {
    this.userParams.minPrice = 1;
    this.userParams.maxPrice =  100000;
    this.userParams.searchTerm = '';
    this.loadProducts(this.userParams.orderBy);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadProducts(this.userParams.orderBy);
  }

  loadProducts(orderBy: string) {
    this.userParams.orderBy = orderBy;
    this.productService.getProducts(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe((res: PaginatedResult<Product[]>) => {
      this.products = res.result;
      this.pagination = res.pagination;
      console.log(this.products)
    }, error => {
      console.log(error);
    });
  }

}
