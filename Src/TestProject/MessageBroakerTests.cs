using AuroraModularis.Messaging;

namespace TestProject;

[TestFixture]
public class MessageBroakerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var mb = new MessageBroker();
        mb.GetInboxes();
    }
}