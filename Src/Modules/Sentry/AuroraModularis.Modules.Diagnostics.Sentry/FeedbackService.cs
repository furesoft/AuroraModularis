using Sentry;
using AuroraModularis.Modules.Diagnostics.Sentry.Models;

namespace AuroraModularis.Modules.Diagnostics.Sentry;

internal class FeedbackServiceImpl : IFeedbackService
{
    public void SendFeedback(string message, string title = "User feedback.", 
        string email = "fake@fake.fk")
    {
        var eventId = SentrySdk.CaptureMessage(title);
        
        SentrySdk.CaptureUserFeedback(eventId, email, message, Environment.UserName);
    }
}
