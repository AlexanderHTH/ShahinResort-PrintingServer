using Xunit;
using PrintingServer.Application.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace PrintingServer.Application.Extentions.Tests
{
    public class BaseTests
    {
        [Theory()]
        [InlineData("10.10.10.10")]
        [InlineData("127.0.0.1")]
        [InlineData("192.168.1.10")]
        public void BeAValidIpTest_OK(string ip)
        {
            var check = Base.BeAValidIp(ip);
            check.Should().BeTrue();
        }
        [Theory()]
        [InlineData("10.10.10")]
        [InlineData("10.10.10.")]
        [InlineData("127.0.0.300")]
        [InlineData("127.0.300.1")]
        [InlineData("127.300.0.1")]
        [InlineData("192.168.1.")]
        [InlineData("12a.300.0.1")]
        [InlineData("127.a.0.1")]
        [InlineData("127.300.ra.r4")]
        public void BeAValidIpTest_Bad(string ip)
        {
            var check = Base.BeAValidIp(ip);
            check.Should().BeFalse();
        }
    }
}