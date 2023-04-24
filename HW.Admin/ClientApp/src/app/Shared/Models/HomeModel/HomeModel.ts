export class AdminDashboardVm {
  public allUsersCount: number;
  public createdJobsCount: number;
  public registeredCustomersCount: number;
  public liveAdsCount: number;
  public supplierCounts: number;
  public tradesmanCount: number;
  public totalCountLhr: number;
  public totalCountIsb: number;
  public totalCountKhi: number;
  public totalCountGujrat: number;
  public totalCountGujWala: number;
  public city: string;
  public ckillName: string
  public tradesmanCountBySkill: number
  public customerCount: number;
  public customerSkillName: string;
  public customerCity: string;
  public supplieCount: number
  public supplierCategory: string
  public supplierCity: string;
  public authorizeJobsCount: number;

}
export class LoginVM{
  emailOrPhoneNumber: string;
  password: string;
}
export class ForgotPassword {
  email: string;
}
export class ResetPasswordVM {
  passwords: string;
  confirmPassword: string;
  userId: string;
}
export class ReportDateVm {
  startDate: Date;
  endDate: Date;
}

export class CategorryCount
{
  count: string;
  category: string;
}
export class ResponseVm {
  status: any;
  message: string;
  resultData: any;
}
