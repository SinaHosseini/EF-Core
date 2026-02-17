
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

> 1. To add foreign key in a model, should create object from that model as list or single (for one to many or many to many).

for example:

`public Order Order { get; set; }`

    Order is a class and for foreign key we must create property with -class's name- + Id
`public int OrderId { get; set; }`