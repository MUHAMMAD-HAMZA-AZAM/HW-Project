export var ApiRoutes = {
  Home: {
    Dashboard: "/home",
    LandingPage: "",
    Index:"/index"
  },
  Product: {
    AddNewProduct: "/product/add-new-product",
    ProductDetail: "/product/product-detail",
    Category: "/product/category",
    Name: "/product/name",
    VendorProfile: "/product/vendor",
    feedback: "/product/product-feedback",
    SearchByTag: "/product/tag",
  },
  Registration: {
    Login: '/user/login',
    Signup: '/user/signup',
    landing: '/landing'
  },
  
  Login: {
    login: "/user/login"
  },

  User: {
    DashBoard: "/user/dashboard",
    WishList: "/user/wishlist",
    Orders: "/user/orders",
    OrderDetails:"/user/order-details",
    OrderPayment: "/user/payment",
    OrderPaymentReceipt: "/user/paymentreceipt",
    OrderTrack: "/user/trackorder"
  },
    ForgotPassword: {
      forgotPasswordCode: '/user/forgotPasswordCode',
      ResetPassword: '/user/resetPinCode',
      EnterMobileNumber: '/user/forgotPassword'
  },
  Cart: {
    viewcart:"/cart/viewcart",
    checkout:"/cart/checkout",
  }
}
