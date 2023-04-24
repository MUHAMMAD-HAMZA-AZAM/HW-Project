using System.Collections.Generic;

namespace HW.Utility
{
    public static class ClientsConstants
    {
        public static class AppCenterSecret
        {
            public const string HWUserAndroid = "215deb11-0570-44c0-a540-2d1e03e2e17d";
            public const string HWUserIOS = "7df00e3c-0582-4521-aa9b-2d48cd0fd42f";
            public const string HWTradesmanAndroid = "bcb9109c-1017-4fb5-b528-46dadecb27f6";
            public const string HWTradesmanIOS = "9e656827-1efe-4289-b44c-c71ace7b1c77";
            public const string HWSupplierAndroid = "6cf5c5ab-a7e2-43e3-b431-5272ae68ec29";
            public const string HWSupplierIOS = "31fee43a-37a9-4541-b140-e21ed6d4c40c";
        }

        public static class SyncfusionLicenseKey
        {
            public const string Key = "Mjg3NTgyQDMxMzgyZTMyMmUzMFhkQTRsRGtMOXpoMWtMa09qRGlhQWFFQXBkd1hMc0F1bEdCbFJGdjhCSkE9";
        }

        public static class AndroidVersionCode
        {
            public const string HomesApp = "Homes_Android_Latest_Version";
            public const string TradesApp = "Trades_Android_Latest_Version";
            public const string SuppliersApp = "Suppliers_Android_Latest_Version";
        }
        
        public static class IOSVersionCode
        {
            public const string HomesApp = "Homes_IOS_Latest_Version";
            public const string TradesApp = "Trades_IOS_Latest_Version";
            public const string SuppliersApp = "Suppliers_IOS_Latest_Version";
        }

        public static class AlertKey
        {
            public const string ServerDown = "Server_Down_Alert";
        }

        public static class AnnouncementKey
        {
            public const string UserAnnouncement = "UserAnnouncement";
            public const string TradesmanAnnouncement = "TradesmanAnnouncement";
            public const string SuppliersAnnouncement = "SuppliersAnnouncement";
        }

        public static class AlertMessages
        {
            public const string UpdateRequiredImmidiate = "Hoomwork needs an update. To use this app, download the latest version.";
            public const string UpdateRequiredFlexable = "Hoomwork recommends to update the latest version.";
            public const string BlockedUser = "Your account has been blocked. Please contact customer support. Ph:03099994534";
        }

        public static class ApplicationLinks
        {
            public const string HoomsAndroid = "https://play.google.com/store/apps/details?id=com.User.HW";
            public const string HoomsIOS = "https://apps.apple.com/us/app/hoomwork-homes/id1490813771?ls=1";
            public const string TradesAndroid = "https://play.google.com/store/apps/details?id=com.TradesmanApp.HW";
            public const string TradesIOS = "https://apps.apple.com/us/app/hoomwork-trades/id1490237957?ls=1";
            public const string SupplierAndroid = "https://play.google.com/store/apps/details?id=com.SupplierApp.HW";
            public const string SupplierIOS = "https://apps.apple.com/us/app/hoomwork-suppliers/id1495763086?ls=1";
        }

        public static class FirebaseConstants
        {
            public const string ChannalId = "FirebasePushNotificationChannel";
            public const string HomesChannelName = "Hoomwork_Homes";
            public const string NotificationId = "100";
            public const string Tag = "Console Log";
            public const string SenderId = "299568931003";
            public const string Scope = "FCM";
        }

        public static class TokenProperties
        {
            public const string role = "Role";
            public const string userId = "UserId";
            public const string entityId = "Id";
        }
        public static class UserRoles
        {
            public const string Customer = "Customer";
            public const string Organization = "Organization";
            public const string Tradesman = "Tradesman";
            public const string Supplier = "Supplier";
        }

        public static class AbortReason
        {
            public const string Unknown = "Unknown";
            public const string ServerFailed = "Unable to connect to server";
            public const string LimitReached = "Reached retry limit";
            public const string Null = "";
        }

        public static class Retries
        {
            public const int Zero = 0;
            public const int One = 1;
            public const int Two = 2;
            public const int Three = 3;
        }

