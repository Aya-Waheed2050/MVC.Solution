using Presentation.Utilities;
using Twilio.Rest.Api.V2010.Account;

namespace Presentation.Helpers
{
    public interface ISmsService
    {
        MessageResource SendSms(SmsMessage smsMessage);
    }
}
