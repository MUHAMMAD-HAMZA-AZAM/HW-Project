using Hangfire;
using Hw.EmailViewModel;
using HW.CommunicationApi.Code;
using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.EmailViewModel;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;

namespace HW.CommunicationApi.Services
{
    public interface IEmailService
    {
        bool SendEmail(EmailVM emailVM, long emailId);
        bool SendEmailJobPost(PostJobEmailVM data);
        bool TradesmanBidEmail(TradesmanBidEmail data);
        void SupplierWelcomeEmail(PostJobEmailVM data);
        bool PostAdEmail(PostSupplierAdsEmail data);
        bool SendWellcomeEmail(WellcomeEmailVM wellcomeEmailVM);
        bool SendOtpEmail(EmailOTPVM emailOTPVM);
        bool SendWellcomeEmailTradesman(WellcomeEmailVM wellcomeEmailVM);
        bool NewJobPosted(NewJobPosted newJobPosted);
        bool AdminForgotPasswordAuthenticationEmail(AdminForgotEmail adminForgotEmail);
        bool SendContactEmail(ContactEmailVm contactEmail);
        bool SendFeedBackEmail(List<FeedBackEmailVM> feedBackEmailVM);
        string ReplacePlaceHolder<T>(T data, string html);
        bool SendInformationEmail(EmailVM emailVM, long emailId);
        long SaveEmail(Email email);
        int NumberOfRetries(long emailId);
        void SendScheduledEmail();
        void SendScheduledOtpEmail();
        bool SendLoginConfirmationEmail(ContactEmailVm contactEmail);
        List<CallRequestVM> RequestCallBacksList();
        Response DeleteRequestCaller(long requestCallerId);
        Response SendTestEmail();
    }
    public class EmailService : IEmailService
    {
        bool result = false;
        private readonly GenericAppConfig genericAppConfig;

        //private readonly SmtpConfig smtpConfig;

        private readonly AwsSmtpConfig _awsSmtpConfig;
        private readonly IExceptionService Exc;
        public readonly ApiConfig _apiConfig;
        public readonly IUnitOfWork uow;
        public EmailService(GenericAppConfig genericAppConfig, AwsSmtpConfig awsSmtpConfig, /*SmtpConfig smtpConfig,*/ IExceptionService Exc, ApiConfig apiConfig, IUnitOfWork _unitOfWork)
        {
            this.genericAppConfig = genericAppConfig;
            //this.smtpConfig = smtpConfig;
            _awsSmtpConfig = awsSmtpConfig;
            _apiConfig = apiConfig;
            this.Exc = Exc;
            uow = _unitOfWork;
        }
        public bool PostAdEmail(PostSupplierAdsEmail data)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/PostAdEmail.html", data);
                EmailVM emailVM = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { data.email_ },
                    Subject = "Posted Ad Details."
                };

