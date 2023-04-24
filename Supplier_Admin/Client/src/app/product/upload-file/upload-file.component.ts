import { Component, OnInit } from '@angular/core';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  selectedFiles: any;
  image: any;
  constructor(public uploadFileService: UploadFileService) { }

  ngOnInit(): void {
    console.log(this.fileget())
    //this.uploadFileService.getFileByName('10.jpeg');
  }
  selectFile(event:Event) {
    
    //this.selectedFiles = event.target.files;
    this.selectedFiles = (<HTMLInputElement>event.target).files;
  }
  postFile() {
    const file = this.selectedFiles.item(0);
    this.uploadFileService.uploadFile(file,'testFile','')
  }
  fileget() {
  //  this.uploadFileService.getFileByName('10.jpeg').then(x => {
  //    this.image = x;
  //    this.uploadFileService.dataUrlToFile(this.image, 'testFile').then(y => {
  //      console.log(y);
  //    });
  //  })
  }
}
