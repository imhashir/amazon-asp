USE myAmazonV2

--Category

set identity_insert Category ON
INSERT INTO Category (id,Name) VALUES (25,'Shoes')
INSERT INTO Category (id,Name) VALUES (26,'Laptops')
INSERT INTO Category (id,Name) VALUES (27,'Electronics')
INSERT INTO Category (id,Name) VALUES (28,'Appliances')
INSERT INTO Category (id,Name) VALUES (29,'Furniture')
INSERT INTO Category (id,Name) VALUES (30,'Sports')
INSERT INTO Category (id,Name) VALUES (31,'Vehicles')
INSERT INTO Category (id,Name) VALUES (32,'Mobiles')
INSERT INTO Category (id,Name) VALUES (33,'Clothing')
INSERT INTO Category (id,Name) VALUES (34,'Arts & Cosmetics')
INSERT INTO Category (id,Name) VALUES (35,'Real Estate')
set identity_insert Category OFF

--Brand

set identity_insert Brand ON
INSERT INTO Brand (id,Name,CategoryId) VALUES (25,'Apple',26)
INSERT INTO Brand (id,Name,CategoryId) VALUES (26,'Lenovo',26)
INSERT INTO Brand (id,Name,CategoryId) VALUES (27,'Stylo Shoes',25)
INSERT INTO Brand (id,Name,CategoryId) VALUES (28,'Metro Shoes',25)
INSERT INTO Brand (id,Name,CategoryId) VALUES (29,'LG',27)
INSERT INTO Brand (id,Name,CategoryId) VALUES (30,'Dawlence',28)
INSERT INTO Brand (id,Name,CategoryId) VALUES (31,'Woodmart',29)
INSERT INTO Brand (id,Name,CategoryId) VALUES (32,'HomeDesire Furniture',29)
INSERT INTO Brand (id,Name,CategoryId) VALUES (33,'Adidas',30)
INSERT INTO Brand (id,Name,CategoryId) VALUES (34,'Nike',30)
INSERT INTO Brand (id,Name,CategoryId) VALUES (35,'Toyota',31)
INSERT INTO Brand (id,Name,CategoryId) VALUES (36,'Honda',31)
INSERT INTO Brand (id,Name,CategoryId) VALUES (37,'Samsung',32)
INSERT INTO Brand (id,Name,CategoryId) VALUES (38,'Huawei',32)
INSERT INTO Brand (id,Name,CategoryId) VALUES (39,'Apple',32)
INSERT INTO Brand (id,Name,CategoryId) VALUES (40,'Levis',33)
INSERT INTO Brand (id,Name,CategoryId) VALUES (41,'Crossroads',33)
INSERT INTO Brand (id,Name,CategoryId) VALUES (42,'Garnier',34)
INSERT INTO Brand (id,Name,CategoryId) VALUES (43,'Lux',34)
INSERT INTO Brand (id,Name,CategoryId) VALUES (44,'LDA',35)
INSERT INTO Brand (id,Name,CategoryId) VALUES (45,'Bahria Group',35)
set identity_insert Brand OFF

