import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TradesmanReportComponent } from './tradesman-report/tradesman-report.component';
import { CustomerReportComponent } from './customer-report/customer-report.component';
import { SupplierReportComponent } from './supplier-report/supplier-report.component';
import { DynamicScriptLoaderService } from '../Shared/CommonServices/dynamicScriptLoaderService';
import { AnnualReportTradesmanComponent } from './annual-report-tradesman/annual-report-tradesman.component';
import { AnnualReportSupplierComponent } from './annual-report-supplier/annual-report-supplier.component';
import { AnnualReportCustomerComponent } from './annual-report-customer/annual-report-customer.component';
import { ManualReportsCustomerComponent } from './manual-reports-customer/manual-reports-customer.component';
import { ManualReportsTradesmanComponent } from './manual-reports-tradesman/manual-reports-tradesman.component';
import { ManualReportsSupplierComponent } from './manual-reports-supplier/manual-reports-supplier.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { InactiveReportUsersComponent } from './inactive-report-users/inactive-report-users.component';
import { SecurityroleComponent } from './securityrole/securityrole.component';
import { CreateadminuserComponent } from './createadminuser/createadminuser.component';
import { PostedJobComponent } from './posted-job/posted-job.component';
import { ActiveBidsDailyComponent } from './active-bids-daily/active-bids-daily.component';
import { PostedAdsDailyComponent } from './posted-ads-daily/posted-ads-daily.component';
import { ManualReportsPostedJobsComponent } from './manual-reports-posted-jobs/manual-reports-posted-jobs.component';
import { ManualReportsBidsComponent } from './manual-reports-bids/manual-reports-bids.component';
import { ManualReportsPostedAdsComponent } from './manual-reports-posted-ads/manual-reports-posted-ads.component';
import { SecurityRolesListComponent } from './security-roles-list/security-roles-list.component';
import { ExcelFileService } from '../Shared/CommonServices/excel-file.service';
import { ClickOutsideModule } from 'ng-click-outside';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

//import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { NgxImageCompressService } from 'ngx-image-compress';
import { LeadgersComponent } from './leadgers/leadgers.component';
import { UserReceiptsComponent } from './user-receipts/user-receipts.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { TransectionReportComponent } from './transection-report/transection-report.component';
import { AccountPayableComponent } from './account-payable/account-payable.component';
import { AccountReceivableComponent } from './account-receivable/account-receivable.component';
import { GeneralAccountComponent } from './general-account/general-account.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';


const routes: Routes = [
  { path: 'app-tradesman-report,1', component: TradesmanReportComponent },
  { path: 'app-tradesman-report,2', component: TradesmanReportComponent },
  { path: 'app-tradesman-report,3', component: TradesmanReportComponent },
  { path: 'app-tradesman-report,4', component: TradesmanReportComponent },
  { path: 'app-customer-report,1', component: CustomerReportComponent },
  { path: 'app-customer-report,2', component: CustomerReportComponent },
  { path: 'app-customer-report,3', component: CustomerReportComponent },
  { path: 'app-customer-report,4', component: CustomerReportComponent },
  { path: 'app-supplier-report,1', component: SupplierReportComponent },
  { path: 'app-supplier-report,2', component: SupplierReportComponent },
  { path: 'app-supplier-report,3', component: SupplierReportComponent },
  { path: 'app-supplier-report,4', component: SupplierReportComponent },
  { path: 'app-annual-report-tradesman', component: AnnualReportTradesmanComponent },
  { path: 'app-manual-reports-tradesman', component: ManualReportsTradesmanComponent },
  { path: 'app-annual-report-supplier', component: AnnualReportSupplierComponent },
  { path: 'app-manual-reports-supplier', component: ManualReportsSupplierComponent },
  { path: 'app-annual-report-customer', component: AnnualReportCustomerComponent },
  { path: 'app-manual-reports-customer', component: ManualReportsCustomerComponent },
  { path: 'app-inactive-report-users', component: InactiveReportUsersComponent },
 // { path: 'app-securityrole', component: SecurityroleComponent },
  { path: 'app-securityrole/:id', component: SecurityroleComponent },
  { path: 'app-createadminuser', component: CreateadminuserComponent },
  { path: 'app-PostedJob-report,1', component: PostedJobComponent },
  { path: 'app-PostedJob-report,2', component: PostedJobComponent },
  { path: 'app-PostedJob-report,3', component: PostedJobComponent },
  { path: 'app-PostedJob-report,4', component: PostedJobComponent },
  { path: 'app-ActiveBids-report,1', component: ActiveBidsDailyComponent },
  { path: 'app-ActiveBids-report,2', component: ActiveBidsDailyComponent },
  { path: 'app-ActiveBids-report,3', component: ActiveBidsDailyComponent },
  { path: 'app-ActiveBids-report,4', component: ActiveBidsDailyComponent },
  { path: 'app-PostedAdsDaily-report,1', component: PostedAdsDailyComponent },
  { path: 'app-PostedAdsDaily-report,2', component: PostedAdsDailyComponent },
  { path: 'app-PostedAdsDaily-report,3', component: PostedAdsDailyComponent },
  { path: 'app-PostedAdsDaily-report,4', component: PostedAdsDailyComponent },
  { path: 'app-ManualPostedJob-report', component: ManualReportsPostedJobsComponent },
  { path: 'app-ManualBids-report', component: ManualReportsBidsComponent },
  { path: 'app-ManualPostedAds-report', component: ManualReportsPostedAdsComponent },
  { path: 'app-securityrollist', component: SecurityRolesListComponent },
  { path: 'app-leadgers', component: LeadgersComponent },
  { path: 'app-user-receipts', component: UserReceiptsComponent },
  { path: 'app-transection-report', component: TransectionReportComponent },
  { path: 'account-payable', component: AccountPayableComponent },
  { path: 'account-receivable', component: AccountReceivableComponent },
  { path: 'general-account', component: GeneralAccountComponent }

];
@NgModule({
  declarations: [
    TradesmanReportComponent,
    CustomerReportComponent,
    SupplierReportComponent,
    AnnualReportTradesmanComponent,
    AnnualReportSupplierComponent,
    AnnualReportCustomerComponent,
    ManualReportsCustomerComponent,
    ManualReportsTradesmanComponent,
    ManualReportsSupplierComponent,
    InactiveReportUsersComponent,
    SecurityroleComponent,
    CreateadminuserComponent,
    PostedJobComponent,
    ActiveBidsDailyComponent,
    PostedAdsDailyComponent,
    ManualReportsPostedJobsComponent,
    ManualReportsBidsComponent,
    ManualReportsPostedAdsComponent,
    SecurityRolesListComponent,
    LeadgersComponent,
    UserReceiptsComponent,
    TransectionReportComponent,
    AccountPayableComponent,
    AccountReceivableComponent,
    GeneralAccountComponent,
   
   
  ],
  
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NgMultiSelectDropDownModule.forRoot(),
    NgbModule,
    ClickOutsideModule,
    NgxSpinnerModule,
    AutocompleteLibModule
  ],
  providers: [
    DynamicScriptLoaderService, ExcelFileService, [NgxImageCompressService]
  ],
  exports: [RouterModule],  
})
export class ReportsModule { }
