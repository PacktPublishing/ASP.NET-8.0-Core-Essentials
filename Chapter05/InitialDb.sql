CREATE DATABASE DbStore;
GO

Create Table Product (
    ID int PRIMARY KEY IDENTITY(1,1),
    Name varchar(100) NOT NULL,
    Price DECIMAL
)
GO 
INSERT INTO Product(NAme, Price) VALUES('Product 1', 10)
INSERT INTO Product(NAme, Price) VALUES('Product 2', 20)
INSERT INTO Product(NAme, Price) VALUES('Product 3', 100)
INSERT INTO Product(NAme, Price) VALUES('Product 4', 550)

GO