import { Injectable } from '@angular/core';

import * as S3 from 'aws-sdk/clients/s3';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UploadFileService {
  FOLDER = 'photos/product/';
  constructor() { }
  async getFileByName(fileName:string, optionalParams?: any) {

    if (fileName) {
      const bucket = new S3(
        {
          accessKeyId: 'AKIA3GCLSU3JEE56GURL',
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
        data.file = 'data:image/jpeg;base64,' + file.Body?.toString('base64');
        return data;
      } else {
        return 'data:image/jpeg;base64,' + file.Body?.toString('base64');
      }
    }
  }

}
