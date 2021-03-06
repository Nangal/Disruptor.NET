using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Disruptor;

namespace Disruptor.Test
{
	[TestFixture]
	public class UtilTest
	{
	    private MockRepository _mocks;
	
		[SetUp]
		public void SetUp()
		{
			_mocks = new MockRepository();
		}
		
	    [Test]
	    public void ShouldReturnNextPowerOfTwo()
	    {
	        int powerOfTwo = Util.CeilingNextPowerOfTwo(1000);
	
	        Assert.AreEqual(1024, powerOfTwo);
	    }
	
	    [Test]
	    public void ShouldReturnExactPowerOfTwo()
	    {
	        int powerOfTwo = Util.CeilingNextPowerOfTwo(1024);
	
	        Assert.AreEqual(1024, powerOfTwo);
	    }
	
	    [Test]
	    public void ShouldReturnMinimumSequence()
	    {
	        IConsumer[] consumers = new IConsumer[3];
	        consumers[0] = _mocks.DynamicMock<IConsumer>();
	        consumers[1] = _mocks.DynamicMock<IConsumer>();
	        consumers[2] = _mocks.DynamicMock<IConsumer>();
	
			Expect.Call(consumers[0].Sequence).Return(7L);
			Expect.Call(consumers[1].Sequence).Return(3L);
			Expect.Call(consumers[2].Sequence).Return(12L);
	
			_mocks.ReplayAll();
			
	        Assert.AreEqual(3L, Util.GetMinimumSequence(consumers));
			
			_mocks.VerifyAll();
	    }
	
		[Test]
	    public void ShouldReturnLongMaxWhenNoConsumers()
	    {
	        IConsumer[] consumers = new IConsumer[0];
	        Assert.AreEqual(long.MaxValue, Util.GetMinimumSequence(consumers));
	    }
	}
}

