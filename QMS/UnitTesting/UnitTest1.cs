using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class ServiceActionTester
    {
        [TestMethod]
        public void TestComplaintDueAlert()
        {
            QMS.Utilities.ServiceActions ServiceActionsObj = new QMS.Utilities.ServiceActions();
            ServiceActionsObj.ComplaintDueAlerts();
        }
    }
}
