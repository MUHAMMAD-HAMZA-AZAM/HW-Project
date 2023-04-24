import { Injectable } from '@angular/core';

import * as S3 from 'aws-sdk/clients/s3';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadFilesServiceService {
  FOLDER = 'photos/mainSlider/';
  imageUrl = "";
  resData: BehaviorSubject<any> = new BehaviorSubject(null);
  data = { message: "", data: "" };
  constructor() { }
  validateandUploadFile(file, Iheight, Iwidth) {
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

            this.uploadFile(file, 'fileName');
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


  async uploadFile(file, fileName) {
                   
    if (file != null) {
      const bucket = new S3(
        {
          accessKeyId: 'AKIA3GCLSU3JCXJJ3A4C',
          secretAccessKey: '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',
          region: 'ap-south-1'
        }
      );
      const params = {
        Bucket: 'homwork-bucket2',
        Key: this.FOLDER + fileName,
        Body: file,
        ACL: 'public-read',
        location: 'https://console.aws.amazon.com/s3/buckets/homwork-bucket2'

      };
      var that = this;
      await bucket.upload(params, function (err, data) {
        if (err) {
          console.log('There was an error uploading your file: ', err);
          return false;
        }
      });
    }
  }
  async deleteFile(fileName) {
    const bucket = new S3(
      {
        accessKeyId: 'AKIA3GCLSU3JCXJJ3A4C',
        secretAccessKey: '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',
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
  async deleteMultipleFile(files) {
                   
    const bucket = new S3(
      {
        accessKeyId: 'AKIA3GCLSU3JCXJJ3A4C',
        secretAccessKey: '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',
        region: 'ap-south-1'
      }
    );
    const params = {
      Bucket: 'homwork-bucket2',
      Delete: { // required
        Objects: files
      }
    };
    await bucket.deleteObjects(params, function (err, data) {
      if (err) console.log(err); // an error occurred
      else console.log(data);           // successful response
    });
  }
  async getFileByName(fileName, optionalParams?: any) {
    if (fileName) {
      const bucket = new S3(
        {
          accessKeyId: 'AKIA3GCLSU3JCXJJ3A4C',
          secretAccessKey: '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',
          region: 'ap-south-1'
        }
      );
      const params = {
        Bucket: 'homwork-bucket2',
        Key: fileName,
      };

      let file = await bucket.getObject(params).promise();
      if (optionalParams) {
        let data = optionalParams;
        data.file = 'data:image/jpeg;base64,' + file.Body.toString('base64');
        return data;
      } else {
        return 'data:image/jpeg;base64,' + file.Body.toString('base64');
      }
    }
  }
  async base64ToFile(dataUrl): Promise<File[]> {
    let files = [];
    for (let i = 0; i < dataUrl.length; i++) {
      await fetch(dataUrl[i]).then(res => res.blob())
        .then(blob => {
          files.push(new File([blob], (Math.random() * 100).toString() + '.jpeg', { type: "image/jpeg" }))
        })

    }
    return files;
  }
  getBase64(file) {
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

