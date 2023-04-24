export class SpTradesmanStatsVM {

  public tradesmanCount: number;
  public organisationCount: number;
  public pageSize: number;
}
export class userdelete {

  public id: string;
  public userId: string;
  public firstName: string;
  public lastName: string;
  public userType: string;
  public email: string;
  public mobileNumber: string;
  public createdOn: Date;
  public sourceOfReg: string;
}

export class SpTradesmanListVM {

  public id: string;
  public tradesmanId: number;
  public isActuve: boolean;
  public tradesmanName: string;
  public noOfRecoards: number;
  public bidsCount: number;
  public recordNo: number;
  public skills: string;
  public smallSkills: string;
  public mobileNo: string;
  public tradesmanAddress: string;
  public cNIC: string;
  public completedJobsCount: number;
  public userId: string;
  public sourceOfReg: string;
  public salesmanName: string;
  public isTestUser: boolean;
  public emailConfirmed: boolean;
  public phoneNumberConfirmed: boolean;
  public createdOn: Date;
  public lastActive: Date;
  public isselectedforexport: boolean;

}

export class TradesmanReportVM {

  public tradesmanId: number;
  public firstName: string;
  public lastName: string;
  public mobileNumber: string;
  public addressLine: string;
  public CreatedOn: string;
  public cnic: number;
}
export class CustomerReportVM {

  public customerId: number;
  public firstName: string;
  public lastName: string;
  public mobileNumber: string;
  public streetAddress: string;
  public CreatedOn: string;
  public lastActive: string;
  public name: string;
  public cnic: number;
}
export class SupplierReportVM {

  public customerId: number;
  public firstName: string;
  public lastName: string;
  public mobileNumber: string;
  public businessAddress: string;
  public CreatedOn: string; 
  public name: string;
  public supplierType: string;
}
export class SupplieradsReport {

  public CreatedOn: string;
  public AdViewCount: string;
  public CityName: string;
  public Town: string;
  public Address: number;
  public ActiveTo: string;
  public ActiveFrom: string;
  public AdReference: string;
  public Price: string;
  public AdDescription: string;
  public AdTitle: number;
  public AddStatus: string;
  public SubCategoryName: string;
  public ProductName: string;
  public ProductCategoryId: string;
  public SupplierAdsId: string;
  public SupplierName: string;
  public SupplierId: string;
}

export interface ITownVM {
  townId: number;
  name: string;
}
export interface TradesmanPayments {
  amount: number;
  paymentMode: string;
  paymentMonth: string;
  paymentStatus: number;
  discountedAmount: number;
  serviceCharges: number;
  otherCharges: number;
  commission: number;
  netPayableToTradesman: number;
  payableAmount: number;
  paidViaWallet: number;
}

