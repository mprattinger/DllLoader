using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CrmLoginPlugin.Tests
{
    public class LoginProcessorTests
    {
        [Fact]
        public async Task TestDeleteLogin()
        {
            var sut = new LoginProcessor();
            sut.TestMode = true;

            var should = new List<LoginDataModel>();
            should.Add(new LoginDataModel { Country = "UK", Sys = "WCD", Username = "ukforchr".ToUpper(), Password = "12345678"});
            should.Add(new LoginDataModel { Country = "UK", Sys = "WCD", Username = "ukcastra".ToUpper(), Password = "12345678" });
            sut.Logins = should;

            var arg = new List<string>();
            arg.Add("ukforchr");
            arg.Add("wcd");
            await sut.DeleteLogin(arg.ToArray());

            sut.Logins.Count.Should().Be(1);
            sut.Logins.First().Username.Should().Be("ukcastra".ToUpper());
            sut.Logins.First().Country.Should().Be("UK");
            sut.Logins.First().Sys.Should().Be("WCD");
        }

        [Fact]
        public async Task TestAddLogin()
        {
            var sut = new LoginProcessor();
            sut.TestMode = true;

            var should = new List<LoginDataModel>();
            should.Add(new LoginDataModel { Country = "UK", Sys = "WCD", Username = "ukforchr".ToUpper(), Password = "12345678" });
            sut.Logins = should;

            var arg = new List<string>();
            arg.Add("wcd");
            arg.Add("uk");
            arg.Add("ukcastra");
            arg.Add("12345678");
            await sut.AddLogin(arg.ToArray());

            sut.Logins.Count.Should().Be(2);
            sut.Logins.Last().Username.Should().Be("ukcastra".ToUpper());
            sut.Logins.Last().Country.Should().Be("UK");
            sut.Logins.Last().Sys.Should().Be("WCD");
        }

        [Fact]
        public async Task TestRemoveAll()
        {
            var sut = new LoginProcessor();
            var fi = await sut.Init();

            fi.Exists.Should().BeTrue();
            await sut.RemoveAll();

            fi.Exists.Should().BeFalse();
        }
    }
}