--Product
SELECT * FROM Product
set identity_insert Product ON
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (25,'Macbook Pro',25,26,111000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (26,'Macbook',25,26,98000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (27,'Black Sneakers',28,25,24000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (28,'Cansual Sandals',27,25,5000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (29,'Ideapad 310',26,26,120000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (30,'Air Conditioner',30,28, 85000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (31,'Living Soffas',31,29,50000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (32,'52" LED TV',29,27,59800)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (33,'Tennis Racket',33,30,2500)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (34,'Premium Bat',35,30,3450)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (35,'Cricket Trousers',33,30,1100)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (36,'Toyota Atlas',35,31,1200000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (37,'Toyota Hybrid',35,31,1800000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (38,'Honda Accord',36,31,2000000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (39,'Honda Civic 2017',36,31,1500000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (40,'Galaxy S7',37,32,75000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (41,'Galaxy S6 Edge',37,32,65000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (42,'Galaxy J7 Prime',37,32,32000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (43,'P9 Lite',38,32,45000)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (44,'Iphone 6s',39,32,86700)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (45,'Blue Shirt',40,33,500)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (46,'Black Jeans',41,33,900)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (47,'Brown Coat',41,33,1500)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (48,'Daily Facewash',43,34,200)
INSERT INTO Product (id,Name,BrandID,CategoryId,Price) VALUES (49,'Fairness Cream Men',42,34,150)
set identity_insert Product OFF

--Customer

INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('messi','lionelmessi','Lionel','Messi','1-1-2001',0300,'m10@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('iniesta','andresiniesta','Andres','Iniesta','1-1-2001',03001,'iniesta8@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('pirlo','andreapirlo','Andrea','Pirlo','1-1-2001',030012,'pirlo23@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('xavi','xavihernandez','Xavi','Hernandez','1-3-2001',0300123,'xavi6@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('schweini','schweini','Bastain','Schweinsteiger','4-1-2001',0400,'schweinsteiger@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('hazard','edenhazard','Eden','Hazard','2-3-2004',0100,'hazard10@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('eriksen','christian','Christian','Eriksen','9-5-2010',0900,'eriksen23@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('ibra','zlatanibra','Zlatan','Ibrahimovic','2-2-2001',02300,'ibra9@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('bale','garethbale','Gareth','Bale','1-1-2001',0300,'bale11@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('deBruyne','kevindebruyne','Kevin De','Bruyne','1-1-2001',0300,'bruyne8@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('ozil','mesutozil','Mesut','Ozil','1-11-2001',03400,'ozil10@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('coutinho','philippe','Philippe','Coutinho','1-10-2001',03100,'coutinho8@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('gotze','mariogotze','Mario','Gotze','11-1-2001',03100,'gotze21@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('gerrard','stevengerrard','Steven','Gerrard','11-1-2001',00300,'stevie@gmail.com')
INSERT INTO Customer (Username,Password,FirstName, LastName,JoinDate,ContactNumber,Email) VALUES ('cryuff','johancryuff','Johan','Cryuff','11-11-2011',01100,'father_of_football@gmail.com')
GO

--Account
INSERT INTO Accounts VALUES(100,'messi')
INSERT INTO Accounts VALUES(200,'iniesta')
INSERT INTO Accounts VALUES(400,'pirlo')
INSERT INTO Accounts VALUES(500,'xavi')
INSERT INTO Accounts VALUES(99,'schweini')
INSERT INTO Accounts VALUES(900,'hazard')
INSERT INTO Accounts VALUES(600,'eriksen')
INSERT INTO Accounts VALUES(2000,'ibra')
INSERT INTO Accounts VALUES(1000,'bale')
INSERT INTO Accounts VALUES(3000,'debruyne')
INSERT INTO Accounts VALUES(4000,'ozil')
INSERT INTO Accounts VALUES(2000,'coutinho')
INSERT INTO Accounts VALUES(9999,'gotze')
INSERT INTO Accounts VALUES(1111,'gerrard')
INSERT INTO Accounts VALUES(200,'cryuff')
INSERT INTO [Accounts] VALUES(333000, 'imhashir')

--Wishlist
INSERT INTO Wishlist VALUES(25,25,'messi','2015-1-1')
INSERT INTO Wishlist VALUES(26,25,'iniesta','2017-2-25')
INSERT INTO Wishlist VALUES(27,26,'pirlo','2017-2-25')
INSERT INTO Wishlist VALUES(28,27,'xavi','2016-3-26')
INSERT INTO Wishlist VALUES(29,28,'schweini','2015-4-26')
INSERT INTO Wishlist VALUES(30,30,'hazard','2014-5-26')
INSERT INTO Wishlist VALUES(31,30,'eriksen','2014-5-27')
INSERT INTO Wishlist VALUES(32,30,'ibra','2014-5-27')
INSERT INTO Wishlist VALUES(33,30,'bale','2013-5-27')
INSERT INTO Wishlist VALUES(34,30,'debruyne','2013-5-27')
INSERT INTO Wishlist VALUES(35,31,'ozil','2013-5-27')
INSERT INTO Wishlist VALUES(36,32,'coutinho','2013-5-28')
INSERT INTO Wishlist VALUES(37,33,'gotze','2013-5-28')
INSERT INTO Wishlist VALUES(38,34,'gerrard','2013-5-26')
INSERT INTO Wishlist VALUES(39,35,'cryuff','2013-6-25')
INSERT INTO Wishlist VALUES(40,36,'xavi','2013-12-26')

[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
[ProductId] INT FOREIGN KEY REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE CASCADE,
[CustomerId] VARCHAR(15) FOREIGN KEY REFERENCES Customer(Username) ON DELETE NO ACTION ON UPDATE CASCADE,
[DateOfOrder] DATE,
[status] VARCHAR(8) CHECK([status]='PENDING' OR [status]='SOLD'),
Quantity INT


--Order
set identity_insert [Order] ON
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity]) VALUES (25,25,'messi','2015-11-25','SOLD',20)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (26,25,'iniesta','2015-12-25','SOLD',30)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (27,26,'iniesta','2015-11-25','SOLD',30)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (28,26,'iniesta','2015-10-25','SOLD',50)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (29,26,'iniesta','2015-9-25','SOLD', 60)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (30,26,'pirlo','2015-5-25','PENDING',70)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (31,26,'xavi','2015-4-28','PENDING',80)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (32,27,'hazard','2015-3-27','PENDING',90)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (33,28,'ibra','2015-2-26','PENDING',100)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (34,29,'ozil','2017-1-26','PENDING',110)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (35,30,'gotze','2017-1-25','PENDING',120)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (36,31,'gotze','2016-2-23','PENDING',130)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (37,32,'cryuff','2016-3-20','PENDING',140)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (38,32,'bale','2016-4-16','PENDING',150)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (39,32,'bale','2016-5-12','PENDING',160)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (40,33,'schweini','2010-1-1','PENDING',170)
INSERT INTO [Order]([id], [ProductId], [CustomerId], [DateOfOrder], [Quantity])  VALUES (42,52,'imhashir','2010-1-1','SOLD',1)
set identity_insert [Order] OFF

--Quantity starts here
INSERT INTO Quantity VALUES(25,Null,1000)
INSERT INTO Quantity VALUES(26,Null,1500)
INSERT INTO Quantity VALUES(27,Null,2000)
INSERT INTO Quantity VALUES(28,Null,3000)
INSERT INTO Quantity VALUES(29,Null,1000)
INSERT INTO Quantity VALUES(30,Null,1500)
INSERT INTO Quantity VALUES(31,Null,2000)
INSERT INTO Quantity VALUES(32,Null,1500)
INSERT INTO Quantity VALUES(33,Null,300)
INSERT INTO Quantity VALUES(34,Null,400)
INSERT INTO Quantity VALUES(35,Null,400)
INSERT INTO Quantity VALUES(36,Null,500)
INSERT INTO Quantity VALUES(37,Null,500)
INSERT INTO Quantity VALUES(38,Null,500)
INSERT INTO Quantity VALUES(39,Null,500)
INSERT INTO Quantity VALUES(40,Null,500)

---Rating

INSERT INTO [Rating] VALUES (25,'messi',4)
INSERT INTO [Rating] VALUES (25,'bale',3)
INSERT INTO [Rating] VALUES (26,'schweini',1)
INSERT INTO [Rating] VALUES (26,'ibra',5)
INSERT INTO [Rating] VALUES (41,'hazard',2)
INSERT INTO [Rating] VALUES (26,'eriksen',4)
INSERT INTO [Rating] VALUES (35,'hazard',3)
INSERT INTO [Rating] VALUES (27,'ozil',2)
INSERT INTO [Rating] VALUES (45,'iniesta',5)
INSERT INTO [Rating] VALUES (29,'gerrard',5)
INSERT INTO [Rating] VALUES (30,'gotze',2)
INSERT INTO [Rating] VALUES (31,'messi',5)
INSERT INTO [Rating] VALUES (32,'ibra',4)
INSERT INTO [Rating] VALUES (32,'schweini',3)
INSERT INTO [Rating] VALUES (32,'bale',4)
INSERT INTO [Rating] VALUES (33,'eriksen',5)

GO

---Comment
INSERT INTO [Comment] VALUES (25,'messi',NULL)
INSERT INTO [Comment] VALUES (25,'bale',NULL)
INSERT INTO [Comment] VALUES (26,'schweini',NULL)
INSERT INTO [Comment] VALUES (26,'ibra',NULL)
INSERT INTO [Comment] VALUES (26,'hazard',NULL)
INSERT INTO [Comment] VALUES (26,'eriksen',NULL)
INSERT INTO [Comment] VALUES (26,'hazard',NULL)
INSERT INTO [Comment] VALUES (27,'ozil',NULL)
INSERT INTO [Comment] VALUES (28,'iniesta',NULL)
INSERT INTO [Comment] VALUES (29,'gerrard',NULL)
INSERT INTO [Comment] VALUES (30,'gotze',NULL)
INSERT INTO [Comment] VALUES (31,'messi',NULL)
INSERT INTO [Comment] VALUES (32,'ibra',NULL)
INSERT INTO [Comment] VALUES (32,'schweini',NULL)
INSERT INTO [Comment] VALUES (32,'bale',NULL)
INSERT INTO [Comment] VALUES (33,'eriksen',NULL)

--Admin
INSERT INTO [Admin](Username) VALUES('imhashir')