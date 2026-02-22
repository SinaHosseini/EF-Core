
# Entity Framework core

## Migration
```
Add-Migration --NAME_MIGRATION--
```
This command is for create migration to database, this is necessary.
```
Update-Database --OPTIONAL_NAME--
```
This command is for create tables and relations in database with migration's name, OPTIONAL_NAME is for to back past migration.
```
Script-Migration
```
This command is for create sql script to run and create database structures with T-SQL.

## Model Configure
---

To add foreign key in a model, should create object from that model as list or single (for one to many or many to many).

for example:

`public Order Order { get; set; }`

Order is a class and for foreign key we must create property with -class's name- + Id<br>
`public int OrderId { get; set; }`

---
- 
     1. **[NOTMAPPED]** isn't that for database but we can use in program.

     2. **[Table("--TABLE_NAME--", Schema = "--SCHEMA_NAME--")]**
    is for create table with another name and different schema (dbo).

     3. **[Column("--COLUMN_NAME--", TypeName = "--DATA_TYPE_IN_DATABASE--")]** is for change name in database and define data type.

     4. **[MaxLength]** is for make limit length.

     5. **[Required]** make not null column in database.

     6. **[Key]** is for make primary key.

     7. **[DatabaseGenerated(DatabaseGeneratedOption.None/Identity/Computed)]** is for indexing automatic or manual.

     8. **[Index(nameof(--COLUMN_NAME--), IsUnique = true)]** this is ti create index and make it unique.

---

-    1. **[ForeignKey("--TABLE_NAME_OR_COLUMN_NAME")]** is for create relation and foreign key to another table.
    <br>for example:<br>
        `[ForeignKey("Order")] public int OrderId { get; set; }`
        <br> or <br>
        `[ForeignKey("OrderId")] public required Order Order { get; set; }`
        <br> and create object from that model, example:<br>
        `public required Order Order { get; set; }`
        <br> also create list in parent class, example:<br>
        `public List<OrderItem> OrderItems { get;set; }`
        

     2. ```c#
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
        ```
        this code disables Cascade Delete for all tables and relationships in the database.

## Fluent API

1. We have to create .cs file with name --TENTITY--Map and inheritance class from `IEntityTypeConfiguration<TEntity>` and make function `Configure(EntityTypeBuilder<TEntity> builder)`.<br>
for example:
    ```c#
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            throw new NotImplementedException();
        }
    }
    ```
2. to change table name and schema, write this code:
    ```c#
    builder.ToTable("--TABLE-NAME--", "--TABLE_SCHEMA--");
    ```

