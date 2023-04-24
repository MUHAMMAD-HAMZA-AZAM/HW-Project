import { Component } from "@angular/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  //template: string = `<div class="loader-container">
  //  <div class="loader-logo">
  //    <div class="loader-circle">
  //     <img src="../assets/hoomwork-loader.gif" />
  //   </div>
  //  </div>
  //</div>`;
  template: string = `<img src="../assets/hoomwork-loader.gif" />`;

}
