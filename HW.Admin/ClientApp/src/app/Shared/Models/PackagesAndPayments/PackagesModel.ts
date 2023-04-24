import { Data } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { ListItem } from 'ng-multiselect-dropdown/multiselect.model';

export class PackagesClass {
  public packageId: number;
  public userRoleId: number;
  public PackageCode: string;
  public PackageName: string;
  public TradePrice: number;
  public salePrice: number;
  public validityDays: number;
  public totalApplicableJobs: number;
  public totalCategories: number;
  public entityStatus: string;
  public isActive: boolean;
  public CreatedDate: Date;
  public CreatedBy: string;
  public UpdatedDate: Date;
  public UpdatedBy: number;
  public Status: string;
  public packageTypeId: number;

}

export class PackagesFiltrClass {
  public PackageId: number;
  public UserRoleId: number;
  public Entity: string;
  public PackageCode: string;
  public PackageName: string;
  public OrderByColumn: string;
  public PageNumber: number;
  public PageSize: number;
}

export class PromotionTypeClass {
  public PromotionTypeId: number;
  public PromotionTypeCode: number;
  public PromotionTypeName: string;
  public IsActive: boolean;
  public CreatedDate: Date;
  public CreatedBy: string;
  public UpdatedDate: Date;
  public UpdatedBy: number;
  public Status: string;
  public entityStatus: string;
  public promotionOn: string;
}
export class PackageTypeClass {
  public PackageTypeId: number;
  public PackageTypeCode: number;
  public PackageTypeName: string;
  public IsActive: boolean;
  public CreatedOn: Date;
  public CreatedBy: string;
  public ModifiedOn: Date;
  public ModifiedBy: number;
  public entityStatus: string;
  public userRoleId: number;
  public sta: string;
 
}

export class CouponTypeClass {
  public CouponTypeId: number;
  public CouponTypeCode: number;
  public CouponTypeName: string;
  public IsActive: boolean;
  public CreatedDate: Date;
  public CreatedBy: string;
  public UpdatedDate: Date;
  public UpdatedBy: number;
  public Status: string;
  public entityStatus: string;
}
export class SalesmanVM {
  public salesmanId: number;
  public name: number;
  public mobileNumber: string;
  public address: string;
  public isActive: boolean;
  public createdOn: Date;
  public createdBy: string;
  public modifiedOn: Date;
  public mobifiedBy: number;
  public Status: string;
  public entityStatus: string;
}

export class CouponClass {
  public CouponId: number;
  public CouponTypeId: number;
  public CouponCode: string;
  public CouponeName: string;
  public DiscountAmount: number;
  public DiscountDays: number;
  public DiscountCategories: number;
  public DiscountJobsApplied: number;
  public CouponIdGuid: number;
  public entityStatus: string;
  public IsActive: boolean;
  public CreatedDate: Date;
  public CreatedBy: string;
  public UpdatedDate: Date;
  public UpdatedBy: number;
  public Status: string;
  public OrderByColumn: string;
  public PageNumber: number;
  public PageSize: number;
  public noOfPages: number;
  public noOfRecoards: number;
}
export class CouponTypeDDClass {
  public CouponId: number;
  public CouponeName: string;
}
export class PromotionDDClass {
  public PromotionId: number;
  public PromotionName: string;
}
export class PackageDDClass {
  public PackageId: number;
  public PackageName: string;
}
export class PromotionOnPackagesDDClass {
  public PromotionOnPackagesId: number;
  public PromotionOnPackagesName: string;
}
export class SetProduct {
  public id: number;
  public value: string;
}
export class PackagesAndPromotion {
  public orderId: number;
  public promotionOnPackagesId: number;
  public packageId: number;
  public PackageName: string;
  public promotionId: number;
  public promotionName: string;
  public userRoleId: number;
  public UserName: string;
  public aspnetUserId: string;
  public userId: string;
  public originalSalePrice: number;
  public discountPercentPrice: number;
  public priceAfterDiscount: number;
  public validityDays: number;
  public discountDays: number;
  public discountedValidityDays: number;
  public totalApplicableJobs: number;
  public discountJobsApplied: number;
  public discountedTotalApplicableJobs: number;
  public totalCategories: number;
  public discountCategories: number;
  public discountedTotalCategories: number;
  public orderTotal: number;
  public entityStatus: string;
  public isActive: boolean
  public CreatedDate: Date;
  public CreatedBy: string;
  public UpdatedDate: Date;
  public UpdatedBy: string;
  public Status: string;
  public OrderByColumn: string;
  public selectedSkills: any;
  public skillsIds: string;
  public PageNumber: number;
  public PageSize: number;
  public noOfPages: number;
  public noOfRecoards: number;
}
export class PromotionClass {
  public promotionId: number;
  public promotionIdTypeId: number
  public promotionCode: string
  public promotionName: string
  public promoStartDate: Date
  public promotionEndDate: Date;
  public discountPercentPrice: number;
  public discountDays: number;
  public discountCategories: number;
  public discountJobsApplied: number;
  public EntityStatus: string
  public isActive: boolean
  public CreatedDate: Date
  public CreatedBy: string
  public UpdatedDate: Date
  public UpdatedBy: string
  public promotionTypeName: number
}

export class UserDetailData {
  public id: string
  public userName: string
  public roleId: number
  public firsrName: string
  public lastName: string
  public userTypeId: string

}
