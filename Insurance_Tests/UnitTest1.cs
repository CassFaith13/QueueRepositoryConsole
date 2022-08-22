using Insurance_Repository;

namespace Insurance_Tests;

[TestClass]
public class Tests
{
    [TestMethod]
    public void SetCorrectClaim()
    {
        ClaimQueue claim = new ClaimQueue();
        claim.ClaimID = 1;

        int expected = 1;
        int actual = claim.ClaimID;

        Assert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    [DataRow(ValidStatus.Yes, true)]
    [DataRow(ValidStatus.No, false)]

    public void GetValidStatus(ValidStatus validStatus, bool expectedValidStatus)
    {
        ClaimQueue claim = new ClaimQueue(4, ClaimType.Car, "Client's car was submerged in flood waters due to Hurricane Michaela. Total Loss.", new DateTime (2022, 08, 25), new DateTime(2022, 09, 01), 35000.00, validStatus);

        bool expected = expectedValidStatus;
        bool actual = claim.IsValid;

        Assert.AreEqual(expected, actual);
    }
}