import { NgModule } from '@angular/core';
import { PrimaryUserComponent } from './primary-user/primary-user.component';
import { TradesmanComponent } from './tradesman/tradesman.component';
import { SupplierComponent } from './supplier/supplier.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrganisationComponent } from './organisation/organisation.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { DynamicScriptLoaderService } from '../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../Shared/CommonServices/excel-file.service';
import { OrderModule } from 'ngx-order-pipe';
import { TradesManByCategoryComponent } from './trades-man-by-category/trades-man-by-category.component';
import { OrgnizationByCategoryComponent } from './orgnization-by-category/orgnization-by-category.component';
import { SupplierbycategoryComponent } from './supplierbycategory/supplierbycategory.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { FeaturedSuppliersComponent } from './featured-suppliers/featured-suppliers.component';
import { DeleteUserInfoComponent } from './delete-user-info/delete-user-info.component';
import { NgxImageCompressService } from 'ngx-image-compress';
import { SalesmanComponent } from './salesman/salesman.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { StatisticalAnalysisComponent } from './statistical-analysis/statistical-analysis.component';
import { ApproveSupplierProductComponent } from './approve-supplier-product/approve-supplier-product.component';
import { AppSupplierHWComponent } from './app-supplier-hw/app-supplier-hw.component';
import { AppSupplierLocalComponent } from './app-supplier-local/app-supplier-local.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SupplierProductListComponent } from './supplier-product-list/supplier-product-list.component';

//import { AutocompleteLibModule } from 'angular-ng-autocomplete';





const routes: Routes= [
  { path: 'app-primary-user', component: PrimaryUserComponent },
  { path: 'app-supplier', component: SupplierComponent },
  { path: 'featured-suppliers', component: FeaturedSuppliersComponent },
  { path: 'app-supplierbycategory', component: SupplierbycategoryComponent },
  { path: 'app-tradesman', component: TradesmanComponent },
  { path: 'app-organisation', component: OrganisationComponent },
  { path: 'app-trades-man-by-category', component: TradesManByCategoryComponent },
  { path: 'app-organisation-by-category', component: OrgnizationByCategoryComponent },
  { path: 'app-user-profile', component: UserProfileComponent },
  { path: 'delete-user-info', component: DeleteUserInfoComponent },
  { path: 'salesman', component: SalesmanComponent },
  { path: 'statistical-analysis/:id', component: StatisticalAnalysisComponent },
  { path: 'app-approve-supplier-product', component: ApproveSupplierProductComponent },
  { path: 'app-supplier-hw', component: AppSupplierHWComponent  },
  { path: 'app-supplier-local', component: AppSupplierLocalComponent  },
  { path: 'supplier-product-list', component: SupplierProductListComponent }
];


@NgModule({
  declarations: [

    PrimaryUserComponent,
    TradesmanComponent,
    SupplierComponent,
    OrganisationComponent,
    UserProfileComponent,
    TradesManByCategoryComponent,
    OrgnizationByCategoryComponent,
    SupplierbycategoryComponent,
    FeaturedSuppliersComponent,
    DeleteUserInfoComponent,
    SalesmanComponent,
    StatisticalAnalysisComponent,
    ApproveSupplierProductComponent,
    AppSupplierHWComponent,
    AppSupplierLocalComponent,
    SupplierProductListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgMultiSelectDropDownModule,
    AutocompleteLibModule,
    RouterModule.forChild(routes),
    NgbModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    PopoverModule.forRoot(),
    /*OrderModule*/
    ImageCropperModule,
    NgxSpinnerModule,
  ],
  exports: [RouterModule],
  providers: [
    DynamicScriptLoaderService, ExcelFileService, NgxImageCompressService
  ],
})
export class UsermoduleModule { }
