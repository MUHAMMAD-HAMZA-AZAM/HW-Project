import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CartService } from './Shared/HttpClient/cart.service';
import { MessagingService } from './Shared/HttpClient/messaging.service';
//import { CartService } from '../Shared/HttpClient/cart.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  template: string = `<img class="loader-img" src="assets/hoomwork-loader.gif" style="position:absolute; width:70px; height:70px; top:48%; left:48%; z-index:999;">`;
  title = 'Hoomwork Web';
  constructor(@Inject(PLATFORM_ID) private platformId: Object, private _cartService: CartService) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }
  }
  ngOnInit() {
    var item = localStorage.getItem("ca_items");
    let cartItems = item != null ? JSON.parse(item) : [] as Array<any>;
    // let cartItems = JSON.parse(localStorage.getItem("ca_items")) as Array<any>;
    if (cartItems) {
      this._cartService.populateCart(cartItems);
    }
    //this.messagingService.requestPermission()
    //this.messagingService.receiveMessage()
    //this.message = this.messagingService.currentMessage
  }
  //public scrollTop(event: any) {
  //  window.scroll(0, 0);
  //}

}
