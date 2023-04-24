import { Injectable } from '@angular/core';
//import * as AWS from 'aws-sdk/global';
import * as S3 from 'aws-sdk/clients/s3';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadFileService {
  FOLDER = 'photos/product/';
  private readonly ACCESSKEYID = "AKIA3GCLSU3JEBL6OU7U";
  private readonly SECRETACCESSKEY = "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";
  readonly AWSPATH = 'https://homwork-bucket2.s3.ap-south-1.amazonaws.com/'
  compressImaged = "";
  imageUrl = "";
  resData: BehaviorSubject<any> = new BehaviorSubject(null);
  data = { message: "", data: "" };
  constructor() { }
  validateandUploadFile(file: any, Iheight: any, Iwidth: any) {
    let fileToUpload = file;
    if (fileToUpload.type == "image/jpeg" || fileToUpload.type == "image/png" || fileToUpload.type == "image/jpeg") {
      //Show image preview
      let reader = new FileReader();
      reader.onload = (event: any) => {
        var img = new Image();
        img.onload = () => {
          let width = img.width;
          let height = img.height;
          if (width <= Iwidth && height <= Iheight) {
            this.imageUrl = event.target.result;

            this.uploadFile(file, 'fileName','');
          } else {
            this.data.message = "You can maximum upload " + Iheight + " * " + Iwidth + " File";
            this.data.data = "";
            this.resData.next(this.data);
            return this.resData;
          }
        };

        img.src = event.target.result;
      }
      reader.readAsDataURL(fileToUpload);
    } else {
      this.data.message = "You can't be able to upload file except JPG and PNG format";
      this.data.data = "";
      this.resData.next(this.data);
      return this.resData;
    }
  }
  async uploadFile(file: any, fileName: string,folderName:string) {
    if (file != null) {
      const bucket = new S3(
        {
          accessKeyId: this.ACCESSKEYID,
          secretAccessKey: this.SECRETACCESSKEY,
          region: 'ap-south-1'
        }
      );
      const params = {
        Bucket: 'homwork-bucket2',
        Key: this.FOLDER + folderName + fileName, //'photos/product/'/${formData.supplierId}/
        Body: file,
        ACL: 'public-read',
        location: 'https://console.aws.amazon.com/s3/buckets/homwork-bucket2'

      };
      var that = this;
      await bucket.upload(params, function (err: any, data: any) {
        if (err) {
          console.log('There was an error uploading your file: ', err);
          return false;
        }
      });
    }
  }
  async deleteFile(fileName: string) {
    const bucket = new S3(
      {
        accessKeyId: this.ACCESSKEYID,
        secretAccessKey: this.SECRETACCESSKEY,
        region: 'ap-south-1'
      }
    );
    const params = {
      Bucket: 'homwork-bucket2',
      Key: this.FOLDER + fileName,
    };
    await bucket.deleteObject(params, function (err, data) {
      if (err) console.log(err, err.stack); // an error occurred
      else console.log(data)
    });
  }
  async deleteMultipleFile(files: any) {
    debugger;
    const bucket = new S3(
      {
        accessKeyId: this.ACCESSKEYID,
        secretAccessKey: this.SECRETACCESSKEY,
        region: 'ap-south-1'
      }
    );
    const params = {
      Bucket: 'homwork-bucket2',
      Delete: { // required
        Objects: files
      }
    };
    console.log(params);
    await bucket.deleteObjects(params, function (err, data) {
      if (err) console.log(err); // an error occurred
      else console.log(data);           // successful response
    });
  }
  async getFileByName(fileName: string, optionalParams?: any) {
    if (fileName) {
      const bucket = new S3(
        {
          accessKeyId: this.ACCESSKEYID,
          secretAccessKey: this.SECRETACCESSKEY,
          region: 'ap-south-1'
        }
      );
      const params = {
        Bucket: 'homwork-bucket2',
        Key: fileName,
        ResponseCacheControl: 'no-cache'
      };
      let file = await bucket.getObject(params).promise();
      if (optionalParams) {
        let data = optionalParams;
        data.file = 'data:image/jpeg;base64,' + file.Body?.toString('base64');
        return data;
      } else {
        return 'data:image/jpeg;base64,' + file.Body?.toString('base64');
      }
    }
  }
  async base64ToFile(dataUrl: any): Promise<File[]> {
    let files = new Array()
    for (let i = 0; i < dataUrl.length; i++) {
      await fetch(dataUrl[i]).then(res => res.blob())
        .then(blob => {
          files.push(new File([blob], (Math.random() * 100).toString() + '.jpeg', { type: "image/jpeg" }))
        })
    }
    return files;
  }
  getBase64(file: any) {
    return new Promise(function (resolve, reject) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        resolve(reader.result)
      };
      reader.onerror = function (error) {
        reject
      };
    })
  }
  getFile() {
    return this.resData.asObservable();
  }
  dateTimeInNumber() {
    var today = new Date();
    var datetime = `${Math.random() * 1000}${today.getMonth()}-${today.getDate()}-${today.getHours()}-${today.getMinutes()}-${today.getSeconds()}-${today.getMilliseconds()}`;
    return datetime;
  }
  async dataUrlToFile(dataUrl: string, fileName: string): Promise<File> {

    const res: Response = await fetch(dataUrl);
    const blob: Blob = await res.blob();
    return new File([blob], fileName, { type: 'image/jpeg' });
  }
}
