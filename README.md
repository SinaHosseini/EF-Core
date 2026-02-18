
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

### Points
---

> - To add foreign key in a model, should create object from that model as list or single (for one to many or many to many).

for example:

`public Order Order { get; set; }`

    Order is a class and for foreign key we must create property with -class's name- + Id
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
        

     2. ```
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