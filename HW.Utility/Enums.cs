using System;

namespace HW.Utility
{
    public enum CodeStatus
    {
        Open = 1,
        Closed = 2,
        Cancelled = 3
    }

    public enum ResponseStatus
    {
        OK = 200,
        Error = 400,
        Restrected = 403
    }

    public enum LoggingEvents
    {
        LIST_ITEMS,
        GET_ITEM,
        INSERT_ITEM,
        UPDATE_ITEM,
        DELETE_ITEM
    }

    public enum Rating
    {
        Positive = 5,
        Neutral = 2,
        Negitive = 1
    }

    public enum AdStatus
    {
        Spotlight = 1,
        Featured = 3,
        Regular = 4
    }

    public enum Role
    {
        Tradesman = 1,
        Organization = 2,
        Customer = 3,
        Supplier = 4,
        Admin = 5
    }

    public enum AccountType
    {
        Assets = 1,
        AccountPayables = 2,
        AccountReceiveables = 3
    }
    public static class UserRoles
    {
        public const string Tradesman = "Tradesman";
        public const string Organization = "Organization";
        public const string Customer = "Customer";
        public const string Supplier = "Supplier";
        public const string Admin = "Admin";
        public const string Anonymous = "Anonymous";
    }

    public enum MessageStatus
    {
        Sent = 1,
        Pending = 2,
        Failed = 3,
        Recieved = 4
    }

    public enum BidStatus
    {
        Active = 1,
        Declined = 2,
        Completed = 3,
        Accepted = 4,
        Pending = 5,
        Urgent=6,
        Deleted = 7,
        StandBy = 8
    }

    public enum DisputeStatusEnums
    {
        Open = 1,
        Closed = 2,
        Cancelled = 3
    }

    public enum TargetDatabase
    {
        Customer = 1,
        Tradesman = 2,
        Supplier = 3
    }

    public enum EmailTypes
    {
        PostNewJob = 1,
        NewjobForTradesman = 2,
        OtpCodeEmail = 3,
        PostNewAd = 4,
        Wellcomeemailfortradesman = 5,
        Welcomeemailforsupplier = 6,
        Wellcomeemailforcustomer = 7,
        NewBidEmail = 8,
        HowSupplierWork = 9,
        Contacttohoomworkteam = 10
    }

    public enum CallTypeEnum
    {
        Received = 1,
        Missed = 2,
        Canceled = 3,
        Outgoing = 4,
        CallRequest = 5
    }

    public enum NotificationType
    {
        NewJobPost = 1,
        JobUpdted = 2,
        NewMessage = 3,
        NewBid = 4,
        BidUpdated = 5,
        NewCallRequest = 6,
        BidAccepted = 7,
        BidDeclined = 8,
        JobIsFinished = 9,
        FeedbackRequest = 10,
        PromoteYourAd = 11,
        YourAdIsPosted = 12,
        NewFeedbackReceived = 13,
        BidCostUpdated = 14,
        ExpiredAd = 15,
        NewOrderPlace = 16,
        OrderStatus = 17
    }

    public enum ReturnNotificationCallRequest
    {
        Success = 1,
        RetriesLimitReached = 2,
        ErrorOccure = 3,
        ExceptionOccur = 4,
        CustomerDontExist = 5,
        TradesmanDontExist = 6,
    }
    public enum Duration
    {
        week = 7,
        twoWeek = 14,
        threeWeek = 21,
        fourWeek = 30,
        twoMonth = 60,
        fourMonth = 120,
        eightMonth = 240,
        Year = 360
    }

    public enum PaymentMethods
    {
        JazzCash = 1,
        EasyPaisa = 3,
        Visa = 5,
        DirectPayment = 6
    }
    public enum PaymentStatus
    {
        JazzCash = 1,
        EasyPaisa = 2,
        DirectPayment = 3,
        PaidByWallet = 4
    }
    public enum CSJobStatus
    {
        Callnotpicked=1,
        Slotbooking=2,
        Waitingduetoissue=3,
        Notinterested=4,
        Outofstation=5
    }
    public enum WithdravalPaymentStatus
    {
        Pending = 1,
        Posted = 2,
        Inprogress = 3,
        Declined = 4,
        Completed = 5,
    }
    public enum EOrderStatus
    {
        Received = 1,
        Inprogress = 2,
        Delievred = 3,
        Completed = 4,
        Declined = 5,
        PackedAndShipped = 6,
        Cancelled = 7
    }
    public enum TraxServiceType
    {
        Regular = 1,
        Replacement = 2,
        TryAndBuy = 3
    }
    public enum TraxShippingMode
    {
        Rush = 1,
        Saverplus = 2,
        Swift = 3,
        Same_day = 4
    }
    public enum TraxStatusCode
    {
        OK = 0,
        Error = 1
    }
    public enum TrackingStatusType
    {
        ShipperRelatedTracking = 0,
        GeneralTracking = 1
    }
    public static class TraxPrefixes {
        public const string OrderPrefix = "HOOMWORK-";
    }
    public static class AspnetUserRoles
    {
        public const string CRole = "Customer";
        public const string TRole = "Tradesman";
        public const string ORole = "Organization";
        public const string SRole = "Supplier";
    }

}

