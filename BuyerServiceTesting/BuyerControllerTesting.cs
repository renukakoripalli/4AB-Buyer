﻿using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Models;
using BUYERDBENTITY.Repositories;
using BuyerService.Controllers;
using BuyerService.Manager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyerServiceTesting
{
    [TestFixture]
    public class BuyerControllerTesting
    {
        BuyerController buyerController;
        private Mock<IBuyerManager> mockBuyerManager;
        [SetUp]
        public void SetUp()
        {
            mockBuyerManager = new Mock<IBuyerManager>();
            buyerController = new BuyerController(mockBuyerManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            buyerController = null;
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [TestCase(4526)]
        [TestCase(3252)]
        [Description("testing buyer Profile")]
        public async Task BuyerProfile_Successfull(int buyerId)
        {
            try
            {
                BuyerData buyer = new BuyerData();
                mockBuyerManager.Setup(x => x.GetBuyerProfile(buyerId)).ReturnsAsync(buyer);
                var result = await buyerController.GetBuyerProfile(buyerId);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [TestCase(452)]
        [TestCase(322)]
        [Description("testing buyer Profile failure")]
        public async Task BuyerProfile_UnSuccessfull(int buyerId)
        {
            try
            {
                mockBuyerManager.Setup(d => d.GetBuyerProfile(buyerId));
                var result = await buyerController.GetBuyerProfile(buyerId) as OkResult;
                Assert.That(result, Is.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [Description("testing buyer edit Profile")]
        public async Task EditBuyerProfile_Successfull()
        {
            try
            {
                mockBuyerManager.Setup(x => x.EditBuyerProfile(It.IsAny<BuyerData>())).ReturnsAsync(new Boolean());
                BuyerData buyer = new BuyerData() { buyerId = 6743, userName = "anvi", password = "abcdefg@", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var result = await buyerController.EditBuyerProfile(buyer) as OkObjectResult;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [Description("testing buyer edit Profile")]
        public async Task EditBuyerProfile_UnSuccessfull()
        {
            try
            {
                mockBuyerManager.Setup(x => x.EditBuyerProfile(It.IsAny<BuyerData>())).ReturnsAsync(new Boolean());
                BuyerData buyer = new BuyerData() { buyerId = 6746, userName = "anvi", password = "abcdefg@", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var result = await buyerController.EditBuyerProfile(buyer) as OkObjectResult;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
