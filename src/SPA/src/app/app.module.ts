import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import { NavComponent } from './nav/nav.component';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { ProductEditComponent } from './product/product-edit/product-edit.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductService } from './_services/product.service';
import { ProductEditResolver } from './_resolvers/product-edit.resolver';
import { ProductDetailResolver } from './_resolvers/product-detail.resolver';
import { ProductListsResolver } from './_resolvers/product-lists.resolver';
import { ProductCardComponent } from './product/product-card/product-card.component';
import { PaginationModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';


@NgModule({
   declarations: [
      AppComponent,
      ProductComponent,
      NavComponent,
      ProductListComponent,
      ProductDetailComponent,
      ProductEditComponent,
      ProductCardComponent,
      HomeComponent,
   ],
   imports: [
      FormsModule,
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      HttpClientModule,
      PaginationModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      ProductService,
      ProductEditResolver,
      ProductDetailResolver,
      ProductListsResolver,
      PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
