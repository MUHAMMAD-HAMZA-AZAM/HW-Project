export var ApiUrls = {
  Login: {
    customer: "login/Customer",
    supplier: "login/Supplier",
    tradesman: "login/Tradesman",
    login: "login/Customer",
  },
  Common: {
    Header: {
      Dashboard: "Home",
      userSignUp: "registration/appregistrationbasic/user",
      packagesplans: "/packagesplans/viewcart"

    },
  },
  User: {
    UserDefault: "User/Default",
    GetFinishedJobs: "User/FinishedJobs/List",
    GetFinishedJobDetail: "User/FinishedJobs/Detail",
    InProgreesJobDetails: "User/InProgressJob/Details",
    TradesmanProfile: "User/Profile/TradesmanProfile",
    ReceivedBidDetail: "User/Bids/Detail",
    Mybids: "User/MyBids/List",
    InProgressJobList: "User/InProgressJob/List",
    GetPostedJobs: "User/Quote/List",
    PersonalProfile: "User/Profile/PersonalProfile",
    PersonalProfileEdit: "User/Profile/PersonalProfileEdit",
    ReceivedBids: "User/Bids/List",
    GetPostedJobDetail: "User/Quote/Detail",
    QuoteStep1: "User/JobQuotes/step1",
    AdDetail: "/MarketPlace/SupplierAdDetails/",
    JazzCashPayment: "User/Payments/JazzCashPayment/",
    MarkeetPlace: "/MarketPlace/MarketPlaceIndex",
    Payments: "User/Payments/PaymentMethod",
    topup: "User/Payments/topup",
    Home: {
      Blogs: "User/Home/Blogs",
      BlogDetails: "User/Home/BlogDetails",
      BlogDetails1: "User/Home/BlogDetails1",
      BlogDetails2: "User/Home/BlogDetails2",
      UserAgreement: "User/Agrements/UserAgreement",
      ContactUs: "Contacts/User/ContactUs",
    },
    Quotations: {
      getQuotes1: "User/JobQuotes/step1",
      getQuotesautoselected: "User/JobQuotes/step1",
      getQuotes2: "User/JobQuotes/step2",
      SupplierHome: "supplier/home",
    }
  },
  Supplier: {
    Home: "Supplier/Home",
    Blogs: "/Supplier/Home/Blogs",
    BlogDetails: "Supplier/Home/BlogDetails",
    BlogDetails1: "Supplier/Home/BlogDetails1",
    BlogDetails2: "Supplier/Home/BlogDetails2",
    BussinesDetail: "Supplier/Registration/SupplierRegister",
    EditAd: "Supplier/PostAd/Create",
    PromoteAd: "Supplier/ManageAd/PromoteAd",
    ManageAd: "Supplier/ManageAd/Ads",
    Profile: "Supplier/Profile/Profile",
    //MarkeetPlace: "/MarketPlace/ProductCategoryHome",
    MarkeetPlace: "/MarketPlace",

  },
  Tradesman: {
    BlogDetails: "Tradesman/LiveLeads/BlogOneDetail",
    BlogDetails1: "Tradesman/LiveLeads/BlogTwoDetail",
    BlogDetails2: "Tradesman/LiveLeads/BlogThreeDetail",
    MakeBids: "Tradesman/LiveLeads/MakeBid",
    LiveLeads: "Tradesman/LiveLeads/list",
    ViewDetial: "Tradesman/LiveLeads/Detail",
    LiveLeadsDeatils: "Tradesman/LiveLeads/Details",
    ViewDetialJobs: "Tradesman/MyJobs/Detail",
    Notification: "Tradesman/Notifications/List",
    Invoice: "Tradesman/Invoice/List",
    topup: "Tradesman/Invoice/trsdesmantopup",
    MyBids: "Tradesman/MyBids/List",
    MyJobs: "Tradesman/MyJobs/List",
    BussinessRegistration: "Tradesman/Registration/Bussiness",
    //MakeBid: "Tradesman/LiveLeads/MakeBid",
    ReviewCompletedJob: "Tradesman/MyJobs/Review",
    Searchtradesmanbyskill: "/landing-page/searchtradesmanbyskill",
    tradesmanProfile: "/landing-page/tradesman-profile",
    Profile: "Tradesman/Profile/Profile"
  },

  UserManagement: {
    GetPromotionsListByUserRole: "/promotions/promotionsByUser",
    GetPromotionDetailsByUserRole: "/promotions/promotionDetails",
    GetPromotionList: "/promotions/promotionlist",
    GetCampaignList: "/promotions/campaignlist",
    SuccessMessage:"/promotions/thankyou"
  }

}
