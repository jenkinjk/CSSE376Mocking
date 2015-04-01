using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
            var target = ObjectMother.Saab();
            var BMW = ObjectMother.BMW();
            Assert.AreEqual(BMW.getBasePrice(), 10 * 10 * .8);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [TestMethod]
        public void TestGetCarLocation()
        {
            IDatabase mockDB = mocks.DynamicMock<IDatabase>();
            String LocationOne = "3345 Peddleton Ave";
            String LocationTwo = "3028 Francis Dr";

            Expect.Call(mockDB.getCarLocation(1)).Return(LocationOne);
            Expect.Call(mockDB.getCarLocation(2)).Return(LocationTwo);
            mocks.ReplayAll();
           
            var target = new Car(1);
            target.Database = mockDB;

            String Location = target.getCarLocation(1);
            Assert.AreEqual(Location, LocationOne);

            Location = target.getCarLocation(2);
             Assert.AreEqual(Location, LocationTwo);

             mocks.VerifyAll();
        }

        [TestMethod]
        public void TestGetCarMileage()
        {
            IDatabase mockDB2 = mocks.DynamicMock<IDatabase>();

            Expect.Call(mockDB2.Miles).PropertyBehavior();

            mocks.ReplayAll();

            var target = new Car(1);
            target.Database = mockDB2;

            mockDB2.Miles = 50;

            Assert.AreEqual(target.Mileage,50);

            mocks.VerifyAll();
        }
	}
}
