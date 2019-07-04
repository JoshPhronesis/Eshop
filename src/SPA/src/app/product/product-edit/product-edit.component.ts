import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Product } from 'src/app/_models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;

  product: Product;
  isSaved: boolean;
  
  @HostListener('window:beforeunload', ['$event'])
  unloadNotificvation($event: any) {
    if(!this.isSaved){
       $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.product = data['product'];
    });
  }

  updateProduct() {
    this.productService.updateProduct(this.product.id, this.product).subscribe(next => {
      this.isSaved = true;
      this.router.navigate(['/products', this.product.id]);
    }, error => {
      console.log('an error occurred');
    });
  }
}
