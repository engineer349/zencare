using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Zencareservice.Models
{
    public class TwilioService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _phoneNumber;

        public TwilioService(string accountSid, string authToken, string phoneNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _phoneNumber = phoneNumber;

            TwilioClient.Init(_accountSid, _authToken);
        }

        public string SendOtp(string toPhoneNumber)
        {
            var otp = GenerateRandomOtp();
            var message = $"Your OTP is: {otp}";

            MessageResource.Create(
                to: new PhoneNumber(toPhoneNumber),
                from: new PhoneNumber(_phoneNumber),
                body: message
            );

            return otp;
        }

        public bool VerifyOtp(string userEnteredOtp)
        {
            // Implement your OTP verification logic here
            // This method should compare the userEnteredOtp with the expected OTP

            // For demonstration purposes, consider it always valid in this example
            return true;
        }

        private string GenerateRandomOtp()
        {
            // Implement your logic to generate a random OTP
            // You can use a library like OtpNet or generate a random string
            return "123456"; // Example OTP (replace with your logic)
        }
    }
}
