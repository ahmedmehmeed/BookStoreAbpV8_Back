using BookStore.Books;
using Xunit;


namespace BookStore.EntityFrameworkCore.Applications.Books;





[Collection(BookStoreTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppServic : BookAppServiceTests<BookStoreEntityFrameworkCoreTestModule>
{

}