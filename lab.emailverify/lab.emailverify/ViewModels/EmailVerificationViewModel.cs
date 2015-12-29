using Cobisi.EmailVerify;
using lab.emailverify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.emailverify.ViewModels
{
    public class EmailVerificationResultViewModel : BaseModel
    {
        public String ResultLevel { get; set; }
        public String ResultLevelDescription { get; set; }
        public String SuccessMessage { get; set; }
        public String ErrorMessage { get; set; }
        public VerificationStatus Status { get; set; }

    }

    public class EmailVerificationViewModel : BaseModel
    {
        public Int32 EmailVerificationId { get; set; }
        public Boolean IsPositive { get; set; }
        public Boolean IsVerified { get; set; }
        public String EmailAddress { get; set; }
        public String AsciiEmailAddressDomainPart { get; set; }
        public String EmailAddressDomainPart { get; set; }
        public String EmailAddressLocalPart { get; set; }
        
        public Boolean? HasInternationalMailboxName { get; set; }

        public Boolean? HasInternationalDomainName { get; set; }

        public Boolean? IsDisposableEmailAddress { get; set; }

        public String[] Comments { get; set; }

        public VerificationLevel CurrentLevel { get; set; }

        public String SyntaxStatus { get; set; }
        public String SyntaxMessage { get; set; }

        public String SpecificSyntaxStatus { get; set; }
        public String SpecificSyntaxMessage { get; set; }

        public String AddressStatus { get; set; }
        public String AddressMessage { get; set; }

        public String RoleAccountStatus { get; set; }
        public String RoleAccountMessage { get; set; }

        public String LocalPartStatus { get; set; }
        public String LocalPartMessage { get; set; }

        public String DomainPartStatus { get; set; }
        public String DomainPartMessage { get; set; }

        public String AsciiDomainPartStatus { get; set; }
        public String AsciiDomainPartMessage { get; set; }

        public String DeaDomainStatus { get; set; }
        public String DeaDomainMessage { get; set; }

        public String DeaMailExchangerStatus { get; set; }
        public String DeaMailExchangerMessage { get; set; }

        public String DnsStatus { get; set; }
        public String DnsMessage { get; set; }

        public String SmtpStatus { get; set; }
        public String SmtpMessage { get; set; }

        public String MailboxStatus { get; set; }
        public String MailboxMessage { get; set; }

        public String CatchAllStatus { get; set; }
        public String CatchAllMessage { get; set; }

        public Boolean IsSynctexValidated { get; set; }

        public Boolean IsRoleAccountValidated { get; set; }

        public Boolean IsIspSpecificSyntaxValidated { get; set; }

        public Boolean IsDEADomainValidated { get; set; }

        public Boolean IsDNSValidated { get; set; }

        public Boolean IsDEAMailExchangerValidated { get; set; }

        public Boolean IsSMTPValidated { get; set; }

        public Boolean IsMailboxValidated { get; set; }

        public Boolean IsCatchAllValidated { get; set; }

        public Int32 VerificationLevel { get; set; }

        public String LastStatus { get; set; }

        public String VerificationLevelName { get; set; }

        public Int32 VerificationLevelNo { get; set; }

        public List<EmailVerificationResultViewModel> EmailVerificationResultViewModelList { get; set; }
    }
}