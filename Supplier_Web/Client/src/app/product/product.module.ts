import { NgModule,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductsByCategoryComponent } from './products-by-category/products-by-category.component';
import { ProductsByUserComponent } from './products-by-user/products-by-user.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductsByNameComponent } from './products-by-name/products-by-name.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { OrdertrackingComponent } from './ordertracking/ordertracking.component';
import { CustomerFeedbackComponent } from './customer-feedback/customer-feedback.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { ProductByTagComponent } from './product-by-tag/product-by-tag.component';
import { NgxImageZoomModule } from 'ngx-image-zoom';


const routes: Routes = [
  //{path: 'product-detail/:id/:varientId',component: ProductDetailComponent},
  {path: 'product-detail',component: ProductDetailComponent},
  { path: 'category', component: ProductsByCategoryComponent },
  { path: 'name', component: ProductsByNameComponent },
  { path: 'vendor/:supplierId', component: ProductsByUserComponent },
  { path: 'product-feedback', component: CustomerFeedbackComponent },
  {
    path: 'product',
    children: [
      { path: 'product-detail', component: ProductDetailComponent },
      { path: 'category', component: ProductsByCategoryComponent },
      { path: 'name', component: ProductsByNameComponent },
      { path: 'vendor/:shopUrl', component: ProductsByUserComponent },
        { path: 'tag', component: ProductByTagComponent },
    ]
  },
  
];
@NgModule({
  declarations: [
    ProductDetailComponent,
    ProductsByCategoryComponent,
    ProductsByUserComponent,
    ProductsByNameComponent,
    NotFoundComponent,
    OrdertrackingComponent,
    CustomerFeedbackComponent,
    ProductByTagComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgxSliderModule,
    NgbModule,
    NgxImageZoomModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  providers: [DatePipe]
})
export class ProductModule { }
