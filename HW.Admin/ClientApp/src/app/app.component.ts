import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  template: string = `<img height=80px; wigth=50px
   src="../assets/hoomwork-loader.gif"/>`;
  title = 'Hoomwork Admin';
}
