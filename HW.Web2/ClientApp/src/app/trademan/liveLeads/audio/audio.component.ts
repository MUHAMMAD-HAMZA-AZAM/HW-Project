import { Component } from '@angular/core';
import * as RecordRTC from 'recordrtc';
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: 'app-audio',
  templateUrl: './audio.component.html',
  styleUrls: ['./audio.component.css']
})
export class BidAudioComponent {
  title = 'record-app';
  isRecording = false;
  private record: any="";
  // private url;
  //private urls = [];
  private error: string="";
    blobUrl: any;

  constructor(private domSanitizer: DomSanitizer) {
  }
  startRecording() {
    
    this.isRecording = true;
    let mediaConstraints = {
      video: false,
      audio: true
    };
    navigator.mediaDevices
      .getUserMedia(mediaConstraints)
      .then(this.successCallback.bind(this), this.errorCallback.bind(this));
  }

  successCallback(stream:any) {
    var options = {
      mimeType: "audio/wav",
      numberOfAudioChannels: 1
    };

    //Start Actuall Recording
    var StereoAudioRecorder = RecordRTC.StereoAudioRecorder;
    this.record = new StereoAudioRecorder(stream, options);
    this.record.record();
  }

  stopRecording() {
    
    this.isRecording = false;
    this.record.stop(this.processRecording.bind(this));
  }

  processRecording(blob: any) {
    
    var url = URL.createObjectURL(blob);
    this.blobUrl = this.domSanitizer.bypassSecurityTrustUrl(url);

    console.log(this.blobUrl)
  }

  sanitize(url: string) {
    this.blobUrl = this.domSanitizer.bypassSecurityTrustUrl(url);
    console.log(this.blobUrl)
    return this.blobUrl;
  }

  errorCallback(error: string) {
    this.error = 'Can not play audio in your browser';
  }
}
