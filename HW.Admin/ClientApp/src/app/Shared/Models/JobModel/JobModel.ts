import { Data } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { ListItem } from 'ng-multiselect-dropdown/multiselect.model';

export class GetAllaJobsCount {
  public jobsCreated: number;
  public jobsInProgress: number;
  public jobsCompleted: number;
  public jobsApprove: number;
  public deletedJobs: number;
  public declinedJobs: number;
  public pageSize: number;
  public activeJobs: number;
  public recoardNoFrom: number;
  public recordNo: number;
}

export class GetCustomerJobsCount {
  public pageSize: number;
  public notificationpageSize: number;
  public activeJobs: number;
  public recoardNoFrom: number;
  public recordNo: number;
}

export class GetactiveJobList {
  public customerId: number;
  public tradesmanId: number;
  public customerStatus: boolean;
  public isselectedforexport: boolean;
  public jobId: number;
  public jobDetailId: number;
  public statusId: string;
  public jobTitle: string;
  public noOfRecoards: number;
  public csJobStatusName: string;
  public recordNo: number;
  public City_Town: string;
  public city: string;
  public dateTime: Date;
  public workStartDate: Date;
  public createdOn: Date;
  public bidRecived: number;
  public mobileNumber: string;
  public customerName: string;
  public skillName: string;
  public subSkillName: string;
  public tradesmanName: string;
  public workBudget: string;
  public visitCharges: string;
  public serviceCharges: string;
  public otherCharges: string;
  public isAuthorize: boolean;
  public town: string;
  public streetAddress: string;
}
export class GetDeletedJobList {
  public customerId: number;
  public customerName: string;
  public mobileNumber: string;
  public city: string;
  public workBudget: string;
  public visitCharges: string;
  public serviceCharges: string;
  public otherCharges: string;
  public isAuthorize: boolean;
  public jobId: number;
  public jobTitle: string;
  public createdOn: Date;
  public noOfRecoards: number;
  public town: string;
  public isselectedforexport: boolean;
}
export class jobDetails {
  customerId: number;
  customerName: string;
  jobDetailId: number;
  jobQuotationId: number;
  customerEmail: string;
  jobTitle: string;
  jobDescription: string;
  jobImages: [];
  bidImage: [];
  latLong: string;
  budget: number;
  tradesmanBid: number;
  expectedJobStartDate: Date;
  expectedJobStartTime: Date;
  tradesmanMessage: string;
  jobLocation: string;
  jobAddress: string;
  jobAddressLine: string;
  customerAddress: string;
  userAddress: string;
  bidId: number;
  video: []
  audioMessage: string;

}

export class InprogressJobList {
  public customerId: number;
  public jobDetailId: number;  
  public jobId: number;
  public jobTitle: string;
  public city: string;
  public Town: string;
  public mobileNumber: string;
  public customerName: string;
  public Assignee: string;
  public noOfRecoards: number;
  public recordNo: number;
  public createdOn: Data;
}

export class JobDetails {
  
  public skill: string;
  public jobQuotationId: number;
  public skillName: string;
  public subSkillName: string;
  public jobAddress: string;
  public firstName: string;
  public lastName: string;
  public customerMobileNo: string;
  public customerEmail: string;
  public email: string;
  public singUpDate: Date;
  public customerAddress: Date;
  public subSkill: string;
  public workTitle: string;
  public workDescription: string;
  public workBudget: DecimalPipe;
  public agreedBudget: DecimalPipe;
  public workStartDate: Date;
  public city: string;
  public area: string;
  public streetAddress: string;
  public tradesmanId: number;
  public isOrgnization: boolean;
  public profileImage: string;
  public tradesmanBudget: DecimalPipe;
  public endDate: Data;
  public csJobStatusId: number;
  public csjqJobStatusId: number;
  public visitCharges: DecimalPipe;
  public serviceCharges: DecimalPipe;
  public otherCharges: DecimalPipe;
  public estimatedCommission: DecimalPipe;
  public materialCharges: DecimalPipe;
  public additionalCharges: DecimalPipe;
  public totalJobValue: DecimalPipe;
  public quantity: DecimalPipe;
  public chargesDescription: string;
  
  public jobActivity: Array<{
     activiyType: string,
     status: string,
    createdDate: Date,
  }>;
  public bidsList: Array<{
    tradesmanName: string,
    skill: string,
    status: string,
    createdOn: Date,
    bidsId:number,
  }>;
  public cSJobRemarksVM: Array<{
    remarksDescription: string,
    createdBy: string,
    createdOn: Date,
  }>;
  public notificationDTO: Array<{
    title: string,
    body: string,
    senderEntityId: string,
    targetActivity: string,
    createdOn: Date,
  }>;
}
export class JobQuotations {
  public JobQuotationId: string;
  public SkillName: string;
  public SubSkillName: string;
  public WorkTitle: string;
  public WorkDescription: string;
  public FirstName: string;
  public LastName: string;
  public WorkBudget: DecimalPipe;
  public JobAddress: string;
  public DesiredBids: string;
  public StatusName: string;
  public TradesmanName: string;
  public CreatedOn: Data;
  public CreatedBy: Data;
  public ModifiedOn: Data;
  public ModifiedBy: Data;

}
export class AllJobsCountVm {
  public customerId: number;
  public jobId: number;
  public jobQuotationId: number;
  public jobTitle: string;
  public jobDetailId: string;
  public City: string;
  public StartDate: string;
  public MobileNumber: string;
  public OverallRating: number;
  public noOfRecoards: number;
  public RecordNo: number;
  public comments: string;
  public customerName: string;
}

export class GetReciveBids {
  public jobQoutation: number;
  public Name: string;
  public Address: string;
  public CraetedOn: Date;
 
}

export class ReciveBidDetails {
  public title: string;
  public budget: DecimalPipe;
  public tradesmanBudget: DecimalPipe;
  public description: string;
  public address: string;

}
export class Bids {
  public BidsId: string;
  public JobQuotationId: string;
  public TradesmanName: string;
  public CustomerId: string;
  public CustomerName: string;
  public Comments: string;
  public Amount: DecimalPipe;
  public IsSelected: string;
  public StatusId: string;
  public Status: string;
  public CreatedOn: Date;
  public CreatedBy: string;
  public ModifiedOn: Date;
  public ModifiedBy: string;



}
export class JobActivity {
  public ActiviyType: string;
  public Status: string;
  public CreatedDate: Date;

}
export class PageingCount {
  public Id: number;
  public Name: string;
}

