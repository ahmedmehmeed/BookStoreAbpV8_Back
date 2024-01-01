using BookStore.Authors;
using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.EntityFrameworkCore.Applications.Authors;


[Collection(BookStoreTestConsts.CollectionDefinitionName)]
public class EfCoreAuthorAppService : AuthorAppServiceTests<BookStoreEntityFrameworkCoreTestModule>
{

}