        public static class UserApplicationConstants
        {
            public const string UserContactNumber = "+923174298370";
            public const string UserApplicationShareLinkAndroid = "https://play.google.com/store/apps/details?id=com.User.HW";
            public const string UserApplicationShareLinkIOS = "https://apps.apple.com/pk/app/hoomwork-homes/id1490813771";
            public const string UserContactUsEmail = "info@hoomwork.com";
        }
        public static class TradeApplicationConstants
        {
            public const string TradeContactNumber = "+923174298370";
            public const string TradeApplicationShareLinkAndroid = "https://play.google.com/store/apps/details?id=com.TradesmanApp.HW";
            public const string TradeApplicationShareLinkIOS = "https://apps.apple.com/pk/app/hoomwork-trades/id1490237957";
            public const string TradeContactUsEmail = "info@hoomwork.com";
        }
        public static class SupplyApplicationConstants
        {
            public const string SupplyContactNumber = "+923174298370";
            public const string SupplyApplicationShareLinkAndroid = "https://play.google.com/store/apps/details?id=com.SupplierApp.HW";
            public const string SupplyApplicationShareLinkIOS = "https://apps.apple.com/pk/app/hoomwork-supply/id1495763086";
            public const string SupplyContactUsEmail = "info@hoomwork.com";
        }

        public static class NotificationTopics
        {
            public const string AndCondition = " && ";
            public const string OrCondition = " || ";
            public const string InTopics = " in topics ";
        }

        public static class NotificationTitles
        {
            public const string NewJobPost = "New job post";
            public const string JobUpdted = "Job Update";
            public const string NewMessage = "New message received";
            public const string NewBid = "New bid";
            public const string BidUpdated = "Bid Updated";
            public const string NewCallRequest = "New call request";
            public const string BidAccepted = "Bid accepted";
            public const string BidDeclined = "Bid declined";
            public const string JobIsFinished = "Job is finished";
            public const string FeedbackRequest = "Feedback Request";
            public const string PromoteYourAd = "Promote Your Ad";
            public const string YourAdIsPosted = "Your Ad is posted successfully";
            public const string NewFeedbackReceived = "New feedback received";
            public const string BidCostUpdated = "Job cost updated";
            public const string ExpiredAd = "AD Expiry Notification";
            public const string Credit = "Referral Credit";
            public const string NewOrderPlace = "New Order Placed";
            public const string OrderStatus = "Order Status";
        }

        public static class ApplicationName
        {
            public const string TradesmanApplication = "Tradesman";
            public const string SuppliersApplication = "Suppliers";
            public const string UserApplication = "Users";
        }

        public static class SinchConstants
        {
            public const string AppKey = "8f9b715a-3c26-46c7-aac7-6d558926e7c4";
            public const string AppSecret = "pD1et51NH0yX27dkpMPxlw==";
            public const string EnvironmentHost = "clientapi.sinch.com";
        }

        public static class GenricTopics
        {
            public const string Hoomwork = "Hoomwork";
            public const string Tradesman = "Tradesman";
            public const string Supplier = "Supplier";
            public const string Customer = "Customer";
            public const string Test = "Test";
        }

        public static class FacebookConstants
        {
            public const string AppId = "216933125925491";
            public const string ApiBaseUrl = "https://graph.facebook.com";
            public const string GetAccessToken = "/oauth/access_token?client_id={{clientId}}&client_secret={{clientSecret}}&grant_type=client_credentials";
            public const string DebugToken = "/debug_token?input_token={{inputToken}}&access_token={{accessToken}}";

            public static List<string> ReadPermissions = new List<string>
            {
                "public_profile",
                "email"
            };

            public static List<string> Scopes = new List<string>
            {
                "id",
                "first_name",
                "last_name",
                "email",
                "picture.width(200)"
            };
        }

        public static class EmailAddresses
        {
            public const string InformationEmail = "Hoomwork <info@hoomwork.com>";
            public const string RegisterEmail = "register@hoomwork.com";
        }

        public static class EmailSendFor
        {
            public const string jobPostEmail = "Post New Job.";
            public const string jobfortradesman = "New job For Tradesman.";
            public const string OtpEmail = "Otp Code Email.";
            public const string PostAdEmail = "Post New Ad.";
            public const string WellcomeTradesmanEmail = "Wellcome email for tradesman.";
            public const string WellSupplierEmail = "Welcome email for supplier.";
            public const string WellcomeCustomerEmail = "Wellcome email for customer.";
            public const string NewBidEmail = "New Bid Email.";
            public const string HowToSupplierWork = "How Supplier Work.";
            public const string ContactEmail = "Contact to hoomwork team.";
        }

