export var ApiRoutes = {
  Home: {
    Dashboard: "/home/dashboard",
  },
  Product: {
    AddNewProduct:"/product/add-new-product",
    ProductList:"/product/product-list",
    ProductDetail:"/product/product-detail",
    EditProduct:"/product/edit-product"
  },
  Promotions: {
    PromotionsList:"/promotions/Promotion",
    PromotionType:"/promotions/PromotionTypes"
  },
  Registration: {
    Login:'/login',
    Signup: '/signup',
    landing: '/landing', 
    forgotpassword: '/forgotpassword'
  },
  ForgotPassword: {
    ForgotPasswordCode:'/forgotpasswordcode',
    ResetPassword:'/resetpassword'
  },
  ProfileManagement: {
    ProfileManagement:'/profile/management'
  },
  Order: {
    OrderList:"/inventory/orderlist",
    OrderDetails: '/inventory/orderDetails',
   CancelledOrdersList: '/inventory/cancelled-orders',
    ProductsInventory: '/inventory/products-inventory',
    SalesSummary: '/inventory/app-sales-summary',
    orderTrack:"/inventory/orderTrack",
  },
  Payment: {
    PaymentHistory:"/payment/app-payment-history",
    PaymentHistoryDetails:"/payment/app-payment-history-details",
    WithdrawalRequest:"/payment/app-withdrawal",
    WithdrawalRequestList:"/payment/app-withdrawallist"
  },
  FreeShipping: {
    freeshipping:"/freeshipping/free-shipping"
  }
}
