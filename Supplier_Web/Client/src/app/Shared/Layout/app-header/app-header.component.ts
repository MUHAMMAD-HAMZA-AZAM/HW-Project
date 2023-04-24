
import { Component, OnInit, TemplateRef, Renderer2 } from '@angular/core';
import { HttpStatusCode } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
import { IDisplayNotification, ILoggedUserDetails, IPersonalDetailsVM, IPostNotificationVM, IProductCategory, IProductCategoryGroup, IProductCategoryList, IProductNames, IProductSubCategoryList } from '../../Enums/Interface';
import { CartService } from '../../HttpClient/cart.service';
import { CommonService } from '../../HttpClient/HttpClient';
import { MessagingService } from '../../HttpClient/messaging.service';
import { OrderItem } from '../../Models/IOrderItem';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { IResponseVM, StatusCode } from '../../Enums/enum';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  filterForm: FormGroup;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  cartItems: OrderItem[] = [];
  categoryList: IProductCategoryList[] = [];
  subCategoryList: IProductSubCategoryList[] = [];
  subCategoryGroupList: IProductCategoryGroup[] = [];
  topFiveProductList: IProductCategory[] = [];
  searchproductList: IProductNames[] = [];
  productName: string = "";
  public searchProductCTRL: Subscription;
  subTotal: number = 0;
  toggleDropdown: boolean = false;
  isSubCategory: boolean = false;
  isSubCategoryGroup: boolean = false;
  disableFlag: boolean = true;
  loginUserName: string | null = "";
  public loggedUserDetails: ILoggedUserDetails;
  public decodedToken: any = "";
  public componentRouteURL: string = "";
  public submitted: boolean = false;
  public notificationList: IDisplayNotification[] = [];
  public pageSize: number = 10;
  public pageNumber: number = 1;
  elementIndex: number = 0;
  dataNotFound: boolean = false;
  unreadNotificationCount: number = 0;
  public response: IResponseVM;
  public isDropdown: boolean = false;
  constructor(public _httpService: CommonService,
    public fb: FormBuilder,
    private _cartService: CartService,
    public _router: Router,
    public _modalService: NgbModal,
    private _messagingService: MessagingService,
    private angularFireMessaging: AngularFireMessaging,
    private renderer: Renderer2
  ) {

    this.loggedUserDetails = {} as ILoggedUserDetails;

    this.filterForm = {} as FormGroup;
  }

  ngOnInit(): void {
    let obj = {
      cats: [
        { cat: 'name', hasChild: true, subChild: [] }
      ]
    }
    let categories = [{ id: "1", name: 'painter' }, { id: "2", name: 'pulmber' }, { id: "3", name: 'barber' }, { id: "4", name: 'electrician' }, { id: "5", name: 'santiry' }];
    let subcategories = [{ id: "1", name: 'painter1', catId: '2' }, { id: "2", name: 'pulmber1', catId: '2' }, { id: "3", name: 'barber1', catId: '2' }, { id: "4", name: 'electrician1', catId: '2' }, { id: "5", name: 'santiry1', catId: '2' }];
    let catGroup = [{ id: "1", name: 'painter2', catGroup: '1' }, { id: "2", name: 'pulmber2', catGroup: '5' }, { id: "3", name: 'barber2', catGroup: '5' }, { id: "4", name: 'electrician2', catGroup: '5' }, { id: "5", name: 'santiry2', catGroup: '5' }];
    let makeData = [];
    let makeData1 = [];
    categories.forEach(main => {
      let hasMainChilds = subcategories.filter(x => x.catId == main.id);
      let mainCats = {
        ...main,
        hasChildCats: false
      }
      if (hasMainChilds.length > 0) {
        mainCats.hasChildCats = true;
      }
      if (makeData1.length <= 0) {
        subcategories.forEach(subC => {
          let hasSubChild = catGroup.filter(x => x.catGroup == subC.id);
          let subCats = {
            ...subC,
            hasGroupCats: false
          }
          if (hasSubChild.length > 0) {
            subCats.hasGroupCats = true;
          }

          makeData1.push(subCats)
        })

      }
      makeData.push(mainCats)
    })
    //console.table(makeData);
    //console.table(makeData1);
    this._messagingService.receiveMessage();
    let token = localStorage.getItem('web_auth_token');
    this.decodedToken = token != null ? this.jwtHelperService.decodeToken(token) : null;
    if (this.decodedToken != null) {
      this.getLoggedUserDetails(this.decodedToken.Role, this.decodedToken.UserId);
      //this.angularFireMessaging.messages.subscribe((payload: any) => {
      //  debugger;
      //  console.log("web new message received. ", payload);
      //  this._messagingService.currentMessage.next(payload);
      //  this._messagingService.showCustomPopup(payload);
      //})
      this._messagingService.currentMessage.subscribe(x => {
        this.getNotificationList();
      })
    }

    this._httpService.subject$.subscribe((checkDifferentUser): any => {
      if (checkDifferentUser) {
        this._cartService.clearCartItems();
      }
    });
    this._cartService.getItems().subscribe(res => {
      this.cartItems = res;
      this.subTotal = this.cartItems.reduce((accumulate, item) => {
        return accumulate + (item.product.discountedPrice ? item.product.discountedPrice * item.quantity : item.product.price * item.quantity)
      }, 0)
    });
    this.filterProductForm();
    this.getTopFiveProductCategory();
    this.getCategoryList();
    this.searchProductCTRL = this.filterForm.controls['productName'].valueChanges.pipe(debounceTime(1000), distinctUntilChanged()).subscribe(name => {
      if (name?.length >= 3) {
        this._httpService.get(this._httpService.apiUrls.Supplier.Product.GetProductsList + "?productName=" + name).subscribe(res => {
          this.searchproductList = res.resultData;

        });
      }
      else {
        this.searchproductList = [];
      }

    });
  }
  ngOnDestroy() {
    this.searchProductCTRL.unsubscribe();
  }
  get f() {
    return this.filterForm.controls;
  }
  getNotificationList() {
    this._httpService.GetData(this._httpService.apiUrls.Notifications.GetNotificationsByUserId + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber + "&userId=" + this.decodedToken.UserId, false).then(result => {
      let res: IPostNotificationVM[] = result;
      if (res) {
        this.notificationList = [];
        this.unreadNotificationCount = res.filter(x => !x.isRead).length;
        res.forEach(x => {
          if (x.body) {
            let body = JSON.parse(x.body);
            let obj = {
              createdOn: x.createdOn,
              title: body.notification.title,
              content: body.notification.body,
              isRead: x.isRead,
              notificationId: x.notificationId,
              targetActivity: body.data.targetActivity
            }
            this.notificationList.push(obj);
          }

        })
      }
    })
  }
  navigateToRoute(targetActivity: string) {
    if (targetActivity == "OrderStatus") {
      this._httpService.NavigateToRoute(this._httpService.apiRoutes.User.Orders);
    }
  }
  markNotificationAsRead(notificationId: number, targetActivity: string, index: number) {
    this._httpService.GetData(this._httpService.apiUrls.Notifications.MarkNotificationAsRead + `?notificationId=${notificationId}`, false).then(x => {
      if (x.status == HttpStatusCode.Ok) {
        this.elementIndex = index;
        this.getNotificationList();
        this.navigateToRoute(targetActivity);
      }
    })
  }
  identify(index: number, item: any) {
    return item.notificationId
  }
  getLoggedUserDetails(userRole: string, userId: string) {
    this._httpService.GetData1(this._httpService.apiUrls.Customer.LoggedUserDetails.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).then(res => {
      this.response = <any>res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = this.response.resultData;
        this._messagingService.requestPermission(this.loggedUserDetails.firebaseClientId);
        this.loginUserName = this.loggedUserDetails?.firstName ? this.loggedUserDetails.firstName + ' ' + this.loggedUserDetails.lastName : null
      }

    });
  }
  handleCategoryMenu(e: Event) {
    this.toggleDropdown = !this.toggleDropdown;
    let categoryDropdwon = document.querySelector('.product-category-menu');
    this.toggleDropdown ? categoryDropdwon?.classList.add('active-pcm') : categoryDropdwon?.classList.remove('active-pcm')
  }
  onClickedOutside(e: Event) {
    if (this.toggleDropdown) {
      let categoryDropdwon = document.querySelector('.product-category-menu');
      categoryDropdwon?.classList.remove('active-pcm')
      this.toggleDropdown = false;
    }
  }
  onClickedIn(e: Event) {
    this.isDropdown = true;
  }
  onClickedOut(e: Event) {
    this.isDropdown = false;
  }

  removeCartItem(item: OrderItem) {
    this._cartService.removeItem(item);
  }
  getTopFiveProductCategory() {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetTopFiveProductCategory, false).then(result => {
      this.topFiveProductList = (<any>result).resultData;
    })
  }

  Logout() {
    localStorage.removeItem('web_auth_token');
    this._httpService.subject$.next(false);
    this._httpService.NavigateToRoute(this._httpService.apiRoutes.Login.login);
  }
  // Category Lists
  getCategoryList() {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetActiveProducts).then(res => {
      this.categoryList = res.resultData;
    });
  }
  getSubCategoryList(productId: number | undefined, fromMobile: boolean = false) {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetProductSubCategoryById + `?productCatgoryId=${productId}`).then(res => {
      this.subCategoryList = res;
      this.isSubCategoryGroup = false;
      if (this.subCategoryList.length > 0) {
        if (!fromMobile) {
          this.isSubCategory = true;
          const subCat = document.querySelector('.sub_categories')
          if (subCat) {
            subCat.classList.remove('d-none');
            subCat.classList.add('d-block')
          }
        }
      }
      else {
        //this.clearDropdown();
        this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Product.Category, { queryParams: { lvl: 'one', categoryId: productId } })
      }
    });
  }
  getProductCategoryGroupList(subCategoryId: number | undefined, fromMobile: boolean = false) {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetProductCategoryGroupListById + `?subCategoryId=${subCategoryId}`).then(res => {
      this.subCategoryGroupList = res.resultData;
      if (this.subCategoryGroupList.length > 0) {
        if (!fromMobile) {
          this.isSubCategoryGroup = true;
          const subCatGroup = document.querySelector('.category_group')
          if (subCatGroup) {
            subCatGroup.classList.remove('d-none');
            subCatGroup.classList.add('d-block')
          }
        }
      }
      else {
        //this.clearDropdown();
        this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Product.Category, { queryParams: { lvl: 'two', categoryId: subCategoryId } })
      }
    });
  }
  navigateToCategoryGroupProducts(catgoryGroupId: number | undefined) {
    //this.clearDropdown();
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Product.Category, { queryParams: { lvl: 'three', categoryId: catgoryGroupId } })
  }

  handleMouseLeave() {
    const subCats = document.querySelector('.sub_categories');
    if (subCats) {
      subCats.classList.add("d-none")
    }
    const catGroup = document.querySelector('.category_group')
    if (catGroup) {
      catGroup.classList.add('d-none');
    }
  }
  filterProductForm() {
    this.filterForm = this.fb.group({
      productName: ['', [Validators.required, Validators.minLength(3)]]
    })
  }


  filterProduct() {
    if (!this.filterForm.valid)
      this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Index)
    else {
      this.productName = this.filterForm.controls['productName'].value;
      this.productName = this.productName.trim();
      this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Product.Name, { queryParams: { productName: this.productName } });
    }
  }

  clearDropdown() {
    let categoryDropdwon = document.querySelector('.product-category-menu');
    categoryDropdwon?.classList?.remove('active-pcm')
    this.isSubCategory = false;
    this.isSubCategoryGroup = false;
    this.toggleDropdown = false;
  }

  //----------------- Get Required Component URL
  public getRouteName(routeName: string, modalName: TemplateRef<any>) {

    if (this.decodedToken == null) {
      if (routeName == "viewcart") {
        this._modalService.open(modalName, { centered: true });
        this.componentRouteURL = this._httpService.apiRoutes.Cart.viewcart;
      }
      else {
        this._modalService.open(modalName, { centered: true });
        this.componentRouteURL = this._httpService.apiRoutes.Cart.checkout;
      }
    }
    else {
      if (routeName == "viewcart") {
        this._httpService.NavigateToRoute(this._httpService.apiRoutes.Cart.viewcart);
      }
      else {
        this._httpService.NavigateToRoute(this._httpService.apiRoutes.Cart.checkout);
      }
    }
  }

  //----------------- Go for Account Login
  public loginAccount() {
    this._modalService.dismissAll();
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Login.login, { queryParams: { returnUrl: this.componentRouteURL } });
  }

}
