"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StatusCode = exports.TransectionTypes = exports.Payments = exports.Orders = exports.GLAccounts = exports.GLAccountTypes = exports.escalateIssueStatus = exports.CSJobStatus = exports.BidStatus = exports.RegistrationErrorMessages = exports.ReportDatesValidation = exports.resetPassword = exports.forgotPassword = exports.LoginValidation = exports.httpStatus = exports.CampaignTypes = exports.SupplierRole = exports.aspNetUserRoles = exports.loginsecurity = void 0;
exports.loginsecurity = {
    Role: "Admin",
    TRole: "Tradesman",
    ORole: "Oragnization",
    SRole: "Supplier",
    CRole: "Customer",
};
exports.aspNetUserRoles = {
    TRole: "1",
    ORole: "2",
    SRole: "4",
    CRole: "3",
    Admin: "5"
};
exports.SupplierRole = {
    localSupplier: "1",
    hoomworkSupplier: "2"
};
exports.CampaignTypes = {
    Promotion: "1",
    Advertisement: "2"
};
exports.httpStatus = {
    Ok: "200",
    loginError: "invalid email address/phone number or password."
};
exports.LoginValidation = {
    emailOrPhoneNumber: "Please enter email address or phone Number.",
    password: "Please enter password.",
    unauthorizedUser: "You are Un-Authorized User.",
    InvalidCredentials: "Invalid Credentials.",
    Invalidrecaptcha: "Please enter captcha.",
    BlockedUSer: "This user is blocked."
};
exports.forgotPassword = {
    forgotPasswordEmailError: "please enter email address.",
    unauthorizedUser: "Can't Send Email.",
    unauthorizedErrormessage: "You are unauthorized user."
};
exports.resetPassword = {
    password: "Please enter Password.",
    confirmPassword: "Please enter Confirm Password.",
    passwordMatch: "Passwords not match."
};
exports.ReportDatesValidation = {
    startDateErrorMessage: "Please enter start date.",
    endDateErrorMessage: "Please enter end date.",
};
exports.RegistrationErrorMessages = {
    passwordErrorMessage: "Please enter password.",
    passwordMinLenghtErrorMessage: "Please enter minimum 6 charcters",
    phonenumberErrorMessage: "Please enter Phone Number.",
    emailErrorMessage: "Please enter Email.",
    UserNameErrorMessage: "Please enter User Name.",
    UserSecurityRoleMessage: "Please select security role.",
    emailAlreadyRegisteredErrorMessage: "User Already Registered with this Email.",
    mobileNumberAlreadyRegisteredErrorMessage: "User Already Registered with this Mobile Number"
};
exports.BidStatus = {
    Active: 1,
    Declined: 2,
    Completed: 3,
    Accepted: 4,
    Pending: 5,
    Urgent: 6,
    Deleted: 7,
};
exports.CSJobStatus = {
    Callnotpicked: 1,
    Slotbooking: 2,
    Waitingduetoissue: 3,
    Notinterested: 4,
    Outofstation: 5
};
exports.escalateIssueStatus = {
    Pending: 0,
    Approve: 1
};
exports.GLAccountTypes = {
    AccountReceiveables: 1,
    AccountPayable: 2,
    Assets: 3
};
exports.GLAccounts = {
    Receiveables: 3,
    Payable: 2,
    Assets: 1
};
exports.Orders = {
    Received: 1,
    Inprogress: 2,
    Delivered: 3,
    Completed: 4,
    Declined: 5,
    PackedAndShipped: 6,
    Cancelled: 7
};
exports.Payments = {
    Pending: 1,
    Posted: 2,
    Inprogress: 3,
    Declined: 4,
    Completed: 5
};
exports.TransectionTypes = {
    TTransection: "TopUp",
    SDTransection: "Self Deposit",
    RTransection: "Referral",
    WTransection: "WithDraw",
    PBWTransectioin: "Paid By Wallet",
};
exports.StatusCode = {
    OK: 200,
    Error: 505,
    failure: 500,
    Restricted: 403,
    partialContent: 206,
    Conflict: 409
};
//# sourceMappingURL=enums.js.map