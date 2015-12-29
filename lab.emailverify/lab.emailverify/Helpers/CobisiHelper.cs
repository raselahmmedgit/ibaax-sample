using Cobisi.EmailVerify;
using lab.emailverify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.emailverify.Helpers
{
    public static class CobisiHelper
    {

        public static EmailVerificationViewModel EmailVerifyByLevel(string primaryEmailAddress, int verificationLevel, bool toSave = false)
        {
            EmailVerificationViewModel emailVerificationInfoViewModel = new EmailVerificationViewModel();
            try
            {
                VerificationLevel level = VerificationLevel.Syntax;
                switch (verificationLevel)
                {
                    case 1: // Syntax
                        level = VerificationLevel.Syntax;
                        break;
                    case 2: // disposal / free
                        level = VerificationLevel.DeaMailExchanger;
                        break;
                    case 3: // Dns
                        level = VerificationLevel.Dns;
                        break;
                    case 4: // Smtp
                        level = VerificationLevel.Smtp;
                        break;
                    case 5: // Mailbox
                        level = VerificationLevel.Mailbox;
                        break;
                    case 6:// CatchAll
                        level = VerificationLevel.CatchAll;
                        break;
                }

                emailVerificationInfoViewModel = GetEmailVerificationReport(primaryEmailAddress);

                if (toSave)
                {
                    emailVerificationInfoViewModel.EmailAddress = emailVerificationInfoViewModel.EmailAddress;
                    emailVerificationInfoViewModel.IsSynctexValidated = emailVerificationInfoViewModel.IsSynctexValidated;
                    emailVerificationInfoViewModel.IsRoleAccountValidated = emailVerificationInfoViewModel.IsRoleAccountValidated;
                    emailVerificationInfoViewModel.IsIspSpecificSyntaxValidated = emailVerificationInfoViewModel.IsIspSpecificSyntaxValidated;
                    emailVerificationInfoViewModel.IsDEADomainValidated = emailVerificationInfoViewModel.IsDEADomainValidated;
                    emailVerificationInfoViewModel.IsDNSValidated = emailVerificationInfoViewModel.IsDNSValidated;
                    emailVerificationInfoViewModel.IsSMTPValidated = emailVerificationInfoViewModel.IsSMTPValidated;
                    emailVerificationInfoViewModel.IsMailboxValidated = emailVerificationInfoViewModel.IsMailboxValidated;
                    emailVerificationInfoViewModel.IsCatchAllValidated = emailVerificationInfoViewModel.IsCatchAllValidated;
                    emailVerificationInfoViewModel.IsPositive = emailVerificationInfoViewModel.IsPositive;
                    emailVerificationInfoViewModel.IsVerified = emailVerificationInfoViewModel.IsVerified;
                    emailVerificationInfoViewModel.LastStatus = emailVerificationInfoViewModel.LastStatus;

                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
                if (!ex.Message.ToString().Contains("Your free 30 days trial version of Cobisi EmailVerify for .NET is expired"))
                {
                    emailVerificationInfoViewModel.LastStatus = ex.Message;
                }
            }

            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReport(string primaryEmailAddress)
        {
            EmailVerificationViewModel emailVerificationInfoViewModel = new EmailVerificationViewModel();
            try
            {
                List<VerificationLevel> verificationLevelList = new List<VerificationLevel>
                {
                    VerificationLevel.Syntax,
                    VerificationLevel.RoleAccount,
                    VerificationLevel.IspSpecificSyntax,
                    VerificationLevel.DeaDomain,
                    VerificationLevel.Dns,
                    VerificationLevel.DeaMailExchanger,
                    VerificationLevel.Smtp,
                    VerificationLevel.Mailbox,
                    VerificationLevel.CatchAll
                };

                foreach (var verificationLevel in verificationLevelList)
                {
                    #region Code

                    //start 
                    LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                    VerificationEngine engine = new VerificationEngine();
                    engine.VerificationLevelCompleted += (sender, e) =>
                    {
                        switch (e.Level.Name.ToUpper())
                        {
                            case "SYNTAX":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                                break;

                            case "ROLEACCOUNT":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                                break;

                            case "ISPSPECIFICSYNTAX":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                                break;

                            case "DEADOMAIN":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                                break;

                            case "DNS":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                                break;

                            case "DEAMAILEXCHANGER":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                                break;

                            case "SMTP":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                                break;

                            case "MAILBOX":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                                break;

                            case "CATCHALL":
                                if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                                break;
                        }
                    };
                    var result = engine.Run(primaryEmailAddress, verificationLevel);
                    if (result != null)
                    {
                        emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                        emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                        emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                        emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                        emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                        emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                        emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                        emailVerificationInfoViewModel.Comments = result.Comments;
                        emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                    }
                    emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                    if (result.Result != null)
                    {
                        if (result.Result.AllLevels != null)
                        {
                            int index = 0;
                            foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                            {
                                EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                                newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;

                                int indexResult = 0;

                                foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                                {
                                    if (index == indexResult)
                                    {
                                        newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                        if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                        {
                                            emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                                                                        VerificationStatus.Success;
                                            emailVerificationInfoViewModel.LastStatus =
                                                result.Result.LastStatus.ToString();
                                        }

                                        //start Syntax
                                        if (index == 0)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation";

                                            if (verificationLevelResult.Status == VerificationStatus.AtSignNotFound)
                                            {
                                                newEmailVerificationResultViewModel.ErrorMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";

                                                emailVerificationInfoViewModel.IsSynctexValidated = false;

                                                emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                                emailVerificationInfoViewModel.SyntaxMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";
                                            }
                                            else if (verificationLevelResult.Status == VerificationStatus.DomainPartCompliancyFailure)
                                            {
                                                newEmailVerificationResultViewModel.ErrorMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";

                                                emailVerificationInfoViewModel.IsSynctexValidated = false;

                                                emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                                emailVerificationInfoViewModel.SyntaxMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";
                                            }
                                            else if (verificationLevelResult.Status == VerificationStatus.CatchAllValidationTimeout)
                                            {
                                                newEmailVerificationResultViewModel.ErrorMessage = "The validation timed out. Please try again later";

                                                emailVerificationInfoViewModel.IsSynctexValidated = false;

                                                emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                                emailVerificationInfoViewModel.SyntaxMessage = "The validation timed out. Please try again later";
                                            }
                                            else if (verificationLevelResult.Status == VerificationStatus.Success)
                                            {
                                                newEmailVerificationResultViewModel.SuccessMessage = "The address is syntactically valid.";

                                                emailVerificationInfoViewModel.IsSynctexValidated = true;

                                                emailVerificationInfoViewModel.SyntaxStatus = "Success";
                                                emailVerificationInfoViewModel.SyntaxMessage = "The address is syntactically valid.";



                                            }
                                        }//end Syntax
                                        //start RoleAccount Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "roleAccount" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Role accounts detection";
                                            newEmailVerificationResultViewModel.SuccessMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                            emailVerificationInfoViewModel.IsRoleAccountValidated = true;

                                            emailVerificationInfoViewModel.RoleAccountStatus = "Success";
                                            emailVerificationInfoViewModel.RoleAccountMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                        }//end RoleAccount Success
                                        //start IspSpecificSyntax Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "ispSpecificSyntax" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation (ISP-specific rules)";
                                            newEmailVerificationResultViewModel.SuccessMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                            emailVerificationInfoViewModel.IsIspSpecificSyntaxValidated = true;

                                            emailVerificationInfoViewModel.SpecificSyntaxStatus = "Success";
                                            emailVerificationInfoViewModel.SpecificSyntaxMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                        }//end IspSpecificSyntax Success
                                        //start deaDomain Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "deaDomain" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (1st pass)";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                            emailVerificationInfoViewModel.IsDEADomainValidated = true;

                                            emailVerificationInfoViewModel.DeaDomainStatus = "Success";
                                            emailVerificationInfoViewModel.DeaDomainMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                        }//end deaDomain Success
                                        //start dns Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "dns" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "DNS records validation";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The domain of the email address has valid DNS records.";

                                            emailVerificationInfoViewModel.IsDNSValidated = true;

                                            emailVerificationInfoViewModel.DnsStatus = "Success";
                                            emailVerificationInfoViewModel.DnsMessage = "The domain of the email address has valid DNS records.";

                                        }//end dns Success
                                        //start deaMailExchanger Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "deaMailExchanger" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (2nd pass)";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                            emailVerificationInfoViewModel.IsDEAMailExchangerValidated = true;

                                            emailVerificationInfoViewModel.DeaMailExchangerStatus = "Success";
                                            emailVerificationInfoViewModel.DeaMailExchangerMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                        }//end deaMailExchanger Success
                                        //start smtp Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                            emailVerificationInfoViewModel.IsSMTPValidated = true;

                                            emailVerificationInfoViewModel.SmtpStatus = "Success";
                                            emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                        }//end smtp Success
                                        //start smtp Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                            emailVerificationInfoViewModel.IsSMTPValidated = false;

                                            emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                            emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                        }//end smtp Error
                                        //start smtp Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && VerificationStatus.Success.ToString().Contains("SmtpConnectionTimeout"))
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "Smtp connection timeout.";

                                            emailVerificationInfoViewModel.IsSMTPValidated = false;

                                            emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                            emailVerificationInfoViewModel.SmtpMessage = "Smtp connection timeout.";

                                        }//end smtp Error
                                        //start mailbox Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                            emailVerificationInfoViewModel.IsMailboxValidated = true;

                                            emailVerificationInfoViewModel.MailboxStatus = "Success";
                                            emailVerificationInfoViewModel.MailboxMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                        }//end mailbox Success
                                        //start mailbox not exist Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxDoesNotExist)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address does not exist.";

                                            emailVerificationInfoViewModel.IsMailboxValidated = false;

                                            emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                            emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address does not exist.";

                                        }//end mailbox not exist Error
                                        //start mailbox Temporarily Unavailable Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxTemporarilyUnavailable)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                            emailVerificationInfoViewModel.IsMailboxValidated = false;

                                            emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                            emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                        }//end mailbox Temporarily Unavailable Error
                                        //start mailbox Validation Timeout Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxValidationTimeout)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is timed out.";

                                            emailVerificationInfoViewModel.IsMailboxValidated = false;

                                            emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                            emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is timed out.";

                                        }//end mailbox Validation Timeout Error
                                        //start mailbox Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is not successful.";
                                            emailVerificationInfoViewModel.IsMailboxValidated = false;

                                            emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                            emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is not successful.";

                                        }//end mailbox Error
                                        //start catchAll Success
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                            newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                            emailVerificationInfoViewModel.IsCatchAllValidated = true;

                                            emailVerificationInfoViewModel.CatchAllStatus = "Success";
                                            emailVerificationInfoViewModel.CatchAllMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                        }//end catchAll Success
                                        //start catchAll ServerIsCatchAll Error
                                        else if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.ServerIsCatchAll)
                                        {
                                            newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                            newEmailVerificationResultViewModel.ErrorMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                            emailVerificationInfoViewModel.IsCatchAllValidated = false;

                                            emailVerificationInfoViewModel.CatchAllStatus = "Failure";
                                            emailVerificationInfoViewModel.CatchAllMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                        }
                                        //end catchAll ServerIsCatchAll Error
                                        break;
                                    }
                                    indexResult++;
                                }
                                index++;
                                emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                            }


                        }
                    }
                    //end

                    #endregion
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportByLevel(string primaryEmailAddress, int verificationLevel)
        {
            EmailVerificationViewModel emailVerificationInfoViewModel = new EmailVerificationViewModel();
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.Syntax;
                switch (verificationLevel)
                {
                    case 1: // Syntax
                        _verificationLevel = VerificationLevel.Syntax;
                        break;
                    case 2: // disposal / free
                        _verificationLevel = VerificationLevel.RoleAccount;
                        break;
                    case 3: // disposal / free
                        _verificationLevel = VerificationLevel.IspSpecificSyntax;
                        break;
                    case 4: // disposal / free
                        _verificationLevel = VerificationLevel.DeaDomain;
                        break;
                    case 5: // Dns
                        _verificationLevel = VerificationLevel.Dns;
                        break;
                    case 6: // disposal / free
                        _verificationLevel = VerificationLevel.DeaMailExchanger;
                        break;
                    case 7: // Smtp
                        _verificationLevel = VerificationLevel.Smtp;
                        break;
                    case 8: // Mailbox
                        _verificationLevel = VerificationLevel.Mailbox;
                        break;
                    case 9:// CatchAll
                        _verificationLevel = VerificationLevel.CatchAll;
                        break;
                }

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {
                        int index = 0;
                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;

                            int indexResult = 0;

                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {
                                if (index == indexResult)
                                {
                                    newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                    if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                    {
                                        emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                                                                    VerificationStatus.Success;
                                        emailVerificationInfoViewModel.LastStatus =
                                            result.Result.LastStatus.ToString();
                                    }

                                    //start Syntax
                                    if (index == 0)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation";

                                        if (verificationLevelResult.Status == VerificationStatus.AtSignNotFound)
                                        {
                                            newEmailVerificationResultViewModel.ErrorMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";

                                            emailVerificationInfoViewModel.IsSynctexValidated = false;

                                            emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                            emailVerificationInfoViewModel.SyntaxMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";
                                        }
                                        else if (verificationLevelResult.Status == VerificationStatus.DomainPartCompliancyFailure)
                                        {
                                            newEmailVerificationResultViewModel.ErrorMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";

                                            emailVerificationInfoViewModel.IsSynctexValidated = false;

                                            emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                            emailVerificationInfoViewModel.SyntaxMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";
                                        }
                                        else if (verificationLevelResult.Status == VerificationStatus.CatchAllValidationTimeout)
                                        {
                                            newEmailVerificationResultViewModel.ErrorMessage = "The validation timed out. Please try again later";

                                            emailVerificationInfoViewModel.IsSynctexValidated = false;

                                            emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                            emailVerificationInfoViewModel.SyntaxMessage = "The validation timed out. Please try again later";
                                        }
                                        else if (verificationLevelResult.Status == VerificationStatus.Success)
                                        {
                                            newEmailVerificationResultViewModel.SuccessMessage = "The address is syntactically valid.";

                                            emailVerificationInfoViewModel.IsSynctexValidated = true;

                                            emailVerificationInfoViewModel.SyntaxStatus = "Success";
                                            emailVerificationInfoViewModel.SyntaxMessage = "The address is syntactically valid.";

                                        }
                                    }//end Syntax
                                    //start RoleAccount Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "roleAccount" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Role accounts detection";
                                        newEmailVerificationResultViewModel.SuccessMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                        emailVerificationInfoViewModel.IsRoleAccountValidated = true;

                                        emailVerificationInfoViewModel.RoleAccountStatus = "Success";
                                        emailVerificationInfoViewModel.RoleAccountMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                    }//end RoleAccount Success
                                    //start IspSpecificSyntax Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "ispSpecificSyntax" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation (ISP-specific rules)";
                                        newEmailVerificationResultViewModel.SuccessMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                        emailVerificationInfoViewModel.IsIspSpecificSyntaxValidated = true;

                                        emailVerificationInfoViewModel.SpecificSyntaxStatus = "Success";
                                        emailVerificationInfoViewModel.SpecificSyntaxMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                    }//end IspSpecificSyntax Success
                                    //start deaDomain Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "deaDomain" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (1st pass)";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                        emailVerificationInfoViewModel.IsDEADomainValidated = true;

                                        emailVerificationInfoViewModel.DeaDomainStatus = "Success";
                                        emailVerificationInfoViewModel.DeaDomainMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                    }//end deaDomain Success
                                    //start dns Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "dns" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "DNS records validation";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The domain of the email address has valid DNS records.";

                                        emailVerificationInfoViewModel.IsDNSValidated = true;

                                        emailVerificationInfoViewModel.DnsStatus = "Success";
                                        emailVerificationInfoViewModel.DnsMessage = "The domain of the email address has valid DNS records.";

                                    }//end dns Success
                                    //start deaMailExchanger Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "deaMailExchanger" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (2nd pass)";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                        emailVerificationInfoViewModel.IsDEAMailExchangerValidated = true;

                                        emailVerificationInfoViewModel.DeaMailExchangerStatus = "Success";
                                        emailVerificationInfoViewModel.DeaMailExchangerMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                    }//end deaMailExchanger Success
                                    //start smtp Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                        emailVerificationInfoViewModel.IsSMTPValidated = true;

                                        emailVerificationInfoViewModel.SmtpStatus = "Success";
                                        emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                    }//end smtp Success
                                    //start smtp Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                        emailVerificationInfoViewModel.IsSMTPValidated = false;

                                        emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                        emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                    }//end smtp Error
                                    //start smtp Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && VerificationStatus.Success.ToString().Contains("SmtpConnectionTimeout"))
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "Smtp connection timeout.";

                                        emailVerificationInfoViewModel.IsSMTPValidated = false;

                                        emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                        emailVerificationInfoViewModel.SmtpMessage = "Smtp connection timeout.";

                                    }//end smtp Error
                                    //start mailbox Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                        emailVerificationInfoViewModel.IsMailboxValidated = true;

                                        emailVerificationInfoViewModel.MailboxStatus = "Success";
                                        emailVerificationInfoViewModel.MailboxMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                    }//end mailbox Success
                                    //start mailbox not exist Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxDoesNotExist)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address does not exist.";

                                        emailVerificationInfoViewModel.IsMailboxValidated = false;

                                        emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                        emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address does not exist.";

                                    }//end mailbox not exist Error
                                    //start mailbox Temporarily Unavailable Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxTemporarilyUnavailable)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                        emailVerificationInfoViewModel.IsMailboxValidated = false;

                                        emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                        emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                    }//end mailbox Temporarily Unavailable Error
                                    //start mailbox Validation Timeout Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxValidationTimeout)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is timed out.";

                                        emailVerificationInfoViewModel.IsMailboxValidated = false;

                                        emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                        emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is timed out.";

                                    }//end mailbox Validation Timeout Error
                                    //start mailbox Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is not successful.";
                                        emailVerificationInfoViewModel.IsMailboxValidated = false;

                                        emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                        emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is not successful.";

                                    }//end mailbox Error
                                    //start catchAll Success
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                        newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                        emailVerificationInfoViewModel.IsCatchAllValidated = true;

                                        emailVerificationInfoViewModel.CatchAllStatus = "Success";
                                        emailVerificationInfoViewModel.CatchAllMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                    }//end catchAll Success
                                    //start catchAll ServerIsCatchAll Error
                                    else if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.ServerIsCatchAll)
                                    {
                                        newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                        newEmailVerificationResultViewModel.ErrorMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                        emailVerificationInfoViewModel.IsCatchAllValidated = false;

                                        emailVerificationInfoViewModel.CatchAllStatus = "Failure";
                                        emailVerificationInfoViewModel.CatchAllMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                    }
                                    //end catchAll ServerIsCatchAll Error
                                    break;
                                }
                                indexResult++;
                            }
                            index++;
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }


                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForAll(string primaryEmailAddress)
        {
            EmailVerificationViewModel emailVerificationInfoViewModel = new EmailVerificationViewModel();
            try
            {
                List<VerificationLevel> verificationLevelList = new List<VerificationLevel>
                {
                    VerificationLevel.Syntax,
                    VerificationLevel.RoleAccount,
                    VerificationLevel.IspSpecificSyntax,
                    VerificationLevel.DeaDomain,
                    VerificationLevel.Dns,
                    VerificationLevel.DeaMailExchanger,
                    VerificationLevel.Smtp,
                    VerificationLevel.Mailbox,
                    VerificationLevel.CatchAll
                };

                foreach (var verificationLevel in verificationLevelList)
                {
                    if (verificationLevel.Name == VerificationLevel.Syntax.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForSyntax(primaryEmailAddress, emailVerificationInfoViewModel);
                        if (emailVerificationInfoViewModel.SyntaxStatus == "Success")
                        {
                            emailVerificationInfoViewModel.IsPositive = true;
                            emailVerificationInfoViewModel.LastStatus = "Success";
                        }
                    }
                    else if (verificationLevel.Name == VerificationLevel.RoleAccount.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForRoleAccount(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.IspSpecificSyntax.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForIspSpecificSyntax(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.DeaDomain.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForDeaDomain(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.Dns.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForDns(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.DeaMailExchanger.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForDeaMailExchanger(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.Smtp.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForSmtp(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.Mailbox.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForMailbox(primaryEmailAddress, emailVerificationInfoViewModel);
                    }
                    else if (verificationLevel.Name == VerificationLevel.CatchAll.ToString())
                    {
                        emailVerificationInfoViewModel = GetEmailVerificationReportForCatchAll(primaryEmailAddress, emailVerificationInfoViewModel);
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForSyntax(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.Syntax;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {
                        //start foreach
                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;

                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {
                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start Syntax
                                newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation";

                                if (verificationLevelResult.Status == VerificationStatus.AtSignNotFound)
                                {
                                    newEmailVerificationResultViewModel.ErrorMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";

                                    emailVerificationInfoViewModel.IsSynctexValidated = false;

                                    emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                    emailVerificationInfoViewModel.SyntaxMessage = "The at sign symbol (@), used to separate the local part from the domain part of the address, has not been found.";
                                }
                                else if (verificationLevelResult.Status == VerificationStatus.DomainPartCompliancyFailure)
                                {
                                    newEmailVerificationResultViewModel.ErrorMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";

                                    emailVerificationInfoViewModel.IsSynctexValidated = false;

                                    emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                    emailVerificationInfoViewModel.SyntaxMessage = "The domain part of the e-mail address is not compliant with the IETF standards.";
                                }
                                else if (verificationLevelResult.Status == VerificationStatus.CatchAllValidationTimeout)
                                {
                                    newEmailVerificationResultViewModel.ErrorMessage = "The validation timed out. Please try again later";

                                    emailVerificationInfoViewModel.IsSynctexValidated = false;

                                    emailVerificationInfoViewModel.SyntaxStatus = "Failure";
                                    emailVerificationInfoViewModel.SyntaxMessage = "The validation timed out. Please try again later";
                                }
                                else if (verificationLevelResult.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.SuccessMessage = "The address is syntactically valid.";

                                    emailVerificationInfoViewModel.IsSynctexValidated = true;

                                    emailVerificationInfoViewModel.SyntaxStatus = "Success";
                                    emailVerificationInfoViewModel.SyntaxMessage = "The address is syntactically valid.";

                                }//end Syntax
                                break;
                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }


                    }
                    //end foreach
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForRoleAccount(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.RoleAccount;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    //emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    //emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    //emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    //emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    //emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    //emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    //emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    //emailVerificationInfoViewModel.Comments = result.Comments;
                    //emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;
                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;

                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start RoleAccount Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "roleAccount" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Role accounts detection";
                                    newEmailVerificationResultViewModel.SuccessMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                    emailVerificationInfoViewModel.IsRoleAccountValidated = true;

                                    emailVerificationInfoViewModel.RoleAccountStatus = "Success";
                                    emailVerificationInfoViewModel.RoleAccountMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                }//end RoleAccount Success
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForIspSpecificSyntax(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {

            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.IspSpecificSyntax;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    //emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    //emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    //emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    //emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    //emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    //emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    //emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    //emailVerificationInfoViewModel.Comments = result.Comments;
                    //emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;
                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;

                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start IspSpecificSyntax Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "ispSpecificSyntax" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Syntax validation (ISP-specific rules)";
                                    newEmailVerificationResultViewModel.SuccessMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                    emailVerificationInfoViewModel.IsIspSpecificSyntaxValidated = true;

                                    emailVerificationInfoViewModel.SpecificSyntaxStatus = "Success";
                                    emailVerificationInfoViewModel.SpecificSyntaxMessage = "According to the additional syntax rules of the ISP which manages this address, the address is syntactically valid.";

                                }//end IspSpecificSyntax Success
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForDeaDomain(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.DeaDomain;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start deaDomain Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "deaDomain" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (1st pass)";
                                    newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                    emailVerificationInfoViewModel.IsDEADomainValidated = true;

                                    emailVerificationInfoViewModel.DeaDomainStatus = "Success";
                                    emailVerificationInfoViewModel.DeaDomainMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                }//end deaDomain Success
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForDns(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.Dns;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start RoleAccount Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "roleAccount" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Role accounts detection";
                                    newEmailVerificationResultViewModel.SuccessMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                    emailVerificationInfoViewModel.IsRoleAccountValidated = true;

                                    emailVerificationInfoViewModel.RoleAccountStatus = "Success";
                                    emailVerificationInfoViewModel.RoleAccountMessage = emailVerificationInfoViewModel.EmailAddressLocalPart + " is not a role account. ";

                                }//end RoleAccount Success
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForDeaMailExchanger(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.DeaMailExchanger;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start deaMailExchanger Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "deaMailExchanger" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Disposable email address (DEA) validation (2nd pass)";
                                    newEmailVerificationResultViewModel.SuccessMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                    emailVerificationInfoViewModel.IsDEAMailExchangerValidated = true;

                                    emailVerificationInfoViewModel.DeaMailExchangerStatus = "Success";
                                    emailVerificationInfoViewModel.DeaMailExchangerMessage = "The address is not provided by a known <a href='http://en.wikipedia.org/wiki/Disposable_e-mail_address'>disposable e-mail address</a> (DEA) provider.";

                                }//end deaMailExchanger Success
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForSmtp(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.Smtp;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start smtp Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                    newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                    emailVerificationInfoViewModel.IsSMTPValidated = true;

                                    emailVerificationInfoViewModel.SmtpStatus = "Success";
                                    emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can be contacted successfully.";

                                }//end smtp Success
                                //start smtp Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                    emailVerificationInfoViewModel.IsSMTPValidated = false;

                                    emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                    emailVerificationInfoViewModel.SmtpMessage = "The mail exchanger of the email address domain can not be contacted successfully.";

                                }//end smtp Error
                                //start smtp Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "smtp" && VerificationStatus.Success.ToString().Contains("SmtpConnectionTimeout"))
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "SMTP server validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "Smtp connection timeout.";

                                    emailVerificationInfoViewModel.IsSMTPValidated = false;

                                    emailVerificationInfoViewModel.SmtpStatus = "Failure";
                                    emailVerificationInfoViewModel.SmtpMessage = "Smtp connection timeout.";

                                }//end smtp Error
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForMailbox(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.Mailbox;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start mailbox Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                    newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                    emailVerificationInfoViewModel.IsMailboxValidated = true;

                                    emailVerificationInfoViewModel.MailboxStatus = "Success";
                                    emailVerificationInfoViewModel.MailboxMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                }//end mailbox Success
                                //start mailbox not exist Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxDoesNotExist)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address does not exist.";

                                    emailVerificationInfoViewModel.IsMailboxValidated = false;

                                    emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                    emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address does not exist.";

                                }//end mailbox not exist Error
                                //start mailbox Temporarily Unavailable Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxTemporarilyUnavailable)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                    emailVerificationInfoViewModel.IsMailboxValidated = false;

                                    emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                    emailVerificationInfoViewModel.MailboxMessage = "The mailbox for the e-mail address is temporarily unavailable.";

                                }//end mailbox Temporarily Unavailable Error
                                //start mailbox Validation Timeout Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status == VerificationStatus.MailboxValidationTimeout)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is timed out.";

                                    emailVerificationInfoViewModel.IsMailboxValidated = false;

                                    emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                    emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is timed out.";

                                }//end mailbox Validation Timeout Error
                                //start mailbox Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "mailbox" && newEmailVerificationResultViewModel.Status != VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Mailbox validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The mailbox validation for the e-mail address is not successful.";
                                    emailVerificationInfoViewModel.IsMailboxValidated = false;

                                    emailVerificationInfoViewModel.MailboxStatus = "Failure";
                                    emailVerificationInfoViewModel.MailboxMessage = "The mailbox validation for the e-mail address is not successful.";

                                }//end mailbox Error
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }

        public static EmailVerificationViewModel GetEmailVerificationReportForCatchAll(string primaryEmailAddress, EmailVerificationViewModel emailVerificationInfoViewModel)
        {
            try
            {
                VerificationLevel _verificationLevel = VerificationLevel.CatchAll;

                #region Code

                //start 
                LicensingManager.SetLicenseKey(SiteConfigurationReader.CobisiLicenseKey);
                VerificationEngine engine = new VerificationEngine();
                engine.VerificationLevelCompleted += (sender, e) =>
                {
                    switch (e.Level.Name.ToUpper())
                    {
                        case "SYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 1;
                            break;

                        case "ROLEACCOUNT":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 2;
                            break;

                        case "ISPSPECIFICSYNTAX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 3;
                            break;

                        case "DEADOMAIN":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 4;
                            break;

                        case "DNS":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 5;
                            break;

                        case "DEAMAILEXCHANGER":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 6;
                            break;

                        case "SMTP":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 7;
                            break;

                        case "MAILBOX":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 8;
                            break;

                        case "CATCHALL":
                            if (e.Result.Status == VerificationStatus.Success) emailVerificationInfoViewModel.VerificationLevelNo = 9;
                            break;
                    }
                };
                var result = engine.Run(primaryEmailAddress, _verificationLevel);
                if (result != null)
                {
                    emailVerificationInfoViewModel.EmailAddress = result.EmailAddress;
                    emailVerificationInfoViewModel.AsciiEmailAddressDomainPart = result.AsciiEmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressDomainPart = result.EmailAddressDomainPart;
                    emailVerificationInfoViewModel.EmailAddressLocalPart = result.EmailAddressLocalPart;

                    emailVerificationInfoViewModel.HasInternationalMailboxName = result.HasInternationalMailboxName;
                    emailVerificationInfoViewModel.HasInternationalDomainName = result.HasInternationalDomainName;
                    emailVerificationInfoViewModel.IsDisposableEmailAddress = result.IsDisposableEmailAddress;
                    emailVerificationInfoViewModel.Comments = result.Comments;
                    emailVerificationInfoViewModel.CurrentLevel = result.CurrentLevel;

                }
                emailVerificationInfoViewModel.EmailVerificationResultViewModelList = new List<EmailVerificationResultViewModel>();
                if (result.Result != null)
                {
                    if (result.Result.AllLevels != null)
                    {

                        foreach (VerificationLevel itemLevel in result.Result.AllLevels)
                        {
                            EmailVerificationResultViewModel newEmailVerificationResultViewModel = new EmailVerificationResultViewModel();
                            newEmailVerificationResultViewModel.ResultLevel = itemLevel.Name;


                            foreach (VerificationLevelResult verificationLevelResult in result.Result.AllLevelResults)
                            {

                                newEmailVerificationResultViewModel.Status = verificationLevelResult.Status;
                                //if (newEmailVerificationResultViewModel.Status == verificationLevelResult.Status)
                                //{
                                //    emailVerificationInfoViewModel.IsPositive = result.Result.LastStatus ==
                                //                                                VerificationStatus.Success;
                                //    emailVerificationInfoViewModel.LastStatus =
                                //        result.Result.LastStatus.ToString();
                                //}

                                //start catchAll Success
                                if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.Success)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                    newEmailVerificationResultViewModel.SuccessMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                    emailVerificationInfoViewModel.IsCatchAllValidated = true;

                                    emailVerificationInfoViewModel.CatchAllStatus = "Success";
                                    emailVerificationInfoViewModel.CatchAllMessage = "The mail exchanger reponsible for the email address domain can accept messages sent to the email address under test.";

                                }//end catchAll Success
                                //start catchAll ServerIsCatchAll Error
                                else if (newEmailVerificationResultViewModel.ResultLevel == "catchAll" && newEmailVerificationResultViewModel.Status == VerificationStatus.ServerIsCatchAll)
                                {
                                    newEmailVerificationResultViewModel.ResultLevelDescription = "Catch-all mail exchanger validation";
                                    newEmailVerificationResultViewModel.ErrorMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                    emailVerificationInfoViewModel.IsCatchAllValidated = false;

                                    emailVerificationInfoViewModel.CatchAllStatus = "Failure";
                                    emailVerificationInfoViewModel.CatchAllMessage = "The external mail exchanger accepts fake, non existent, e-mail addresses; therefore the provided email address MAY be inexistent too.";

                                }
                                //end catchAll ServerIsCatchAll Error
                                break;

                            }
                            emailVerificationInfoViewModel.EmailVerificationResultViewModelList.Add(newEmailVerificationResultViewModel);
                        }

                    }
                }
                //end

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return emailVerificationInfoViewModel;
        }
    }
}