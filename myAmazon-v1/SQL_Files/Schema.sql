--DROP DATABASE myAmazonV2
--CREATE DATABASE myAmazonV2
USE myAmazonV2
GO

CREATE TABLE Category (
[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[Name] VARCHAR(30) NOT NULL
)
GO

CREATE TABLE CategoryInfo (
[CategoryId] INT FOREIGN KEY REFERENCES Category(id) ON DELETE CASCADE ON UPDATE CASCADE,
[Desc] VARCHAR(80),
[Image] VARCHAR(80)
)
GO

CREATE TABLE Brand (
[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[Name] VARCHAR(30) NOT NULL
)
ALTER TABLE Brand ADD [CategoryId] INT FOREIGN KEY REFERENCES Category(id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

CREATE TABLE BrandInfo (
[BrandId] INT FOREIGN KEY REFERENCES Brand(id) ON DELETE CASCADE ON UPDATE CASCADE,
[Desc] VARCHAR(80),
[Image] VARCHAR(80)
)
GO
CREATE TABLE Product (
[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[Name] VARCHAR(30) NOT NULL,
[BrandId] INT FOREIGN KEY REFERENCES Brand(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CategoryId] INT FOREIGN KEY REFERENCES Category(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[Price] INT NOT NULL
)
ALTER TABLE Product ADD [DateAdded] DATE default GETDATE()
GO
CREATE TABLE ProductInfo(
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE CASCADE ON UPDATE CASCADE,
[Desc] VARCHAR(50),
[Image] VARCHAR(80)
)
GO
CREATE TABLE UserProducts (
[ProductId] INT NOT NULL,
[CustomerId] VARCHAR(15) NOT NULL
)

GO
CREATE TABLE Quantity (
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE CASCADE ON UPDATE CASCADE,
[Sold] INT,
[Stock] INT
)
GO

DROP TABLE Customer 
CREATE TABLE Customer (
[Username] VARCHAR(15) PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[Password] VARCHAR(15),
[FirstName] VARCHAR(20),
[LastName] VARCHAR(20),
[JoinDate] DATE,
[ContactNumber] VARCHAR(15),
[Email] VARCHAR(30),
[Image] VARCHAR(50)
)
GO

Create table Accounts
(
Amount int,
[UserName] varchar(15) Foreign key references Customer([Username])  ON DELETE NO ACTION ON UPDATE CASCADE Primary key
)

CREATE TABLE [Order] (
[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer(Username) ON DELETE NO ACTION ON UPDATE CASCADE,
[DateOfOrder] DATE,
[status] VARCHAR(8) CHECK([status]='PENDING' OR [status]='SOLD'),
Quantity INT
)
ALTER TABLE [Order] ADD idnew INT PRIMARY KEY IDENTITY(1,1)
ALTER TABLE [Order] DROP COLUMN id
EXEC sp_rename '[Order].idnew', 'id', 'COLUMN'
GO

CREATE TABLE [Wishlist] (
[ProductId] INT NOT NULL FOREIGN KEY REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CustomerId] VARCHAR(15) NOT NULL FOREIGN KEY REFERENCES Customer(Username) ON DELETE NO ACTION ON UPDATE CASCADE,
[WishDate] DATE
)
ALTER TABLE [Wishlist] ADD CONSTRAINT PK_PidCid PRIMARY KEY ([ProductId], [CustomerId])
GO

CREATE TABLE [Comment] (
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer(Username) ON DELETE NO ACTION ON UPDATE CASCADE,
[text] VARCHAR(50)
)
GO

CREATE TABLE [Rating] (
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer(Username) ON DELETE NO ACTION ON UPDATE CASCADE,
[Rate] INT CHECK(Rate >= 1 AND RATE <= 5)
)
GO

DROP TABLE [Admin]
CREATE TABLE [Admin] (
[Username] VARCHAR(15) PRIMARY KEY,
)
GO

DROP TABLE Featured
CREATE TABLE Featured (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE CASCADE ON UPDATE CASCADE,
[Level] INT CHECK([Level] >= 1 AND [Level] <= 3),
[CoverImage] VARCHAR(50) NOT NULL
)

CREATE TABLE Requests (
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer([Username]) ON DELETE CASCADE ON UPDATE CASCADE,
[Amount] INT
)

CREATE TABLE ProductRequests (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer([Username]) ON DELETE CASCADE ON UPDATE CASCADE,
[Desc] VARCHAR(100),
[DateOfRequest] DATE
)