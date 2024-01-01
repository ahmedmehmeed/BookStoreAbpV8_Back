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

namespace BookStore.Books;

public abstract class BookAppServiceTests<TStartupModule> : BookStoreApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IBookAppService _bookAppService;
    private readonly IAuthorAppService _authorAppService;

    protected BookAppServiceTests()
    {
        _bookAppService = GetRequiredService<IBookAppService>();
        _authorAppService = GetRequiredService<IAuthorAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Books()
    {
        //Act
        var result = await _bookAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Name == "1984");
    }

    [Fact]
    public async Task Should_Create_A_Valid_Book()
    {
        //Act
        var author = await _authorAppService.CreateAsync(
    new CreateAuthorDto
    {
        Name = "Ahmed",
       ShortBio="Writer",
       BirthDate=new DateTime(1997,10,1)
    });

        var result = await _bookAppService.CreateAsync(
            new CreateUpdateBookDto
            {
                Name = "New test book 42",
                Price = 10,
                PublishDate = DateTime.Now,
                Type = BookType.ScienceFiction,
                AuthorId = author.Id
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("New test book 42");
        result.AuthorId.ShouldBe(author.Id);
    }

    [Fact]
    public async Task Should_Not_Create_A_Book_Without_Name()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    Name = "",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.ScienceFiction
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
    }


}
