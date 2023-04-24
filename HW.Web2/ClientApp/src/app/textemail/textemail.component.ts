import { Component, OnInit } from '@angular/core';
import { CommonService } from '../shared/HttpClient/_http';

@Component({
  selector: 'app-textemail',
  templateUrl: './textemail.component.html',
  styleUrls: ['./textemail.component.css']
})
export class TextemailComponent implements OnInit {

  constructor(public common: CommonService,
) { }

  ngOnInit(): void {
  }

  sendEmail() {
    this.common.GetData(this.common.apiRoutes.Communication.SendTestEmail, true).then(result => {
      var res = result ;
    }, error => {
      console.log(error);
    });
  }

}
