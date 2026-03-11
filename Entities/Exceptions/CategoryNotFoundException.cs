namespace Entities.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int categoryId) : base($"The company with id: {categoryId} doesn't exist in the database.")
        {
        }
    }
}
