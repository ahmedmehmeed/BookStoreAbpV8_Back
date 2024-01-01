using BookStore.Authors;
using Castle.Components.DictionaryAdapter;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace BookStore.Authors;

    public class AuthorAppServiceTests<TStartupModule> : BookStoreApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
    {

        private readonly IAuthorAppService _authorAppService;

        protected AuthorAppServiceTests()
        {
       
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Authors()
        {
            //Act
            var result = await _authorAppService.GetListAsync(
                new GetAuthorListDto()
            );

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "George Orwell");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Author()
        {
            //Act
            var author = await _authorAppService.CreateAsync(
        new CreateAuthorDto
        {
            Name = "Ahmed",
            ShortBio = "Writer",
            BirthDate = new DateTime(1997, 10, 1)
        });


        //Assert
          author.Id.ShouldNotBe(Guid.Empty);
        author.Name.ShouldBe("Ahmed");

        }

        [Fact]
        public async Task Should_Not_Create_A_Author_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _authorAppService.CreateAsync(
                    new CreateAuthorDto
                    {
                        Name = "",
                        ShortBio = "Writer",
                        BirthDate = new DateTime(1997, 10, 1)
                    }
                );
            });

            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }


    }
   
    

