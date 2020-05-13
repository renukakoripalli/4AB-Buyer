﻿using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Models;
using BUYERDBENTITY.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyerServiceTesting
{

    [TestFixture]
    public class BuyerRepositoryTesting
    {
        IBuyerRepository buyerRepository;

        [SetUp]
        public void SetUp()
        {
            buyerRepository = new BuyerRepository(new BuyerdataContext());
        }

        [TearDown]
        public void TearDown()
        {
            buyerRepository = null;
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
                var result = await buyerRepository.GetBuyerProfile(buyerId);
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.GetBuyerProfile(buyerId));
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
        [TestCase(458)]
        [TestCase(322)]
        [Description("testing buyer Profile failure")]
        public async Task BuyerProfile_UnSuccessfull(int buyerId)
        {
            try
            {
                var result = await buyerRepository.GetBuyerProfile(buyerId);
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.GetBuyerProfile(buyerId));
                Assert.IsNull(result);
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
                BuyerData buyer = new BuyerData() { buyerId = 6743, userName = "anvi", password = "abcdefg@", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.EditBuyerProfile(buyer)).ReturnsAsync(true);
                var result = await buyerRepository.EditBuyerProfile(buyer);
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
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
                BuyerData buyer = new BuyerData() { buyerId = 674, userName = "anvi", password = "abcdefg@", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.EditBuyerProfile(buyer)).ReturnsAsync(false);
                var result = await buyerRepository.EditBuyerProfile(buyer);
                Assert.AreEqual(false, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}