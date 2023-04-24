import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityComponent } from './city/city.component';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
//import { NgxPopoverImageModule } from 'ngx-popover-image';
import { SkillComponent } from './skill/skill.component';
import { SubskillComponent } from './subskill/subskill.component';
import { AddProductsComponent } from './add-products/add-products.component';
import { AddSubProductsComponent } from './add-sub-products/add-sub-products.component';
import { FAQsComponent } from './faqs/faqs.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { AgreementsComponent } from './agreements/agreements.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { PromotionsComponent } from './promotions/promotions.component';
import { PackagesComponent } from './packages/packages.component';
import { CommissionComponent } from './commission/commission.component';
import { PromotionOnPackagesComponent } from './promotion-on-packages/promotion-on-packages.component';
import { PromotionTypeComponent } from './promotion-type/promotion-type.component';
import { CouponTypeComponent } from './coupon-type/coupon-type.component';
import { CouponsComponent } from './coupons/coupons.component';
import { OrdersComponent } from './orders/orders.component';
import { TownComponent } from './town/town.component';
import { PackagetypeComponent } from './packagetype/packagetype.component';
//import { NgxInputTagModule } from '@ngx-lite/input-tag';
//import { SelectDropDownModule } from 'ngx-select-dropdown'
import { TagInputModule } from 'ngx-chips';
import { ReferralsComponent } from './referrals/referrals.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { ActivePromotionComponent } from './active-promotion/active-promotion.component';
import { AccountComponent } from './account/account.component';
import { AccountTypeComponent } from './account-type/account-type.component';
import { SubAccountComponent } from './sub-account/sub-account.component';
import { VoucherCategoryComponent } from './vouchercategory/vouchercategory.component';
import { VoucherTypeComponent } from './vouchertype/vouchertype.component';
import { VoucherBookComponent } from './voucherbook/voucherbook.component';
import { VoucherBookAllocationComponent } from './voucherbookallocation/voucherbookallocation.component';
import { VoucherbookpagesComponent } from './voucherbookpages/voucherbookpages.component';
import { VoucherbookleavesComponent } from './voucherbookleaves/voucherbookleaves.component';
import { CategoryCommissionSetupComponent } from './category-commission-setup/category-commission-setup.component';
import { TradesmanCommissionOverrideComponent } from './tradesman-commission-override/tradesman-commission-override.component';
import { TooltipFormComponent } from './tooltip-form/tooltip-form.component';
import { TooltipFormDetailsComponent } from './tooltip-form-details/tooltip-form-details.component';
import { MenuItemComponent } from './menu-item/menu-item.component';
import { SubMenuItemComponent } from './sub-menu-item/sub-menu-item.component';
import { ApplicationSettingComponent } from './application-setting/application-setting.component';
import { ApplicationSettingDetailsComponent } from './application-setting-details/application-setting-details.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { LinkedsalesmanComponent } from './linkedsalesman/linkedsalesman.component';
import { GeneralLedgerScreenComponent } from './general-ledger-screen/general-ledger-screen.component';
import { DetailedGeneralLedgerComponent } from './detailed-general-ledger/detailed-general-ledger.component';
import { CancellationReasonsComponent } from './cancellation-reasons/cancellation-reasons.component';
import { CountryComponent } from './country/country.component';
import { StateComponent } from './state/state.component';
import { BankComponent } from './bank/bank.component';
import { AreaComponent } from './area/area.component';
import { LocationComponent } from './location/location.component';
import { SitePagesComponent } from './site-pages/site-pages.component';
import { TestimonialComponent } from './testimonial/testimonial.component';



