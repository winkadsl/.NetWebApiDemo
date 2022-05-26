using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net.Http.Json;

using System.Net;

using WebApplication1.DataContext;
using Xunit;


namespace WebApplication1.Tests
{
    public class SchoolTests
    {
        [Fact(DisplayName = "�z�L Get �ާ@���� root API�A�w���^�� \"Hello ASP.NET Core WebApplication API~~~\" �C")]
        public async Task GetRootApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = "Hello ASP.NET Core WebApplication API~~~";

            //Act
            var actual = await client.GetStringAsync("/");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "�z�L Get �ާ@���� schools API�A�w���^���L����Ǯո�ơC")]
        public async Task GetSchoolsApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = new List<Models.School>();

            //Act
            var actual = await client.GetFromJsonAsync<List<Models.School>>("/schools");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "�z�L Post �ާ@���� addschool API�A�a�J School �� Json ��ơA�w���^�� Created�C")]
        public async Task PostSchoolApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = HttpStatusCode.Created;


            //Act
            var result = await client.PostAsJsonAsync("/addschool", new Models.School
                                                                        {
                                                                             Name = "��ߤ����j��",
                                                                             Logo = "nchu",
                                                                             Address = "402�x�����n�Ͽ��j��145��",
                                                                             Tel = "04-22873181",
                                                                             Email = "dowdot@nchu.edu.tw"
                                                                        });
            var actual = result.StatusCode;

            //Assert        
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "�z�L Get �ާ@���� findschool API�A�a�J Id �O 1 �w���^�� OK�C")]
        public async Task GetFindSchoolApi_Id_1_is_OK()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.OK;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.GetAsync($"/findschool/{Id}");

            //Assert
            Assert.Equal(expected , response.StatusCode);
        }

        [Fact(DisplayName = "�z�L Get �ާ@���� findschool API�A�a�J Id �O 3 �w���^�� NotFound�C")]
        public async Task GetFindSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.GetAsync($"/findschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact(DisplayName = "�z�L Put �ާ@���� editschool API�A�a�J Id �O 1 �w���^�� NoContent�C")]
        public async Task PutEditSchoolApi_Id_1_is_NoContent()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.NoContent;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.PutAsJsonAsync<Models.School>($"/editschool/{Id}",school);

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact(DisplayName = "�z�L Put �ާ@���� editschool API�A�a�J Id �O 3 �w���^�� NotFound�C")]
        public async Task PutEditSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.PutAsJsonAsync<Models.School>($"/editschool/{Id}", school);

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact(DisplayName = "�z�L Delete �ާ@���� removeschool API�A�a�J Id �O 1 �w���^�� NoContent�C")]
        public async Task DeleteRemoveSchoolApi_Id_1_is_NoContent()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.NoContent;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.DeleteAsync($"/removeschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact(DisplayName = "�z�L Delete �ާ@���� removeschool API�A�a�J Id �O 3 �w���^�� NotFound�C")]
        public async Task DeleteRemoveSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "��ߤ����j��",
                Logo = "nchu",
                Address = "402�x�����n�Ͽ��j��145��",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.DeleteAsync($"/removeschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }
    }

    class SchoolApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<MyDb>));

                services.AddDbContext<MyDb>(options => options.UseInMemoryDatabase("TestingDB", root));
            });

            return base.CreateHost(builder);
        }
    }
}