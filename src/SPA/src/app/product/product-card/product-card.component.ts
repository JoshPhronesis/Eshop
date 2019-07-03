import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  baseUrl = environment.apiUrl;

  @Input() product: Product;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products/' + id ).subscribe(() => {
      // this.products.splice(this.products.findIndex(x => x.id === id), 1);
    },error =>{
      console.log(error);
    });
  }
}
