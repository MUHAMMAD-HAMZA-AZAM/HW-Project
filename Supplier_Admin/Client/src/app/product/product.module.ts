import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewProductComponent } from './add-new-product/add-new-product.component';
import { RouterModule, Routes } from '@angular/router';
import { TagInputModule } from 'ngx-chips';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { NgxImageCompressService } from 'ngx-image-compress';
import { AddBulkProductComponent } from './add-bulk-product/add-bulk-product.component';
const routes: Routes = [
  {path: 'add-new-product',component: AddNewProductComponent,},
  { path: 'product-list', component: ProductListComponent, },
  { path: 'product-detail/:id', component: ProductDetailsComponent, },
  { path: 'edit-product/:id', component: EditProductComponent, },
  { path: 'uploadfile', component: UploadFileComponent, },
  { path: 'AddBulkProduct', component: AddBulkProductComponent }
];

@NgModule({
  declarations: [
    AddNewProductComponent,
    ProductListComponent,
    ProductDetailsComponent,
    EditProductComponent,
    UploadFileComponent,
    AddBulkProductComponent
  ],
  imports: [
    TagInputModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    CommonModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  providers: [NgxImageCompressService]
})
export class ProductModule { }
