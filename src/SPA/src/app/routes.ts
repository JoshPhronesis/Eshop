import {Routes} from '@angular/router';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductEditComponent } from './product/product-edit/product-edit.component';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { ProductListsResolver } from './_resolvers/product-lists.resolver';
import { ProductDetailResolver } from './_resolvers/product-detail.resolver';
import { ProductEditResolver } from './_resolvers/product-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';


export const appRoutes: Routes = [
    {path: 'home', redirectTo: 'products', pathMatch: 'full'},
    {
        path: 'products',
        runGuardsAndResolvers: 'always',
        children:[
            {path: '', component: ProductListComponent, resolve: {products: ProductListsResolver}},
            {path: ':id', component: ProductDetailComponent, resolve: {product: ProductDetailResolver}},
            {path: 'edit/:id', component: ProductEditComponent, resolve: {product: ProductEditResolver}},
        ]
    },
    {path: '**', redirectTo: '/products', pathMatch: 'full'},
];
