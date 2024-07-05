using CandidatesManagement.Core.ValueObjects;

namespace CandidatesManagement.UnitTests;

[TestFixture]
public class ValueObjectsUnitTests
{
    [Test]
    public void EmailAddress_ValidValue_InstanceCreated()
    {
        // Arrange
        string validEmail = "test@example.com";

        // Act
        var emailAddress = new EmailAddress(validEmail);

        // Assert
        Assert.AreEqual(validEmail, emailAddress.Value);
    }

    [Test]
    public void EmailAddress_EmptyValue_ExceptionIsThrown()
    {
        // Arrange
        string invalidEmail = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new EmailAddress(invalidEmail));
    }

    [Test]
    public void PhoneNumber_ValidValue_InstanceCreated()
    {
        // Arrange
        string validPhoneNumber = "+1234567890";

        // Act
        var phoneNumber = new PhoneNumber(validPhoneNumber);

        // Assert
        Assert.AreEqual(validPhoneNumber, phoneNumber.Value);
    }

    [Test]
    public void PhoneNumber_EmptyValue_ExceptionIsThrown()
    {
        // Arrange
        string invalidPhoneNumber = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new PhoneNumber(invalidPhoneNumber));
    }

    [Test]
    public void TimeIntervalValidValues_InstanceCreated()
    {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);

        // Act
        var timeInterval = new TimeInterval(startTime, endTime);

        // Assert
        Assert.AreEqual(startTime, timeInterval.Start);
        Assert.AreEqual(endTime, timeInterval.End);
    }

    [Test]
    public void TimeInterval_EndBeforeStart_ExceptionIsThrown()
    {
        // Arrange
        var startTime = new TimeOnly(17, 0);
        var endTime = new TimeOnly(9, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TimeInterval(startTime, endTime));
    }

    [Test]
    public void Url_ValidUrlValue_InstanceCreated()
    {
        // Arrange
        string validUrl = "https://example.com";

        // Act
        var url = new Url(validUrl);

        // Assert
        Assert.AreEqual(validUrl, url.Value);
    }

    [Test]
    public void Url_InvalidScheme_ExceptionIsThrown()
    {
        // Arrange
        string invalidUrl = "ftp://example.com";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Url(invalidUrl));
    }
}