        public static class EmailSentForType
        {
            public const int jobPostEmail = 1;
            public const int jobfortradesman = 2;
            public const int OtpEmail = 3;
            public const int PostAdEmail = 4;
            public const int WellcomeTradesmanEmail = 5;
            public const int WellSupplierEmail = 6;
            public const int WellcomeCustomerEmail = 7;
            public const int NewBidEmail = 8;
            public const int HowToSupplierWork = 9;
            public const int ContactEmail = 10;
            public const int AdminForgotEmail = 11;
        }

        public static class Loadingmessages
        {
            public const string login = "Signing in...";
            public const string VerifyBeforeJobPost = "* Verify your account before posting a job.";
            public const string VerifyBeforeBidPost = "* Verify your account before posting a bid.";
            public const string VerifyBeforeAdPost = "* Verify your account before posting an Ad.";
            public const string genericErrorMsg = "Cannot process your request due to poor internet connection...";
            public const string InternetIssue = "Failed to connect to server. Please Check your internet connection and try again.";
            public const string ErrorMsg = "An error occure while posting your Ad.";
            public const string basicRegistration = "Validating Information...";
            public const string registrationCode = "Verifying Code...";
            public const string sendingVerificationCode = "Sending Verification Code...";
            public const string validatingAccount = "Validating Account...";
            public const string sendCode = "Sending Code...";
            public const string PostAd = "Posting Ad...";
            public const string loadAdDetails = "Loading Ad Details...";
            public const string registrationPersonalDetail = "Saving Personal Information...";
            public const string registrationBusinessDetail = "Saving Business Information...";
            public const string UpdateAds = "Updating Ad...";
            public const string loadAds = "Loading Ads...";
            public const string activeAd = "Loading ...";
            public const string inactiveAds = "Loading ...";
            public const string Notification = "Loading Notifications...";
            public const string personalDetail = "Loading Details...";
            public const string personalDetailUpdate = "Updating Personal Information...";
            public const string businessDetail = "Loading Details...";
            public const string businessDetailUpdate = "Updating Businesss Information...";
            public const string BusinessDetailsHaveBeenUpdated = "Business details have been updated successfully.";
            public const string Rating = "Loading Rating...";
            public const string PersoanlDetailsUpdated = "Personal Details are Updated...";
            public const string DuplicateOption = "duplicate option is not allowed...";

            public const string tradesmanHome = "Loading Jobs...";
            public const string jobDetailBid = "Loading Job Details...";
            public const string activeBids = "Loading...";
            public const string declindBids = "Loading...";
            public const string activeJob = "Loading Active Job Details...";
            public const string completedJob = "Loading Completed Job Details...";
            public const string getInvoice = "Loading Payments...";
            public const string makeBid = "Making Bids...";


            public const string MessageResent = "New 6-digit code has been sent";

            public const string MarketPlace = "Loading...";
            public const string loadData = "Loading...";
            public const string loadImage = "Loading Images...";
            public const string postedJob = "Loading Quotes...";
            public const string inprogressJob = "Loading Jobs...";
            public const string jobDetails = "Loading Inprogress Job Details...";
            public const string updatingJob = "Updating Job...";
            public const string deletingJob = "Deleting Job...";
            public const string deletingAd = "Deleting Ad...";
            public const string cancelingJob = "Canceling Job...";
            public const string CallbackRequest = "Callback request sent successfully";
            public const string productDetails = "Loading Product Details...";
            public const string bidtDetails = "Loading Bid Details...";
            public const string rejectBid = "Loading Rejected Bid...";
            public const string acceptBid = "Accepting Bid...";
            public const string viewProfile = "Loading Profile...";
            public const string finishedJob = "Loading Jobs...";
            public const string notificationDetails = "Loading Notification Details...";
            public const string quotationDetails = "Loading Quotation Details...";
            public const string CallRequestLimitReached = "You have reached the call request limit. You can request for call only three times a day";
            public const string ResendCode = "Code is succefully resent.";
            public const string PressAgain = "Press again to exit";
            public const string PressAgainJazzCash = "Press again to exit JazzCash";
            public const string ErrorJobFinish = "An error occured during finishing the job.";
            public const string UserDidntExist = "Specific user did not exit";

            public const string NoUser = "You are not User.";
            public const string AgeLimit = "Your age should be minimum 15 years old";


            public const string AppCrashMsg = "An error occure while using the application, Sorry for the inconvenience. We have record this, and trying our best to resolve the issue. Thanks for your patience.";
        }
    }
}