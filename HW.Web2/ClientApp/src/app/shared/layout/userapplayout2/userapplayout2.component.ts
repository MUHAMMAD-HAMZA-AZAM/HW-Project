import { Component, OnInit, AfterViewInit, PLATFORM_ID, Inject } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../../Enums/enums';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { MessagingService } from '../../CommonServices/messaging.service';
import { isPlatformBrowser } from '@angular/common';
import { CommonService } from '../../HttpClient/_http';
import { ISkill } from '../../Enums/Interface';

@Component({
  selector: 'app-userapplayout2',
  templateUrl: './userapplayout2.component.html',
  styleUrls: ['./userapplayout2.component.css']
})
export class Userapplayout2Component implements OnInit {
  public loginCheck: boolean = false;
  public cssUrlBootStrap: SafeResourceUrl;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public skillDetails: ISkill = {} as ISkill;
  public activeskillList: ISkill[] = [];

  constructor(private _messagingService: MessagingService, public sanitizer: DomSanitizer, @Inject(PLATFORM_ID) private platformId: Object, private _httpService: CommonService) {
    this.cssUrlBootStrap = {} as SafeResourceUrl;
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }
  }

  ngOnInit() {
   // this.GetTrustedUrl();
    //var head = document.getElementsByTagName('head')[0];
    //var link = document.createElement('link');
    //link.id = 'customStyle';
    //link.rel = 'stylesheet';
    //link.type = 'text/css';
    //link.href = 'assets/css/custom-style.css';
    //link.media = 'all';
    //head.appendChild(link);
    this._messagingService.receiveMessage();

    this.IsUserLogIn();
    this.populateTradesmanSkills();
  }
  public populateTradesmanSkills() {
    this._httpService.get(this._httpService.apiRoutes.Tradesman.GetSkillList + "?skillId=0").subscribe(result => {
      this.activeskillList = result;
      this.skillDetails = this.activeskillList[0];
      console.log(this.skillDetails)
    });
  }
  public GetTrustedUrl() {
    this.cssUrlBootStrap = this.sanitizer.bypassSecurityTrustResourceUrl('assets/css/style.css');
  }
  public scrollTop(event: Event) {
    //window.scroll(0, 0);
  }
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
        this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }


}