                data.Email.Subject = emailVM.Subject;
                data.Email.RecieverEmailId = data.email_;
                data.Email.SenderEmailId = EmailAddresses.InformationEmail;
                data.Email.CreatedOn = DateTime.Now;
                data.Email.Message = message;
                data.Email.SentFor = EmailSentForType.PostAdEmail;
                data.Email.EmailId = SaveEmail(data.Email);
                if (data.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, data.Email.EmailId));
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool SendEmail(EmailVM emailVM, long emailId)
        {
            try
            {
                if (emailVM.EmailAddresses?.Count > 0)
                {
                    var smtpClient = new SmtpClient
                    {
                        Host = _awsSmtpConfig.Host, // set your SMTP server name here
                        Port = _awsSmtpConfig.Port,
                        EnableSsl = _awsSmtpConfig.EnableSsl,
                        Credentials = new NetworkCredential(_awsSmtpConfig.Username, _awsSmtpConfig.Password)
                    };

                    using (var message = new MailMessage(_awsSmtpConfig.From, string.Join(",", emailVM.EmailAddresses), emailVM.Subject, emailVM.Body))
                    {
                        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailVM.Body, Encoding.UTF8, "text/html"));
                        message.IsBodyHtml = true;
                        smtpClient.Send(message);
                        UpdateSentEmail(emailId);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateFailedEmail(emailId);
                Exc.AddErrorLog(ex);
                return false;
            }
            return true;
        }
        public void SendScheduledOtpEmail()
        {
            var faildeEmails = uow.Repository<Email>().GetAll().Where(x => x.IsSend == false && x.Retries < 4 && x.SentFor == EmailSentForType.OtpEmail).ToList();
            foreach (var item in faildeEmails)
            {
                var email = new EmailVM
                {
                    EmailAddresses = new List<string> { item.RecieverEmailId },
                    Body = item.Message,
                    Subject = item.Subject,
                    Bcc = new List<string> { item.BccEmail }
                };
                SendInformationEmail(email, item.EmailId);
            }
        }
        public bool SendInformationEmail(EmailVM emailVM, long emailId)
        {

            if (emailId > 0)
            {
                try
                {
                    if (emailVM.EmailAddresses?.Count > 0 && !string.IsNullOrWhiteSpace(string.Join(",", emailVM.EmailAddresses)))
                    {
                        var smtpClient = new SmtpClient
                        {
                            Host = _awsSmtpConfig.Host, // set your SMTP server name here
                            Port = _awsSmtpConfig.Port,
                            EnableSsl = _awsSmtpConfig.EnableSsl,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(_awsSmtpConfig.Username, _awsSmtpConfig.Password)
                        };
                        if (emailVM?.Bcc?.Count > 0)
                        {
                            foreach (var item in emailVM.Bcc)
                            {
                                using (var message = new MailMessage(_awsSmtpConfig.From, item, emailVM.Subject, emailVM.Body))
                                {
                                    message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailVM.Body, Encoding.UTF8, "text/html"));
                                    message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                                    message.IsBodyHtml = true;

                                    Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(message)));

                                    smtpClient.Send(message);
                                }
                            }
                            UpdateSentEmail(emailId);
                        }
                        else
                        {
                            using (var message = new MailMessage(_awsSmtpConfig.From, string.Join(",", emailVM.EmailAddresses), emailVM.Subject, emailVM.Body))
                            {
                                AlternateView av1 = AlternateView.CreateAlternateViewFromString(emailVM.Body, null, System.Net.Mime.MediaTypeNames.Text.Html);
                                
                                //for images
                                //LinkedResource logo = new LinkedResource("images/logo.png");
                                //LinkedResource fb = new LinkedResource("images/fb.png");
                                //LinkedResource google_play = new LinkedResource("images/google_play.png");
                                //LinkedResource twitter = new LinkedResource("images/twitter.png");
                                //LinkedResource utube = new LinkedResource("images/utube.png");
                                //fb.ContentId = "fb";
                                //logo.ContentId = "logo";
                                //google_play.ContentId = "google_play";
                                //twitter.ContentId = "twitter";
                                //utube.ContentId = "utube";

                                //av1.LinkedResources.Add(logo);
                                //av1.LinkedResources.Add(fb);
                                //av1.LinkedResources.Add(google_play);
                                //av1.LinkedResources.Add(twitter);
                                //av1.LinkedResources.Add(utube);

                                message.AlternateViews.Add(av1);

                                message.IsBodyHtml = true;
                                message.From = new MailAddress(_awsSmtpConfig.From, "Hoomwork");
                                smtpClient.Send(message);
                                UpdateSentEmail(emailId);
                            }
                        }
                    }
                    _awsSmtpConfig.From = EmailAddresses.InformationEmail;
                    // smtpConfig.Password = "#FirstTime441q";

                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                    UpdateFailedEmail(emailId);
                }
            }
            return result;
        }
        public void SendScheduledEmail()
        {
            var faildeEmails = uow.Repository<Email>().GetAll().Where(x => x.IsSend == false && x.Retries < 4).ToList();
            foreach (var item in faildeEmails)
            {
                var email = new EmailVM
                {
                    EmailAddresses = new List<string> { item.RecieverEmailId },
                    Body = item.Message,
                    Subject = item.Subject,
                    Bcc = new List<string> { item.BccEmail }
                };
                SendInformationEmail(email, item.EmailId);
            }
        }

        public bool SendFeedBackEmail(List<FeedBackEmailVM> feedBackEmailVM)
        {

          foreach (var item in feedBackEmailVM)
          {
              try
              {
                  var email = new EmailVM
                  {
                    EmailAddresses = new List<string> { item.EmailAddress },
                    Body = item.Message,
                    Subject = item.Subject
                  };

                  if (!string.IsNullOrWhiteSpace(string.Join(",", item.EmailAddress)))
                  {
                    var smtpClient = new SmtpClient
                    {
                      Host = _awsSmtpConfig.Host, // set your SMTP server name here
                      Port = _awsSmtpConfig.Port,
                      EnableSsl = _awsSmtpConfig.EnableSsl,
                      UseDefaultCredentials = false,
                      Credentials = new NetworkCredential(_awsSmtpConfig.Username, _awsSmtpConfig.Password)
                    };
                    using (var message = new MailMessage(_awsSmtpConfig.From, item.EmailAddress, item.Subject, item.Message))
                    {
                      message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(item.Message, Encoding.UTF8, "text/html"));
                      message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                      message.IsBodyHtml = true;

                     // Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(message)));

                      smtpClient.Send(message);
                    }
                  }
              }
              catch (Exception ex)
              {
                Exc.AddErrorLog(ex);
                return false;
              }
          }
          return true;
        }
        public bool SendContactEmail(ContactEmailVm contactEmail)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/ContactEmail.html", contactEmail);
                EmailVM emailVM = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { "info@hoomwork.com" },
                    Subject = contactEmail.Subject
                };

                contactEmail.Email.Subject = emailVM.Subject;
                contactEmail.Email.RecieverEmailId = contactEmail.EmailAddress;
                contactEmail.Email.SenderEmailId = EmailAddresses.InformationEmail;
                contactEmail.Email.CreatedOn = DateTime.Now;
                contactEmail.Email.Message = message;
                contactEmail.Email.SentFor = EmailSentForType.ContactEmail;
                contactEmail.Email.EmailId = SaveEmail(contactEmail.Email);
                //if (contactEmail.Email.EmailId > 0)
                //{
                //    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, contactEmail.Email.EmailId));
                //}
                SendInformationEmail(emailVM, contactEmail.Email.EmailId);
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool SendEmailJobPost(PostJobEmailVM data)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/JobPostEmail.html", data);
                EmailVM emailVM = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { data.email_ },
                    Subject = "job's been posted."
                };

                data.Email.Subject = emailVM.Subject;
                data.Email.RecieverEmailId = data.email_;
                data.Email.SenderEmailId = EmailAddresses.InformationEmail;
                data.Email.CreatedOn = DateTime.Now;
                data.Email.Message = message;
                data.Email.SentFor = EmailSentForType.jobPostEmail;
                data.Email.EmailId = SaveEmail(data.Email);
                if (data.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, data.Email.EmailId));
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return result;
        }
        public void SupplierWelcomeEmail(PostJobEmailVM data)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/SupplierWelcomeEmail.html", data);
                EmailVM emailVM = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { data.email_ },
                    Subject = "Welcome to HoomWork."
                };
                data.Email.Subject = emailVM.Subject;
                data.Email.RecieverEmailId = data.email_;
                data.Email.SenderEmailId = EmailAddresses.InformationEmail;
                data.Email.CreatedOn = DateTime.Now;
                data.Email.Message = message;
                data.Email.SentFor = EmailSentForType.WellSupplierEmail;
                data.Email.EmailId = SaveEmail(data.Email);
                if (data.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, data.Email.EmailId));
                }
                SupplierHowItWorks(data);
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
        }
        public bool SupplierHowItWorks(PostJobEmailVM data)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/HowSupplierWork.html", data);
                EmailVM emailVM1 = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { data.email_ },
                    Subject = "How It Works."
                };
                data.Email.Subject = emailVM1.Subject;
                data.Email.RecieverEmailId = data.email_;
                data.Email.SenderEmailId = EmailAddresses.InformationEmail;
                data.Email.CreatedOn = DateTime.Now;
                data.Email.Message = message;
                data.Email.SentFor = EmailSentForType.HowToSupplierWork;
                data.Email.EmailId = SaveEmail(data.Email);
                if (data.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM1, data.Email.EmailId));
                }
                return true;
                //return SendInformationEmail(emailVM1, 0);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool TradesmanBidEmail(TradesmanBidEmail data)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/TradesmanBidEmail.html", data);
                EmailVM emailVM = new EmailVM
                {
                    Body = message,
                    EmailAddresses = new List<string> { data.emailTradesman },
                    Subject = "Bid Details."
                };
                data.Email.Subject = emailVM.Subject;
                data.Email.RecieverEmailId = data.emailTradesman;
                data.Email.SenderEmailId = EmailAddresses.InformationEmail;
                data.Email.CreatedOn = DateTime.Now;
                data.Email.Message = message;
                data.Email.SentFor = EmailSentForType.NewBidEmail;
                data.Email.EmailId = SaveEmail(data.Email);
                if (data.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, data.Email.EmailId));
                }
                return true;
                //return SendInformationEmail(emailVM, 0);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool SendWellcomeEmail(WellcomeEmailVM wellcomeEmailVM)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/WellcomeEmail.html", wellcomeEmailVM);
                var emailVM = new EmailVM()
                {
                    Body = message,
                    Subject = "Welcome to Hoomwork",
                    EmailAddresses = new List<string> { wellcomeEmailVM.UserEmail }
                };
                wellcomeEmailVM.Email.Subject = emailVM.Subject;
                wellcomeEmailVM.Email.RecieverEmailId = wellcomeEmailVM.UserEmail;
                wellcomeEmailVM.Email.SenderEmailId = EmailAddresses.InformationEmail;
                wellcomeEmailVM.Email.CreatedOn = DateTime.Now;
                wellcomeEmailVM.Email.Message = message;
                wellcomeEmailVM.Email.SentFor = EmailSentForType.WellcomeCustomerEmail;
                wellcomeEmailVM.Email.EmailId = SaveEmail(wellcomeEmailVM.Email);
                if (wellcomeEmailVM.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, wellcomeEmailVM.Email.EmailId));
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool SendOtpEmail(EmailOTPVM emailOTPVM)
        {
            try
            {
                emailOTPVM.FAQs = GetFAQByRole(emailOTPVM.Role);
                emailOTPVM.Terms = GetTermsByRole(emailOTPVM.Role);
                emailOTPVM.MyAccount = GetMyAccountByRole(emailOTPVM.Role);
                emailOTPVM.GooglePlayStoreUrl = GetGooglePlayStoreAppLinkByRole(emailOTPVM.Role);
                var message = RenderViewToString("EmailHtml/OTPEmail.html", emailOTPVM);
                var emailVM = new EmailVM()
                {
                    Body = message,
                    Subject = "Hoomwork verification code",
                    EmailAddresses = new List<string> { emailOTPVM.UserEmail }
                };
                emailOTPVM.Email.Subject = emailVM.Subject;
                emailOTPVM.Email.RecieverEmailId = emailOTPVM.UserEmail;
                emailOTPVM.Email.SenderEmailId = EmailAddresses.InformationEmail;
                emailOTPVM.Email.CreatedOn = DateTime.Now;
                emailOTPVM.Email.Message = message;
                emailOTPVM.Email.SentFor = EmailSentForType.OtpEmail;
                emailOTPVM.Email.EmailId = SaveEmail(emailOTPVM.Email);

                Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(emailOTPVM)));

                if (emailOTPVM.Email.EmailId > 0)
                {
                    //BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, emailOTPVM.Email.EmailId));
                    SendInformationEmail(emailVM, emailOTPVM.Email.EmailId);
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool SendWellcomeEmailTradesman(WellcomeEmailVM wellcomeEmailVM)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/SendWellcomeEmailTradesman.html", wellcomeEmailVM);
                var emailVM = new EmailVM()
                {
                    Body = message,
                    Subject = "Welcome to Hoomwork",
                    EmailAddresses = new List<string> { wellcomeEmailVM.UserEmail }
                };
                wellcomeEmailVM.Email.Subject = emailVM.Subject;
                wellcomeEmailVM.Email.RecieverEmailId = wellcomeEmailVM.UserEmail;
                wellcomeEmailVM.Email.SenderEmailId = EmailAddresses.InformationEmail;
                wellcomeEmailVM.Email.CreatedOn = DateTime.Now;
                wellcomeEmailVM.Email.Message = message;
                wellcomeEmailVM.Email.SentFor = EmailSentForType.WellcomeTradesmanEmail;
                wellcomeEmailVM.Email.EmailId = SaveEmail(wellcomeEmailVM.Email);
                if (wellcomeEmailVM.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, wellcomeEmailVM.Email.EmailId));
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public bool AdminForgotPasswordAuthenticationEmail(AdminForgotEmail adminForgotEmail)
        {
            try
            {
                var message = RenderViewToString("EmailHtml/AdminForgotPassword.html", adminForgotEmail);
                var emailVM = new EmailVM()
                {
                    Body = message,
                    Subject = "Reset Password",
                    EmailAddresses = new List<string> { adminForgotEmail.RecivereEmail }
                };
                adminForgotEmail.Email.Subject = emailVM.Subject;
                adminForgotEmail.Email.RecieverEmailId = adminForgotEmail.RecivereEmail;
                adminForgotEmail.Email.SenderEmailId = EmailAddresses.InformationEmail;
                adminForgotEmail.Email.CreatedOn = DateTime.Now;
                adminForgotEmail.Email.Message = message;
                adminForgotEmail.Email.SentFor = EmailSentForType.AdminForgotEmail;
                adminForgotEmail.Email.EmailId = SaveEmail(adminForgotEmail.Email);
                if (adminForgotEmail.Email.EmailId > 0)
                {
                    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, adminForgotEmail.Email.EmailId));
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public bool NewJobPosted(NewJobPosted newJobPosted)
        {
            //bool result = false;
            try
            {
                var message = RenderViewToString("EmailHtml/NewJobPosted.html", newJobPosted);
                //newJobPosted.Email.BccEmail = "zeeshan.haider@datamagnetics.com,zeeshan.khakhi@gmail.com,abdulrehmankhan333@gmail.com,hwuser34@gmail.com,datamagneticstechnologies@gmail.com";
                //List<string> emailstring = newJobPosted.Email.BccEmail.Split(new char[] { ',' }).ToList();
                var emailVM = new EmailVM()
                {
                    Body = message,
                    Subject = "New Job Posted",
                    Bcc = new List<string>()
                };
                emailVM.Bcc = newJobPosted.TradesmanEmail;
                newJobPosted.Email.Subject = emailVM.Subject;
                newJobPosted.Email.BccEmail = string.Join(",", newJobPosted.TradesmanEmail) + "";
                newJobPosted.Email.SenderEmailId = EmailAddresses.InformationEmail;
                newJobPosted.Email.CreatedOn = DateTime.Now;
                newJobPosted.Email.Message = message;
                newJobPosted.Email.SentFor = EmailSentForType.jobfortradesman;
                newJobPosted.Email.EmailId = SaveEmail(newJobPosted.Email);
                if (newJobPosted.Email.EmailId > 0)
                {
                    //if (!string.IsNullOrEmpty(emailVM.Bcc))
                    //{
                    //    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, newJobPosted.Email.EmailId));
                    //}
                    if (emailVM?.Bcc?.Count > 0)
                    {
                        BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, newJobPosted.Email.EmailId));
                    }
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return result;
        }
        #region email db settings
        public bool UpdateSentEmail(long id)
        {
            try
            {
                var email = uow.Repository<Email>().GetById(id);

                email.IsSend = true;
                email.SentOn = DateTime.Now;
                uow.Repository<Email>().Update(email);
                uow.Save();
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
            }
            return true;
        }
        public bool UpdateFailedEmail(long id)
        {
            //bool result = false;
            try
            {
                var email = uow.Repository<Email>().GetById(id);
                if (email.Retries < 4 && email.IsSend == false)
                {
                    email.Retries = ++email.Retries;
                    email.RetriedDate = email.NextRetriedDate;
                    email.NextRetriedDate = DateTime.Now;
                    uow.Repository<Email>().Update(email);
                    uow.Save();
                }
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
            }
            return result;
        }
        public long SaveEmail(Email email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email.RecieverEmailId))
                {
                    email.Subject = "Email is not provided.";
                }
                if (email != null && !string.IsNullOrWhiteSpace(email.Subject))
                {
                    uow.Repository<Email>().Add(email);
                    uow.Save();
                }
                else
                {
                    Exc.AddErrorLog(new Exception(email + ""));
                }
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
            }
            return email.EmailId;
        }
        public int NumberOfRetries(long emailId)
        {
            return uow.Repository<Email>().GetAll().Where(s => s.EmailId == emailId).Select(s => s.Retries.Value).FirstOrDefault();
        }
        #endregion
        #region Get Url over role
        public string GetFAQByRole(string role)
        {
            string url = "";
            switch (role)
            {
                case "Customer":
                    url = "HWUser/Home/UserFAQs";
                    break;
                case "Tradesman":
                    url = "HWTradesman/Home/TradesmanFAQs";
                    break;
                case "Supplier":
                    url = "HWSupplier/Home/SupplierFAQs";
                    break;
                default:
                    break;
            }
            return url;
        }
        public string GetGooglePlayStoreAppLinkByRole(string role)
        {
            string url = "";
            switch (role)
            {
                case "Customer":
                    url = "https://play.google.com/apps/testing/com.User.HW";
                    break;
                case "Tradesman":
                    url = "https://play.google.com/apps/testing/com.TradesmanApp.HW";
                    break;
                case "Supplier":
                    url = "https://play.google.com/apps/testing/com.SupplierApp.HW";
                    break;
                default:
                    break;
            }
            return url;
        }
        public string GetMyAccountByRole(string role)
        {
            string url = "";
            switch (role)
            {
                case "Customer":
                    url = "HWUser/Home/Index";
                    break;
                case "Tradesman":
                    url = "HWTradesman/Home/Index";
                    break;
                case "Supplier":
                    url = "HWSupplier/Home/Index";
                    break;
                default:
                    break;
            }
            return url;
        }
        public string GetTermsByRole(string role)
        {
            string url = "";
            switch (role)
            {

                case "Customer":
                    url = "HWUser/Home/UserAgreement";
                    break;
                case "Tradesman":
                    url = "HWTradesman/Home/TradesmanAgreement";
                    break;
                case "Supplier":
                    url = "HWSupplier/Home/SupplierAgreement";
                    break;
                default:
                    break;
            }
            return url;
        }
        #endregion
        #region  Get String From Html
        public string RenderViewToString<T>(string filePath, T data)
        {
            string fileContent = "";
            if (!string.IsNullOrEmpty(filePath))
            {
                fileContent = ReplacePlaceHolder(data, File.ReadAllText(filePath));
            }
            return fileContent;
        }

        public string ReplacePlaceHolder<T>(T data, string html)
        {
            try
            {
                if (data != null)
                {
                    foreach (var prop in data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        html = html.Replace("{" + prop.Name + "}", $"{prop.GetValue(data, null)}");
                    }
                    html = html.Replace("{baseUrl}", $"{_apiConfig.BaseUrl}");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return html;

        }
        public bool SendLoginConfirmationEmail(ContactEmailVm contactEmail)
        {
            try
            {
                var message = "test email";
                //var message = RenderViewToString("EmailHtml/ContactEmail.html", contactEmail);
                EmailVM emailVM = new EmailVM
                {
                    Body = "",
                    EmailAddresses = new List<string> { "info@hoomwork.com" },
                    Subject = "testing"
                };
                Email email = new Email();
                email.Subject = "User Confirmation";
                email.RecieverEmailId = "bilalafzal0344@yahoo.com";
                email.SenderEmailId = "info@hoomwork.com";
                email.CreatedOn = DateTime.Now;
                email.Message = message;
                email.SentFor = 10;
                email.EmailId = SaveEmail(email);
                //if (contactEmail.Email.EmailId > 0)
                //{
                //    BackgroundJob.Enqueue(() => SendInformationEmail(emailVM, contactEmail.Email.EmailId));
                //}
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public List<CallRequestVM> RequestCallBacksList()
        {
            List<CallRequestVM> callRequestVMs = new List<CallRequestVM>();
            try
            {
                SqlParameter[] sqlParameters = { };

                callRequestVMs = uow.ExecuteReaderSingleDS<CallRequestVM>("SP_GetRequestCallBackList", sqlParameters);

                return callRequestVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CallRequestVM>();
            }
        }

        public Response DeleteRequestCaller(long requestCallerId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                 new SqlParameter("requestCallerId",requestCallerId)
                };
                  uow.ExecuteReaderSingleDS<Response>("Sp_DeleteSelectedRequestCallBack", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Request Caller Deleted  successfully!";
                return response;
            }

            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public Response SendTestEmail()
        {
            Response response = new Response();
            int result = 0;
            MailMessage message = new MailMessage();
            string SmtpHost = _awsSmtpConfig.Host;

            string text = "";
            text += "Test email for testing AWS";

            message.To.Add(new MailAddress("nazim.hussain@datamagnetics.com"));

            message.Subject = "Test Email";
            message.From = new MailAddress(_awsSmtpConfig.From);
            try
            {
                message.IsBodyHtml = true;
                message.Body = text;
                if (result >= 0)
                {

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost, _awsSmtpConfig.Port);
                    // Pass SMTP credentials
                    smtp.Credentials =
                        new NetworkCredential(_awsSmtpConfig.Username, _awsSmtpConfig.Password);

                    // Enable SSL encryption
                    smtp.EnableSsl = _awsSmtpConfig.EnableSsl;
                    smtp.Send(message);

                    result = 1;
                    response.Status = ResponseStatus.OK;
                    response.Message = "Email sent successfuly";
                }
            }
            catch (Exception oExp)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Email failed";
                result = -1;
            }
            finally
            {
                message.Dispose();
            }
            response.ResultData = result;
            return response;
        }
        #endregion
    }
}
