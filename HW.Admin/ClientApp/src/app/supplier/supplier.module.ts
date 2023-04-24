import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCategoryAttributesComponent } from './product-category-attributes/product-category-attributes.component';
import { RouterModule, Routes } from '@angular/router';
import { ProductCategoryGroupComponent } from './product-category-group/product-category-group.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductColorsComponent } from './product-colors/product-colors.component';
import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
import { SupplierSliderComponent } from './supplier-slider/supplier-slider.component';
import { SuppliersOrdersListComponent } from './suppliers-orders-list/suppliers-orders-list.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ShippingChargesComponent } from './shipping-charges/shipping-charges.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SupplierleadgerhistoryComponent } from './supplierleadgerhistory/supplierleadgerhistory.component';
import { SupplierCommisionComponent } from './supplier-commision/supplier-commision.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';




const routes: Routes = [
  //{path: 'add-product-attributes', component: ProductAttributesComponent},
  //{ path: 'app-product-category-group', component: ProductCategoryGroupComponent },
  { path: 'app-product-colors', component: ProductColorsComponent },
  { path: 'add-product-attributes', component: ProductAttributesComponent},
  { path: 'add-product-category-attributes', component: ProductCategoryAttributesComponent},
  { path: 'app-product-category-group', component: ProductCategoryGroupComponent },
  { path: 'supplier-slider', component: SupplierSliderComponent },
  { path: 'app-suppliers-orders-list', component: SuppliersOrdersListComponent },
  { path: 'app-supplierleadgerhistory', component: SupplierleadgerhistoryComponent },
  { path: 'app-shipping-charges', component: ShippingChargesComponent },
  { path: 'app-supplier-commission', component: SupplierCommisionComponent }
]
@NgModule({
  declarations: [
    ProductCategoryAttributesComponent,
    ProductCategoryGroupComponent,
    ProductColorsComponent,
    ProductAttributesComponent,
    SupplierSliderComponent,
    SuppliersOrdersListComponent,
    ShippingChargesComponent,
    SupplierleadgerhistoryComponent,
    SupplierCommisionComponent,
],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    NgbModule,
    AutocompleteLibModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
  providers: [
  ]
})
export class SupplierModule { }
