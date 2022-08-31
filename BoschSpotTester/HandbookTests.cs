using BoschSpot.Controllers;
using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories;
using BoschSpot.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BoschSpotTester
{
    public class HandbookTests
    {
        private HandbookController TestController;
        [SetUp]
        public void Setup()
        {
            var MockBirdRepo = new Mock<IContenderRepo>();
            MockBirdRepo.Setup(repo => repo.GetAll()).Returns(new List<ContenderModel>
            { new ContenderModel { ID = 1, GroupID = 1 }, new ContenderModel { ID = 2, GroupID = 2 } });
            var MockGroupRepo = new Mock<IGroupRepo>();
            MockBirdRepo.Setup(repo => repo.GetContendersOfGroup(1)).Returns(new List<ContenderModel>
            { new ContenderModel { ID = 1, GroupID = 1 } });
            var MockCategoryRepo = new Mock<ICategoryRepo>();
            TestController = new HandbookController(MockBirdRepo.Object, MockGroupRepo.Object, MockCategoryRepo.Object);
        }

        [Test]
        public void Should_Return_IndexView()
        {
            var result = TestController.Index() as ViewResult;

            Assert.IsTrue(result.ViewName == "Index");
        }

        [Test]
        public void Should_Return_BirdGroupView() 
        {
            var result = TestController.Groups(1) as ViewResult;

            Assert.IsTrue(result.ViewName == "Groups");
        }

        [Test]
        public void Should_Return_BirdGroupListView() 
        {
            var result = TestController.GroupList(1, 1) as ViewResult;

            Assert.IsTrue(result.ViewName == "GroupList");
        }

        [Test]
        public void Should_Return_BirdGroupListView_All_Birds() 
        {
            var result = TestController.GroupList(null, 1) as ViewResult;

            Assert.IsTrue(result.ViewName == "GroupList");
        }

        [Test]
        public void Should_Return_BirdView() 
        {
            var result = TestController.Contender(1) as ViewResult;

            Assert.IsTrue(result.ViewName == "Contender");
        }


        [Test]
        public void Should_Return_Birds_Of_Group() 
        {
            var result = TestController.GroupList(1, 1) as ViewResult;
            var model = result.Model as List<ContenderModel>;
            var count = model.Count;

            Assert.IsTrue(count == 1);
        }

        [Test]
        public void Should_Return_All_Birds() 
        {
            var result = TestController.GroupList(null, 1) as ViewResult;
            var model = result.Model as List<ContenderModel>;
            var count = model.Count;

            Assert.IsTrue(count == 2);
        }
    }
}