const routes: Routes = [
  { path: 'addproducts', component: AddProductsComponent },
  { path: 'addsubproducts', component: AddSubProductsComponent },
  { path: 'city', component: CityComponent },
  { path: 'skill', component: SkillComponent },
  { path: 'subskill', component: SubskillComponent },
  { path: 'faq', component: FAQsComponent },
  { path: 'agreements', component: AgreementsComponent },
  { path: 'promotions', component: PromotionsComponent },
  { path: 'commission', component: CommissionComponent },
  { path: 'packages', component: PackagesComponent },
  { path: 'promoonpackages', component: PromotionOnPackagesComponent },
  { path: 'PromotionType', component: PromotionTypeComponent },
  { path: 'CouponType', component: CouponTypeComponent },
  { path: 'Coupons', component: CouponsComponent },
  { path: 'Orders', component: OrdersComponent },
  { path: 'town', component: TownComponent },
  { path: 'packagetype', component: PackagetypeComponent },
  { path: 'referrals', component: ReferralsComponent },
  { path: 'vouchercategory', component: VoucherCategoryComponent },
  { path: 'vouchertype', component: VoucherTypeComponent },
  { path: 'voucherbook', component: VoucherBookComponent },
  { path: 'voucherbookallocation', component: VoucherBookAllocationComponent },
  { path: 'voucherbookpages', component: VoucherbookpagesComponent },
  { path: 'voucherbookleaves', component: VoucherbookleavesComponent },
  { path: 'activepromotion', component: ActivePromotionComponent },
  { path: 'account', component: AccountComponent },
  { path: 'accounttype', component: AccountTypeComponent },
  { path: 'subaccount', component: SubAccountComponent },
  { path: 'categorycommissionsetup', component: CategoryCommissionSetupComponent },
  { path: 'tradesmancommissionoverride', component: TradesmanCommissionOverrideComponent },
  { path: 'tooltip', component: TooltipFormComponent },
  { path: 'tooltipdetails', component: TooltipFormDetailsComponent },
  { path: 'menuitem', component: MenuItemComponent },
  { path: 'submenuitem', component: SubMenuItemComponent },
  { path: 'applicationsetting', component: ApplicationSettingComponent },
  { path: 'applicationsettingdetails', component: ApplicationSettingDetailsComponent },
  { path: 'campaignslist', component: CampaignsComponent },
  { path: 'linkedsalesman', component: LinkedsalesmanComponent },
  { path: 'generalLedger', component: GeneralLedgerScreenComponent },
  { path: 'detailedgeneralLedger', component: DetailedGeneralLedgerComponent },
  {path:'cancellationreasons',component:CancellationReasonsComponent},
  { path: 'country', component: CountryComponent },
  { path: 'state', component: StateComponent },
  { path: 'area', component: AreaComponent },
  { path: 'location', component: LocationComponent },
  { path: 'bank', component: BankComponent },
  { path: 'sitepages', component: SitePagesComponent },
  { path: 'testimonials', component: TestimonialComponent }

    
];

@NgModule({
  declarations: [
    CityComponent,
    AddProductsComponent,
    AddSubProductsComponent,
    SkillComponent,
    SubskillComponent,
    FAQsComponent,
    AgreementsComponent,
    PromotionsComponent,
    PackagesComponent,
    CommissionComponent,
    PromotionOnPackagesComponent,
    PromotionTypeComponent,
    CouponTypeComponent,
    CouponsComponent,
    OrdersComponent,
    TownComponent,
    PackagetypeComponent,
    ReferralsComponent,
    VoucherTypeComponent,
    VoucherBookComponent,
    VoucherbookpagesComponent,
    VoucherbookleavesComponent,
    VoucherBookAllocationComponent,
    VoucherCategoryComponent,
    ActivePromotionComponent,
    AccountComponent,
    AccountTypeComponent,
    SubAccountComponent,
    VoucherbookpagesComponent,
    VoucherbookleavesComponent,
    CategoryCommissionSetupComponent,
    TradesmanCommissionOverrideComponent,
    TooltipFormComponent,
    TooltipFormDetailsComponent,
    MenuItemComponent,
    SubMenuItemComponent,
    ApplicationSettingComponent,
    ApplicationSettingDetailsComponent,
    CampaignsComponent,
    LinkedsalesmanComponent,
    GeneralLedgerScreenComponent,
    DetailedGeneralLedgerComponent,
    CancellationReasonsComponent,
    CountryComponent,
    StateComponent,
    BankComponent,
    AreaComponent,
    LocationComponent,
    SitePagesComponent,
    TestimonialComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    //NgxInputTagModule.forRoot(),
    TagInputModule,
    FormsModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    AngularEditorModule,
    ImageCropperModule,
    AutocompleteLibModule,
    //SelectDropDownModule
  ],
  
  exports: [RouterModule],
})
export class SettingsModule { }