3. column attribute:
    - **`HasIndex()`** to create index column.
    - **`HasKey()`** to set primary key.
    - **`Ignore()`** to ignore property in database.
    - **`Property()`** to use property for attribute.
    - **`IsRequired()`** to not allow null column in database.
    - **`MaxLength()`** to make limit length.
    - **`HasNoKey()`** set nothing key in table.
    - **`HasDefaultValue()`** to set value for property, if it be null.
    - **`IsUnicode()`** to set varchar instead nvarchar.
    - **`HasColumnName()`** to change name in database.
    - **`HasColumnType()`** to change column type in database.
    - **`HasData()`** to add data in database when migrate.
    - **`HasConversion()`** this method use to convert data to database. usage example: 
        ```c#
        public class Product
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ImageName { get; set; }
            public string ProductDescription { get; set; }
            public List<string> Tags { get; set; } // this our conversion property

            public List<UserProduct> UserProducts { get; set; }
        }
        ```
        then configure in *ProductMap.cs* like this:
        ```c#
        builder.Property(b => b.Tags)
            .HasConversion(
            data => JsonSerializer.Serialize(data),
            data => JsonSerializer.Deserialize<List<string>>(data));
        ```
        or for enums we can write like this:
        ```c#
        public class Order
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public DateTime OrderTime { get; set; }
            public OrderStatus Status { get; set; }

            public User User { get; set; }
            public OrderAddress OrderAddress { get; set; }
            public List<OrderItem> OrderItems { get;set; }
        }

        public enum OrderStatus
        {
            IsPay,
            Canceled,
            Finally
        }
        ```
        ```c#
        // first way, manually
        builder.Property(b => b.Status)
            .HasConversion(v => v.ToString(),
            v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
        // second way, smart
        builder.Property(b => b.Status)
            .HasConversion<string>();
        // third way, ef core type
        builder.Property(b => b.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>());
        // forth way, create value 
        var conversion = new ValueConverter<OrderStatus, string>(
            data => data.ToString(),
            data => (OrderStatus)Enum.Parse(typeof(OrderStatus), data));

        builder.Property(b => b.Status)
            .HasConversion(conversion);
        ``` 
        > to more info read [Microsoft EntityFrameWork core value convertor](https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#built-in-converters)

    - **`HasQueryFilter()`** to apply global filter.
        

or write code in AppDBContext with lambada, for example:
```c#
modelBuilder.Entity<User>(config =>
{
    config.HasKey(c => c.UserId);
    config.Property(c => c.FullName)
    .HasDefaultValue("[Name] + ' ' + [Family]");
});
```
or 
```c#
modelBuilder.Entity<User>()
    .Property(b => b.Name).IsRequired();
```

but if we mapped class in another file we have to call that like this for single:
`modelBuilder.ApplyConfiguration(new --TENTITY--Map());`<br>
or call all maps one command:
`modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);`

4. `modelBuilder.HasDefaultSchema("DBO");` is for set default schema.

5. this code set a relation one to many in database.
    ```c#
        builder.HasOne(b => b.TEntity)
        .WithMany(b => b.TEntity)
        .HasForeignKey(b => b.ColumnId);
    ```

    also for one to one relation we can use this code:
    ```c#
    builder.HasOne(b => b.TEntity)
    .WithOne(b => b.TEntity)
    .HasForeignKey<TEntity>(b => b.ColumnId);
    ```

    > **`OnDelete(DeleteBehavior.Restrict/Cascade/NoAction/SetNull)`** determines how it should behave when its parent is deleted. it usually come after .HasForeignKey(). example:<br>
    `builder.HasOne(b => b.Order).WithMany(b => b.OrderItems).HasForeignKey(b => b.OrderId).OnDelete(DeleteBehavior.Cascade);`

## Crud
For crud to database(insert, update, delete, select) at first we have to create object from AppDBContext like this: 
```c#
var context = new AppDBContext();
```

1. for add data we use this command:
    ```c#
    context.TEntity.Add();
    ```
    and in **`Add()`** method we create `new TEntity() {--DATA--}` then call `context.SaveChanges()` to add in database.
    > TEntity is optional, we can pas entity in method, like: `context.Add(new TEntity() {--DATA--})`

2. EF Core attribute for select from database:
     - **`.ToList()`** get all data from database. usage example: 
    ```c#
    var products = context.Products.ToList();
    ```
     - **`Find()`** get one data from database  by one attribute in find, usage example:
    ```c#
    var product = context.Products.Find(1);
    ```
    - **`First()`** this method search in database and return first data, if data doesn't exit, return exception. also we can use condition in First to find intended data. usage example: 
    ```c#
    var product = context.Products.First(f => f.Name == "Mobile")
    ```
    - **`FirstOrDefault()`** it's like `First()` but when doesn't find data, return null.
    - **`Single()`** this method search in database and if doesn't find data or find 1 more data from intended data return exception.
    - **`SingleOrDefault`** this is like `Single()` but if doesn't find data, return null.
    
3. for delete from database, use this command:
    ```c#
    context.TEntity.Remove(TEntity);
    ```
    and for TEntity in remove, we should get it before it from Find().
    after that we should call `context.SaveChange()`.
    <!-- > also we can use `Remove()` like this: `context.Remove(TEntity)`. -->

4. update in ef core is like delete, but before update we can change data, after that we should call `context.SaveChange()`. usage command:
    ```c#
    context.TEntity.Update(TEntity);
    ```

5. to add a list to database we use `AddRange()`. usage example:
    ```c#
    var products = new List<Product>(
        {
            new Product
            { --DATA-- },

            new Product
            { --DATA-- }
        }
    );

    context.Product.AddRange(products);
    context.SaveChange();
    ```

6. **`IgnoreQueryFilter()`** to ignore global query filter.

## Eager Loading

in this loading we use 2 methods with name `Include()` and `ThenInclude()`.

1. at first we create object from our AppDBContext:
    ``` c#
    using var context = new AppDBCContext();
    ```

2. we use methods to make joins and get data.
- **`Include()`** this method make `INNER JOIN` to some table with ForeignKey we made before. usage example:
    ```c#
    var userProducts = context.UserProducts
        .Include(c => c.User)
        .Include(c => c.Product)
        .ToList();
    ```
- **`ThenInclude()`** we use ThenInclude() to connect to the third table, which is in our second table, with which we made a JOIN. Usage example:
    ```c#
    var userProducts = context.UserProducts
        .Include(c => c.User)
            .ThenInclude(c => c.Order)
        .Include(c => c.Product)
        .ToList();
    ```
- also we can make condition to get intended data. usage example:
    ```c#
    var userProducts = context.UserProducts
        .Include(c => c.OrderItems.Where(r => r.IsPay == true))
        .ToList();
    ```

## Explicit Loading

1. at first we create object from our AppDBContext:
    ``` c#
    using var context = new AppDBCContext();
    ```

2. Explicit loading attribute:
- **`Entry()`** get our entity to load data. this is our entry.
- **`Reference()`** is for single navigation property. like: `public Order Order { get; set; }`
- **`Collection()`** is for collection navigation property. like: `public ICollection<Order> Orders { get; set; }`
- **`Load()`** load data after choose type relation. like: `context.Entry(author).Collection(a => a.Posts).Load();
- **`Query()`** we can filter (`Where()`), sort (`OrderBy()`), pagination (`Skip()` or `Take()`) or projection (`Select()`).
    > we can use `Load()` after that or put it into a variable.

## Lazy Loading
1. In first way we can install package `Microsoft.EntityFrameWorkCore.Proxies`.
then we have to add <u>*virtual*</u> to models relations.
this way is so heavy and load all data relations and no recommended.

2. in second way we add ILazyLoader to our model and create 2 ctor, one with no action and second fill _lazy loader. then we set get and set manually. usage example:
    ```c#
    public class UserProduct
    {
        private Product _product;
        private ILazyLoader _lazyLoader;

        public UserProduct() { }    

        public UserProduct(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public Product Product
        {
            get => _lazyLoader.Load(this, ref _product);
            set => _product = value;
        }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
    ```
---
### IQueryable vs IEnumerable
The difference between `IEnumerable` and `IQueryable` is that `IEnumerable` executes commands logically on the <u>client side</u>, and `IQueryable` converts the written commands into <u>SQL queries</u> and then executes them.

---

### Change Tracker
this tracker come all data in memory by indexing and do crud with its own things, to off this we can use **`AsNoTracking()`** in queries or off global by **`optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);`** command in AppDbContext.

---

## Database First
this method use when we create database firs.
to add models and DbContext to program use this command in terminal.
```
Scaffold-DbContext "--CONNECTION_STRING" --PROVIDER-- -outputDir --FOLDER_NAME_MODELS--
```
like: `Scaffold-DbContext 'Server=ServerName;Database=DatabaseName;User Id=YourUserId;Password=YourPassword; Trusted_Connection=True; TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`