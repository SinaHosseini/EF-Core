using DataLayer;
using DomainLayer;

class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDBContext();

        //var userProducts = context.UserProducts.First(c => c.UserProductId == 1);

        //context.Entry(userProducts)
        //    .Reference(c => c.Product)
        //    .Load();
    }
}