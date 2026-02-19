
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

> - To add foreign key in a model, should create object from that model as list or single (for one to many or many to many).

for example:

`public Order Order { get; set; }`

Order is a class and for foreign key we must create property with -class's name- + Id<br>
`public int OrderId { get; set; }`

---
- 
    > 1. **[NOTMAPPED]** isn't that for database but we can use in program.

    > 2. **[Table("--TABLE_NAME--", Schema = "--SCHEMA_NAME--")]**
    is for create table with another name and different schema (dbo).

    > 3. **[Column("--COLUMN_NAME--", TypeName = "--DATA_TYPE_IN_DATABASE--")]** is for change name in database and define data type.

    > 4. **[MaxLength]** is for make limit length.

    > 5. **[Required]** make not null column in database.

    > 6. **[Key]** is for make primary key.

    > 7. **[DatabaseGenerated(DatabaseGeneratedOption.None/Identity/Computed)]** is for indexing automatic or manual.

    > 8. **[Index(nameof(--COLUMN_NAME--), IsUnique = true)]** this is ti create index and make it unique.

---

-   > 1. **[ForeignKey("--TABLE_NAME_OR_COLUMN_NAME")]** is for create relation and foreign key to another table.
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
    > - **`HasIndex()`** to create index column.
    > - **`HasKey()`** to set primary key.
    > - **`Ignore()`** to ignore property in database.
    > - **`Property()`** to use property for attribute.
    > - **`IsRequired()`** to not allow null column in database.
    > - **`MaxLength()`** to make limit length.
    > - **`HasNoKey()`** set nothing key in table.
    > - **`HasDefaultValue()`** to set value for property, if it be null.
    > - **`IsUnicode()`** to set varchar instead nvarchar.
    > - **`HasColumnName()`** to change name in database.
    > - **`HasColumnType()`** to change column type in database.

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

