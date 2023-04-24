import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { JazzCashModel } from '../../Shared/Models/payments';

@Component({
  selector: 'app-jazz-cash-init',
  templateUrl: './jazz-cash-init.component.html',
  styleUrls: ['./jazz-cash-init.component.css']
})
export class JazzCashInitComponent implements OnInit {
  public jazzCashData;
  public hashString: string = "";
  public pp_Amount: number = 0;
  public pp_TxnRefNo: string = "";
  public pp_BillReference: string = "";
  public pp_TxnDateTime: string = "";
  public pp_TxnExpiryDateTime: string = "";
  public hashValue: string = "";
  public pp_Description: string = "";
  public transactionType: string = "";
  @ViewChild('btn', { static: true }) clickBtn: ElementRef;

  constructor(
    private route: ActivatedRoute,
    public common: CommonService,
    private _metaService: metaTagsService,
  ) {

    this.clickBtn = {} as ElementRef;
    this.jazzCashData = {} as JazzCashModel;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {

      this.pp_Amount = params['amount'];
      this.pp_TxnRefNo = params['paymentRefNo'];
      this.pp_BillReference = params['billReference'];
      this.transactionType = params['transactionType'];
      if (this.transactionType == "MWALLET") {
        this.transactionType = "";
      }
      var currentdate = new Date();
      this.pp_TxnDateTime = this.getFormattedDate(currentdate);
      currentdate.setDate(currentdate.getDate() + 1);
      this.pp_TxnExpiryDateTime = this.getFormattedDate(currentdate);
      this.pp_Description = "HoomWork";
      this.populateData();
      this.genreateHash();

    });
    this.bindMetaTags();
  }

  populateData() {

    this.jazzCashData.pp_Version = "1.1";
    this.jazzCashData.pp_TxnType = this.transactionType;
    this.jazzCashData.pp_Language = "EN";
    this.jazzCashData.pp_MerchantID = "00100301";
    this.jazzCashData.pp_SubMerchantID = "";
    this.jazzCashData.pp_Password = "29w3cx29dz";
    this.jazzCashData.pp_BankID = "";
    this.jazzCashData.pp_ProductID = "";
    this.jazzCashData.pp_TxnRefNo = this.pp_TxnRefNo;
    this.jazzCashData.pp_Amount = this.pp_Amount;
    this.jazzCashData.pp_TxnCurrency = "PKR";
    this.jazzCashData.pp_TxnDateTime = this.pp_TxnDateTime;
    this.jazzCashData.pp_BillReference = this.pp_BillReference;
    this.jazzCashData.pp_Description = this.pp_Description;
    this.jazzCashData.pp_TxnExpiryDateTime = this.pp_TxnExpiryDateTime;
    this.jazzCashData.pp_ReturnURL = "https://www.hoomwork.com/weblivedll/api/Integration/JazzCashCallBack";
    this.jazzCashData.pp_SecureHash = "";
    this.jazzCashData.ppmpf_1 = "1";
    this.jazzCashData.ppmpf_2 = "supplier";
    this.jazzCashData.ppmpf_3 = "3";
    this.jazzCashData.ppmpf_4 = "4";
    this.jazzCashData.ppmpf_5 = "5";

    setTimeout(() => {
      document.getElementById('proceedMerchantPayment')?.click()
    }, 2000);
  }

  getFormattedDate(currentdate: Date) {
    var todayDate = currentdate.getFullYear()
      + ('0' + (currentdate.getMonth() + 1)).slice(-2)
      + ('0' + currentdate.getDate()).slice(-2)
      + ('0' + currentdate.getHours()).slice(-2)
      + ('0' + currentdate.getMinutes()).slice(-2)
      + ('0' + currentdate.getSeconds()).slice(-2);

    return todayDate;
  }


  genreateHash() {

    this.CalculateHash();
    var IntegritySalt = document.getElementById("salt")?.innerText;
    //var hash = IntegritySalt != null ? this.common.encrypt(this.hashString, IntegritySalt) : "";

    //console.log('hash: ' + hash);
  }
  proceedMerchantPayment() {
    //let data = {
    //  pp_Version: (<HTMLInputElement>document.getElementsByName("pp_Version")[0]).value,
    //  pp_TxnType: (<HTMLInputElement>document.getElementsByName("pp_TxnType")[0]).value,
    //  pp_Language: (<HTMLInputElement>document.getElementsByName("pp_Language")[0]).value,
    //  pp_MerchantID: (<HTMLInputElement>document.getElementsByName("pp_MerchantID")[0]).value,
    //  pp_SubMerchantID: (<HTMLInputElement>document.getElementsByName("pp_SubMerchantID")[0]).value,
    //  pp_Password: (<HTMLInputElement>document.getElementsByName("pp_Password")[0]).value,
    //  pp_BankID: (<HTMLInputElement>document.getElementsByName("pp_BankID")[0]).value,
    //  pp_ProductID: (<HTMLInputElement>document.getElementsByName("pp_ProductID")[0]).value,
    //  pp_TxnCurrency: (<HTMLInputElement>document.getElementsByName("pp_TxnCurrency")[0]).value,
    //  pp_TxnDateTime: (<HTMLInputElement>document.getElementsByName("pp_TxnDateTime")[0]).value,
    //  pp_TxnExpiryDateTime: (<HTMLInputElement>document.getElementsByName("pp_TxnExpiryDateTime")[0]).value,
    //  //pp_SecureHash: (<HTMLInputElement>document.getElementsByName("pp_SecureHash")[0]).value,
    //  pp_TxnRefNo: (<HTMLInputElement>document.getElementsByName("pp_TxnRefNo")[0]).value,
    //  pp_Amount: (<HTMLInputElement>document.getElementsByName("pp_Amount")[0]).value,
    //  pp_BillReference: (<HTMLInputElement>document.getElementsByName("pp_BillReference")[0]).value,
    //  pp_Description: (<HTMLInputElement>document.getElementsByName("pp_Description")[0]).value,
    //  pp_ReturnURL: (<HTMLInputElement>document.getElementsByName("pp_ReturnURL")[0]).value,
    //  ppmpf_1: (<HTMLInputElement>document.getElementsByName("ppmpf_1")[0]).value,
    //  ppmpf_2: (<HTMLInputElement>document.getElementsByName("ppmpf_2")[0]).value,
    //  ppmpf_3: (<HTMLInputElement>document.getElementsByName("ppmpf_3")[0]).value,
    //  ppmpf_4: (<HTMLInputElement>document.getElementsByName("ppmpf_4")[0]).value,
    //  ppmpf_5: (<HTMLInputElement>document.getElementsByName("ppmpf_5")[0]).value,
    //  salt: document.getElementById("salt").innerText
    //};
    this.common.PostData("Integration/ProceedToJazzCash", this.jazzCashData, true, true).then(result => {

      var data = result;
      if (data.status == 200) {
        ((<HTMLInputElement>document.getElementsByName("pp_SecureHash")[0]).value = data.resultData.pp_SecureHash + '');
        ((<HTMLInputElement>document.getElementsByName("ppmpf_1")[0]).value = data.resultData.ppmpf_1 + '');
        this.redirectPage();
      }
      //
    });
  }
  redirectPage() {

    if (this.pp_Amount == this.jazzCashData.pp_Amount && this.pp_TxnRefNo == this.jazzCashData.pp_TxnRefNo
      && this.pp_BillReference == this.jazzCashData.pp_BillReference) {

      setTimeout(() => {
        document.getElementById('submitFormBtn')?.click()
      }, 5000);

    }

  }

  CalculateHash() {

    var IntegritySalt = document.getElementById("salt")?.innerText;
    this.hashString = '';
    this.hashString += IntegritySalt + '&';
    this.hashString += this.jazzCashData.pp_Amount + '&';
    if (this.jazzCashData.pp_BankID != '') {
      this.hashString += this.jazzCashData.pp_BankID + '&';
    }
    this.hashString += this.jazzCashData.pp_BillReference + '&';
    this.hashString += this.jazzCashData.pp_Description + '&';
    if (this.jazzCashData.pp_Language != '') {
      this.hashString += this.jazzCashData.pp_Language + '&';
    }
    if (this.jazzCashData.pp_MerchantID != '') {
      this.hashString += this.jazzCashData.pp_MerchantID + '&';
    }
    if (this.jazzCashData.pp_Password != '') {
      this.hashString += this.jazzCashData.pp_Password + '&';
    }
    if (this.jazzCashData.pp_ProductID != '') {
      this.hashString += this.jazzCashData.pp_ProductID + '&';
    }
    if (this.jazzCashData.pp_ReturnURL != '') {
      this.hashString += this.jazzCashData.pp_ReturnURL + '&';
    }
    if (this.jazzCashData.pp_SubMerchantID != '') {
      this.hashString += this.jazzCashData.pp_SubMerchantID + '&';
    }
    if (this.jazzCashData.pp_TxnCurrency != '') {
      this.hashString += this.jazzCashData.pp_TxnCurrency + '&';
    }
    this.hashString += this.jazzCashData.pp_TxnDateTime + '&';
    this.hashString += this.jazzCashData.pp_TxnExpiryDateTime + '&';
    this.hashString += this.jazzCashData.pp_TxnRefNo + '&';
    if (this.jazzCashData.pp_TxnType != '') {
      this.hashString += this.jazzCashData.pp_TxnType + '&';
    }
    if (this.jazzCashData.pp_Version != '') {
      this.hashString += this.jazzCashData.pp_Version + '&';
    }
    if (this.jazzCashData.ppmpf_1 != '') {
      this.hashString += this.jazzCashData.ppmpf_1 + '&';
    }
    if (this.jazzCashData.ppmpf_2 != '') {
      this.hashString += this.jazzCashData.ppmpf_2 + '&';
    }
    if (this.jazzCashData.ppmpf_3 != '') {
      this.hashString += this.jazzCashData.ppmpf_3 + '&';
    }
    if (this.jazzCashData.ppmpf_4 != '') {
      this.hashString += this.jazzCashData.ppmpf_4 + '&';
    }
    if (this.jazzCashData.ppmpf_5 != '') {
      this.hashString += this.jazzCashData.ppmpf_5 + '&';
    }
    this.hashString = this.hashString.slice(0, -1);
    (<HTMLInputElement>document.getElementById("hashValuesString")).value = this.hashString;
  }
  public bindMetaTags() {
    this.common.get(this.common.apiUrls.CMS.GetSeoPageById + "?pageId=19").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
