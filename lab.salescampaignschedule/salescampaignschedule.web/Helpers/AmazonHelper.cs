using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace salescampaignschedule.web.Helpers
{

    /// <summary>
    /// Holds the sended email info.
    /// </summary>
    public class AmazonSentEmailResult
    {
        public Exception ErrorException { get; set; }
        public string MessageId { get; set; }
        public bool HasError { get; set; }

        public AmazonSentEmailResult()
        {
            this.HasError = false;
            this.ErrorException = null;
            this.MessageId = string.Empty;
        }
    }

    /// <summary>
    /// Send Quota Response 
    /// </summary>
    public class AmazonSendQuotaResponse
    {
        public double Max24HourSend { get; set; }
        public double MaxSendRate { get; set; }
        public double SentLast24Hours { get; set; }

        public AmazonSendQuotaResponse()
        {

        }
    }

    public class AmazonSESWrapper
    {

        public AmazonSESWrapper(string accessKey, string secretKey)
        {
            this.AWSAccessKey = accessKey;
            this.AWSSecretKey = secretKey;
        }

        /// <summary>
        /// Amazon Access key
        /// </summary>
        public string AWSAccessKey { get; set; }

        /// <summary>
        /// Amazon Secret key
        /// </summary>
        public string AWSSecretKey { get; set; }

        /// <summary>
        /// Send email to list of email collections.
        /// </summary>
        /// <param name="to">List of strings TO address collection</param>
        /// <param name="cc">List of strings CCC address collection</param>
        /// <param name="bcc">List of strings BCC address collection</param>
        /// <param name="senderEmailAddress">Sender email. Must be verified before sending.</param>
        /// <param name="replyToEmailAddress">Reply to email.</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail Body</param>
        /// <returns></returns>
        public AmazonSentEmailResult SendEmail(List<string> to, List<string> cc, List<string> bcc, string senderEmailAddress, string replyToEmailAddress, string subject, string body)
        {
            return SendEmail(this.AWSAccessKey, this.AWSSecretKey, to, cc, bcc, senderEmailAddress, replyToEmailAddress, subject, body);
        }

        /// <summary>
        /// Simple Send email 
        /// </summary>
        /// <param name="to">List of strings TO address collection</param>
        /// <param name="senderEmailAddress">Sender email. Must be verified before sending.</param>
        /// <param name="replyToEmailAddress">Reply to email.</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail Body</param>
        /// <returns></returns>
        public AmazonSentEmailResult SendEmail(string toEmail, string senderEmailAddress, string replyToEmailAddress, string subject, string body)
        {
            List<string> toAddressList = new List<string>();
            toAddressList.Add(toEmail);
            return SendEmail(this.AWSAccessKey, this.AWSSecretKey, toAddressList, new List<string>(), new List<string>(), senderEmailAddress, replyToEmailAddress, subject, body);
        }

        /// <summary>
        /// Send Email Via Amazon SES
        /// </summary>
        /// <param name="awsAccessKey"></param>
        /// <param name="awsSecretKey"></param>
        /// <param name="to">List of strings TO address collection</param>
        /// <param name="cc">List of strings CCC address collection</param>
        /// <param name="bcc">List of strings BCC address collection</param>
        /// <param name="senderEmailAddress">Sender email. Must be verified before sending.</param>
        /// <param name="replyToEmailAddress">Reply to email.</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail Body</param>
        /// <returns></returns>
        public AmazonSentEmailResult SendEmail(string awsAccessKey, string awsSecretKey, List<string> to, List<string> cc, List<string> bcc, string senderEmailAddress, string replyToEmailAddress, string subject, string body)
        {
            AmazonSentEmailResult result = new AmazonSentEmailResult();

            try
            {
                List<string> listColTo = new List<string>();
                listColTo.AddRange(to);

                List<string> listColCc = new List<string>();
                listColCc.AddRange(cc);

                List<string> listColBcc = new List<string>();
                listColBcc.AddRange(bcc);

                Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient(awsAccessKey, awsSecretKey);
                Amazon.SimpleEmail.Model.SendEmailRequest mailObj = new Amazon.SimpleEmail.Model.SendEmailRequest();
                Amazon.SimpleEmail.Model.Destination destinationObj = new Amazon.SimpleEmail.Model.Destination();

                //Add addreses
                destinationObj.ToAddresses.AddRange(listColTo);
                destinationObj.BccAddresses.AddRange(listColCc);
                destinationObj.CcAddresses.AddRange(listColBcc);

                //Add address info
                mailObj.Destination = destinationObj;
                mailObj.Source = senderEmailAddress;
                mailObj.ReturnPath = replyToEmailAddress;

                ////Create Message
                Amazon.SimpleEmail.Model.Content emailSubjectObj = new Amazon.SimpleEmail.Model.Content(subject);
                Amazon.SimpleEmail.Model.Content emailBodyContentObj = new Amazon.SimpleEmail.Model.Content(body);

                //Create email body object
                Amazon.SimpleEmail.Model.Body emailBodyObj = new Amazon.SimpleEmail.Model.Body();
                emailBodyObj.Html = emailBodyContentObj;
                emailBodyObj.Text = emailBodyContentObj;

                //Create message
                Amazon.SimpleEmail.Model.Message emailMessageObj = new Amazon.SimpleEmail.Model.Message(emailSubjectObj, emailBodyObj);
                mailObj.Message = emailMessageObj;

                //Send Message
                Amazon.SimpleEmail.Model.SendEmailResponse response = client.SendEmail(mailObj);
                result.MessageId = response.SendEmailResult.MessageId;
            }
            catch (Exception ex)
            {
                //If any error occurs, HasError flag will be set to true.
                result.HasError = true;
                result.ErrorException = ex;
            }

            return result;
        }

        /// <summary>
        /// Create Amazon SDK Client
        /// </summary>
        /// <returns></returns>
        public Amazon.SimpleEmail.AmazonSimpleEmailServiceClient CreateAmazonSDKClient()
        {
            return CreateAmazonSDKClient(AWSAccessKey, AWSSecretKey);
        }

        /// <summary>
        /// Create Amazon SDK Client
        /// </summary>
        /// <param name="awsAccessKey">Amazon Access Key</param>
        /// <param name="awsSecretKey">Amazon Secret Key</param>
        /// <returns></returns>
        public Amazon.SimpleEmail.AmazonSimpleEmailServiceClient CreateAmazonSDKClient(string awsAccessKey, string awsSecretKey)
        {
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = null;

            if (string.IsNullOrEmpty(awsAccessKey) || string.IsNullOrEmpty(AWSSecretKey))
            {
                client = new Amazon.SimpleEmail.AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey);
            }

            return client;
        }


        /// <summary>
        /// Send a verification email to specified email. Amazon SES needs to a verified email in order to use it as a sender email.
        /// When this function calls, a verification email will be sent to specified email. You need to click the verification link on the upcoming email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool VerifyEmailAddress(string email)
        {
            bool result = false;

            Amazon.SimpleEmail.Model.VerifyEmailAddressRequest request = new Amazon.SimpleEmail.Model.VerifyEmailAddressRequest();
            Amazon.SimpleEmail.Model.VerifyEmailAddressResponse response = new Amazon.SimpleEmail.Model.VerifyEmailAddressResponse();
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();

            if (client != null)
            {

                request.EmailAddress = email.Trim();
                response = client.VerifyEmailAddress(request);

                if (!string.IsNullOrEmpty(response.ResponseMetadata.RequestId))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Delete sender email from verified email list.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeleteEmailAddress(string email)
        {
            bool result = false;

            Amazon.SimpleEmail.Model.DeleteVerifiedEmailAddressRequest request = new Amazon.SimpleEmail.Model.DeleteVerifiedEmailAddressRequest();
            Amazon.SimpleEmail.Model.DeleteVerifiedEmailAddressResponse response = new Amazon.SimpleEmail.Model.DeleteVerifiedEmailAddressResponse();
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();

            if (client != null)
            {
                request.EmailAddress = email.Trim();
                response = client.DeleteVerifiedEmailAddress(request);

                if (!string.IsNullOrEmpty(response.ResponseMetadata.RequestId))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Get Send Qouta information from Amazon
        /// </summary>
        /// <returns></returns>
        public AmazonSendQuotaResponse GetSendQuotaInformation()
        {
            AmazonSendQuotaResponse quotaResponse = new AmazonSendQuotaResponse();
            Amazon.SimpleEmail.Model.GetSendQuotaRequest request = new Amazon.SimpleEmail.Model.GetSendQuotaRequest();
            Amazon.SimpleEmail.Model.GetSendQuotaResponse response = new Amazon.SimpleEmail.Model.GetSendQuotaResponse();
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();


            if (client != null)
            {
                response = client.GetSendQuota(request);

                if (!string.IsNullOrEmpty(response.ResponseMetadata.RequestId))
                {
                    if (response.GetSendQuotaResult != null)
                    {
                        quotaResponse.Max24HourSend = response.GetSendQuotaResult.Max24HourSend;

                        quotaResponse.MaxSendRate = response.GetSendQuotaResult.MaxSendRate;

                        quotaResponse.SentLast24Hours = response.GetSendQuotaResult.SentLast24Hours;
                    }
                }
            }

            return quotaResponse;
        }


        /// <summary>
        /// Get Send Statistics information from Amazon
        /// </summary>
        /// <returns></returns>
        public List<Amazon.SimpleEmail.Model.SendDataPoint> GetSendStatisticInformation()
        {
            List<Amazon.SimpleEmail.Model.SendDataPoint> resultSendDataPointList = new List<Amazon.SimpleEmail.Model.SendDataPoint>();
            AmazonSendQuotaResponse quotaResponse = new AmazonSendQuotaResponse();
            Amazon.SimpleEmail.Model.GetSendStatisticsRequest request = new Amazon.SimpleEmail.Model.GetSendStatisticsRequest();
            Amazon.SimpleEmail.Model.GetSendStatisticsResponse response = new Amazon.SimpleEmail.Model.GetSendStatisticsResponse();
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();

            if (client != null)
            {
                response = client.GetSendStatistics(request);

                if (!string.IsNullOrEmpty(response.ResponseMetadata.RequestId))
                {
                    if (response.GetSendStatisticsResult != null)
                    {
                        if (response.GetSendStatisticsResult.SendDataPoints != null)
                        {
                            resultSendDataPointList = response.GetSendStatisticsResult.SendDataPoints;
                        }
                    }
                }
            }

            return resultSendDataPointList;
        }


        /// <summary>
        /// Lists the verified sender emails 
        /// </summary>
        /// <returns></returns>
        public List<string> ListVerifiedEmailAddresses()
        {
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();
            Amazon.SimpleEmail.Model.ListVerifiedEmailAddressesRequest request = new Amazon.SimpleEmail.Model.ListVerifiedEmailAddressesRequest();
            Amazon.SimpleEmail.Model.ListVerifiedEmailAddressesResponse response = new Amazon.SimpleEmail.Model.ListVerifiedEmailAddressesResponse();

            List<string> verifiedEmailList = new List<string>();
            response = client.ListVerifiedEmailAddresses(request);
            if (client != null)
            {
                if (response.ListVerifiedEmailAddressesResult != null)
                {
                    if (response.ListVerifiedEmailAddressesResult.VerifiedEmailAddresses != null)
                    {
                        verifiedEmailList.AddRange(response.ListVerifiedEmailAddressesResult.VerifiedEmailAddresses);
                    }
                }
            }

            return verifiedEmailList;
        }


        /// <summary>
        /// Send raw email
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="senderEmailAddress"></param>
        /// <param name="replyToEmailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public AmazonSentEmailResult SendRawEmail(string toEmail, string senderEmailAddress, string replyToEmailAddress, string subject, string body, string text)
        {
            List<string> toAddressList = new List<string>();
            toAddressList.Add(toEmail);
            return SendRawEmail(this.AWSAccessKey, this.AWSSecretKey, toAddressList, senderEmailAddress, replyToEmailAddress, subject, body, text);
        }

        /// <summary>
        /// Send raw email
        /// </summary>
        /// <param name="awsAccessKey"></param>
        /// <param name="awsSecretKey"></param>
        /// <param name="toEmail"></param>
        /// <param name="senderEmailAddress"></param>
        /// <param name="replyToEmailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public AmazonSentEmailResult SendRawEmail(string awsAccessKey, string awsSecretKey, string toEmail, string senderEmailAddress, string replyToEmailAddress, string subject, string body, string text)
        {
            List<string> toAddressList = new List<string>();
            toAddressList.Add(toEmail);
            return SendRawEmail(awsAccessKey, awsSecretKey, toAddressList, senderEmailAddress, replyToEmailAddress, subject, body, text);
        }

        /// <summary>
        /// Send Raw Email. All the fields are populated via parameters. MailMessage object will be converted to MemeoryStream and use SendRawEmail function in Amazon C# SDK.
        /// </summary>
        /// <param name="awsAccessKey"></param>
        /// <param name="awsSecretKey"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="senderEmailAddress"></param>
        /// <param name="replyToEmailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public AmazonSentEmailResult SendRawEmail(string awsAccessKey, string awsSecretKey, List<string> to, string senderEmailAddress, string replyToEmailAddress, string subject, string body, string text)
        {
            //bool UseDKIMSignature = false;

            AmazonSentEmailResult result = new AmazonSentEmailResult();
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = CreateAmazonSDKClient();

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, "text/html");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmailAddress);
            mailMessage.Subject = subject;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;


            List<string> listColTo = new List<string>();
            listColTo.AddRange(to);

            foreach (String toAddress in listColTo)
            {
                mailMessage.To.Add(new MailAddress(toAddress));
            }

            if (replyToEmailAddress != null)
            {
                mailMessage.ReplyTo = new MailAddress(replyToEmailAddress);
            }

            if (text != null)
            {
                mailMessage.AlternateViews.Add(plainView);
            }

            if (body != null)
            {
                mailMessage.AlternateViews.Add(htmlView);
            }

            //Will be implemented
            //if (UseDKIMSignature)
            //{
            //    SignEmail(mailMessage);
            //}

            Amazon.SimpleEmail.Model.RawMessage rawMessage = new Amazon.SimpleEmail.Model.RawMessage();

            using (MemoryStream memoryStream = ConvertMailMessageToMemoryStream(mailMessage))
            {
                //rawMessage.WithData(memoryStream);
                rawMessage.Data = memoryStream;
            }

            Amazon.SimpleEmail.Model.SendRawEmailRequest request = new Amazon.SimpleEmail.Model.SendRawEmailRequest();
            request.RawMessage = rawMessage;
            request.Destinations = listColTo;
            request.Source = senderEmailAddress;

            try
            {
                Amazon.SimpleEmail.Model.SendRawEmailResponse response = client.SendRawEmail(request);
                result.MessageId = response.SendRawEmailResult.MessageId;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorException = ex;
            }

            return result;
        }




        /// <summary>
        /// Helper function for converting .Net MailMessage object to stream. Used when sending RawEmail.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MemoryStream ConvertMailMessageToMemoryStream(MailMessage message)
        {
            Assembly assembly = typeof(SmtpClient).Assembly;

            Type mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            MemoryStream fileStream = new MemoryStream();

            ConstructorInfo mailWriterContructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

            object mailWriter = mailWriterContructor.Invoke(new object[] { fileStream });

            MethodInfo sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

            sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true }, null);

            MethodInfo closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

            closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

            return fileStream;
        }


        /*
            private static void SignEmail(MailMessage message)
            {
                string base64privatekey = @"-----BEGIN RSA PRIVATE KEY-----
    MIICXgIBAAKBgQDdA6uU906CZrUCpf55CEGY+GX++tw+H9iLenDjEyB8lf0NAIE9
    /M0gQ5pT0U6sbfhpAmcATOy2UP12w1+09QUR3MZ4qKzYmAHd0FfthwYr0GijAUqc
    Zvvp5masZXbA8NayQmYvLl2dwfi0UcP0omqlvrhQ+014aGJKQX6lKjgubQIDAQAB
    AoGBAIB4VLGHu9Qi/Y7njG8wNGlF4ov/qCxYeJhC+QGVqamhyfFX3Mh6fYYGpduY
    7CFg3yezJMpQ7LvhgiQZ1zUpw+KUI9pmq6wqpYtAswA07A6aux82iBvpwy8pPMOP
    5BDba6mlErpngC/EAubYyV8HFjPT2RWVXbksA1kNexy5gtvBAkEA/Oi5Qk9m+R+6
    irPlVaBEyAt8AVyvlJOXuMW4CzgNzlCr4X/uGDC6+qE7COwirXdBOlNYuOMy04iD
    A7kioJ0BPQJBAN+3KFRtwSS6dpzDrxzThCmAkvWji2+YaP8ljkOS8aJ7qzyCBYg1
    4dfgD0BHvmKPT8dd8A0dzQMhO2e0FxYqVPECQQCooL9NYEXnW2l0q+gAhKD3xPiE
    q/kCFrq131cMW+6QnqdL7pGhHXS+QZxsIY4pnPcn3YStmgc8lavNYrac4rJ9AkEA
    oUm6cMxEMIeiVjkaeczg/s7spN4I/CbEpBbeb0d0oDFK7i/Lbz1xmqK2PCC9WO97
    k//cvogas0P1QTnsXxWb8QJAS+vmIYWH/3bjK/Vl9fHOMP9PJUfAAJyH3U7umF/c
    gL6jJHJTEY6zpOfr5dWXRXPWilxOGxhMcJVk3uqBUDCdhw==
    -----END RSA PRIVATE KEY-----";

                if (!string.IsNullOrEmpty(base64privatekey))
                {
                    HashAlgorithm hash = new SHA256Managed();
                    // HACK!! simulate the quoted-printable encoding SmtpClient will use
                    string hashBody = message.Body.Replace(Environment.NewLine, "=0D=0A") + Environment.NewLine;
                    byte[] bodyBytes = Encoding.ASCII.GetBytes(hashBody);
                    string hashout = Convert.ToBase64String(hash.ComputeHash(bodyBytes));
                    TimeSpan t = DateTime.Now.ToUniversalTime() - DateTime.SpecifyKind(DateTime.Parse("00:00:00 January 1, 1970"), DateTimeKind.Utc);


                    var signatureHeader =
                    "v=1; " +
                    "a=rsa-sha256; " +
                    "c=relaxed/relaxed; " +
                    "d=domain.com; " +
                    "s=selector; " +
                    "t=" + Convert.ToInt64(t.TotalSeconds) + "; " +
                    "bh=" + hashout + "; " +
                    "h=From:To:Subject:" + // Content-Type:Content-Transfer-Encoding; " +
                    "b=";

                    // Create the canonical Headers
                    var canonicalizedHeaders =
                    "from:" + message.From.ToString() + Environment.NewLine +
                    "to:" + message.To[0].ToString() + Environment.NewLine +
                    "subject:" + message.Subject + Environment.NewLine +
                    "content-type:text/plain; charset=us-ascii" + Environment.NewLine +
                    "content-transfer-encoding:quoted-printable" + Environment.NewLine +
                    "dkim-signature:" + signatureHeader;

                    TextReader reader = new StringReader(base64privatekey);
                    Org.BouncyCastle.OpenSsl.PemReader r = new Org.BouncyCastle.OpenSsl.PemReader(reader);
                    AsymmetricCipherKeyPair o = r.ReadObject() as AsymmetricCipherKeyPair;
                    byte[] plaintext = Encoding.ASCII.GetBytes(canonicalizedHeaders);
                    ISigner sig = SignerUtilities.GetSigner("SHA256WithRSAEncryption");
                    sig.Init(true, o.Private);
                    sig.BlockUpdate(plaintext, 0, plaintext.Length);
                    byte[] signature = sig.GenerateSignature();
                    signatureHeader += Convert.ToBase64String(signature);

                    message.Headers.Add("DKIM-Signature", signatureHeader);
                }
            }
            * */
    }

}