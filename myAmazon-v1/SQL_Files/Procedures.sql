USE myAmazonV2
GO

--Insert UPDATE  Brand
ALTER PROCEDURE UpdateBrand
@Id INT,
@name VARCHAR(30),
@categoryId INT,
@updateType BIT,     -- 0 is for INSERT, 1 is for UPDATE
@BrandId INT OUTPUT
AS BEGIN 
IF @updateType = 0
BEGIN
INSERT INTO Brand(Name,CategoryId) OUTPUT inserted.id VALUES(@name, @categoryId)
SET  @BrandId= SCOPE_IDENTITY();

END

ELSE
BEGIN

UPDATE Brand SET Name=@name, CategoryId=@categoryId  where id=@Id
END 
END 

GO

--insert UPDATE Category 
CREATE PROCEDURE UpdateCategory
@Id INT,
@name VARCHAR(30),
@updateType BIT,     -- 0 is for INSERT, 1 is for UPDATE
@flag INT OUTPUT,
@CategoryId INT OUTPUT
AS BEGIN 
SET @flag = 0
IF @updateType = 0
BEGIN
INSERT INTO Category(Name) OUTPUT inserted.id VALUES( @name)
SET  @CategoryId= SCOPE_IDENTITY();

END

ELSE
BEGIN

UPDATE Category SET Name=@name  where id=@Id
END 
END 

--insert into AddtoWishlist
GO
ALTER PROCEDURE AddToWishlist
@customerId VARCHAR(15),
@productId INT

AS 
BEGIN
	Print 'Data was right'
	insert into Wishlist(ProductId,CustomerId,WishDate) values(@productId,@customerId,getdate())
END 

--Buy an Product 
GO

ALTER PROCEDURE BuyProduct
@username VARCHAR(15),
@productId INT,
@quantity INT,
@flag INT output,
@orderId INT output
AS 
BEGIN
	DECLARE @userAccount INT
	SET @orderId = -1
	IF  (@quantity<1)
		BEGIN
		SET @flag=4     -- quantity is zero
		END
	ELSE IF NOT EXISTS(SELECT * FROM Quantity WHERE ProductId=@productId and Stock > @quantity)
		BEGIN 
		SET @flag=3  -- quantity in stock is less than required one
		END 
	ELSE IF NOT EXISTS(SELECT * FROM Accounts WHERE [Username] = @username AND Amount > @quantity*(SELECT Price FROM Product WHERE id=@productId))
		BEGIN 
		SET @flag=2   --user don't have enough amount 
		END 
	ELSE
		BEGIN
		INSERT into [Order] (ProductId,CustomerId,DateOfOrder,Quantity) values(@productId,@username,getdate(),@quantity)-- place thing in order
		SET @orderId = @@IDENTITY
		UPDATE Quantity SET Stock=Stock-@quantity,Sold=Sold+@quantity  where ProductId= @productId        -- you need to UPDATE sold and in stock
		UPDATE Accounts SET Amount=Amount-(@quantity*(Select Price from Product where id=@productId))     where    [UserName]=@username                                           
		SET @flag=1   --successful
		END
END

GO

-- INSERT/UPDATE Product
CREATE PROCEDURE UpdateProduct
@name VARCHAR(30),
@id INT,
@brandId INT,
@categoryId INT,
@price INT,
@updateType BIT,			-- 0 is for INSERT, 1 is for UPDATE
@productId INT OUTPUT,
@flag INT OUTPUT
AS
BEGIN
	SET @flag = 0
	IF @price < 0
	BEGIN
		SET @flag = 1	--flag = 1 means price is wrong 
	END
	ELSE 
	BEGIN
		IF @updateType = 0
		BEGIN
			INSERT INTO Product(Name, [BrandId], [CategoryId], [Price]) OUTPUT inserted.id VALUES(@name, @brandId, @categoryId, @price)
			SET @productId = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			UPDATE Product SET Name=@name, [BrandId]=@brandId, [CategoryId]=@categoryId, [Price]=@price WHERE id=@id
		END
	END
END
GO

CREATE PROCEDURE DeleteProduct
@productId INT
AS
BEGIN
	DELETE FROM [Product] WHERE id = @productId
END
GO

ALTER PROCEDURE AddToFeatured
@ProductId INT,
@level INT,
@image VARCHAR(50),
@flag INT OUTPUT -- 0=Success, 1=No Space for Platinum, 2 = No Space for Gold, 3 = No Space for Silver
AS
BEGIN
	IF (SELECT COUNT(*) FROM Featured WHERE [Level] = @level) < @level
	BEGIN
		INSERT INTO Featured(ProductId, [Level], [CoverImage]) VALUES(@ProductId, @level, @image)
		SET @flag = 0
	END
	ELSE 
	BEGIN
		SET @flag = @level
	END
END
GO

CREATE procedure SignUp	--flag 1 means username invalid, 2 means no password given 3 means duplicate email given, 0 means signUP krlia user ne
@username VARCHAR(15),
@pass VARCHAR(15),
@firstName VARCHAR(20),
@lastName VARCHAR(20),
@number VARCHAR(15),
@email VARCHAR(30),
@img VARCHAR(50),
@flag int output

AS
BEGIN
	IF EXISTS (SELECT * FROM Customer WHERE Username = @username)
	BEGIN
		SET @flag = 1
	END
	ELSE IF (@pass IS NULL) 
	BEGIN
		SET @flag = 2
	END
	ELSE IF Exists(Select * from Customer Where Email=@email)
	BEGIN
		SET @flag = 3
	END
	ELSE
	BEGIN
		SET @flag = 0
		INSERT INTO Customer([Username], [FirstName], [LastName], [Password],[Email],[ContactNumber],[JoinDate],[Image]) VALUES (@username,@firstName,@lastName,@pass,@email,@number,getdate(),@img)
		PRINT 'Inserted'
	END
END
GO

DECLARE @outputFlag AS INT
EXECUTE [SignUp]
@Un = 'messi',
@Pass='lionelmessi',
@Fn = 'Lionel',
@Lan = 'Messi',
@Cn = '03001',
@em='messi10@gmail.com',
@img=NULL,
@flag = @outputFlag OUTPUT

PRINT CAST(@outputFlag AS VARCHAR)

----For Signing In
GO
ALTER PROCEDURE SignInUser	--flag -1 means username invalid, -2 means wrong password given, 0 means signIN krlia user ne 3 means admin is here
@Un VARCHAR(15),
@Pass VARCHAR(15),
@flag int output

AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Customer WHERE Username = @Un)
	BEGIN
		SET @flag = -1
	END
	ELSE IF NOT Exists(Select * from Customer Where Username=@Un AND Password=@Pass)
	BEGIN
		SET @flag = -2
	END
	ELSE
	BEGIN
		SET @flag = 0
		IF EXISTS (SELECT * FROM [Admin] WHERE [Username] = @Un)
		BEGIN
			SET @flag = 1
		END
		PRINT 'Signed In'
	END
END
GO

DECLARE @outputFlag AS INT
EXECUTE [SignInUser]
@Un = 'messi',
@Pass='lionelmessi',
@flag = @outputFlag OUTPUT

PRINT CAST(@outputFlag AS VARCHAR)
GO
ALTER PROCEDURE AddComment	--flag 1 means added 0 means ni hua add
@Cid VARCHAR(15),
@Pid INT,
@input VARCHAR(50),
@flag INT output
AS
BEGIN
	IF EXISTS (SELECT * FROM [Order] WHERE [Order].ProductId=@Pid AND [Order].CustomerId=@Cid)
	BEGIN
		IF EXISTS (SELECT * FROM [Comment] WHERE [ProductId] = @Pid AND [CustomerId] = @Cid)
		BEGIN
			UPDATE [Comment] SET [text] = @input WHERE [ProductId] = @Pid AND [CustomerId] = @Cid
		END
		ELSE 
		BEGIN
			INSERT INTO [Comment]([ProductId], [CustomerId], [text]) VALUES (@Pid,@Cid,@input)
		END
		SET @flag = 1
		PRINT 'Inserted'
	END
	ELSE 
	BEGIN
		SET @flag = 0
		PRINT 'Not Inserted'
	END
END
GO

DECLARE @outputFlag AS INT
EXECUTE [AddComment]
@Pid=1,
@Cid='lionel messi',
@input='Behtreen cheez ha yr!!!',
@flag = @outputFlag OUTPUT

PRINT CAST(@outputFlag AS VARCHAR)

GO
ALTER PROCEDURE AddRating	--flag 1 means added 0 means ni hua add
@Cid VARCHAR(15),
@Pid INT,
@input VARCHAR(50),
@flag INT output

AS
BEGIN
	IF EXISTS (SELECT * FROM [Order] WHERE [Order].ProductId=@Pid AND [Order].CustomerId=@Cid)
	BEGIN
		IF EXISTS (SELECT * FROM [Rating] WHERE ProductId=@Pid AND CustomerId=@Cid)
		BEGIN
			UPDATE [Rating] SET [Rate] = @input WHERE [ProductId] = @Pid AND [CustomerId] = @Cid
		END
		ELSE
		BEGIN
			INSERT INTO [Rating] ([ProductId], [CustomerId], [Rate]) VALUES (@Pid,@Cid,@input)
		END
		SET @flag = 1
		PRINT 'Rated'
	END

	ELSE 
	BEGIN
		SET @flag = 0
		PRINT 'Not Rated'
	END
END
GO

DECLARE @outputFlag AS INT
EXECUTE [AddRating]
@Pid=1,
@Cid='lionel messi',
@input=4,
@flag = @outputFlag OUTPUT

PRINT CAST(@outputFlag AS VARCHAR)

GO
ALTER PROCEDURE UpdateStock
@Pid INT,
@amount VARCHAR(50)

AS
BEGIN
	IF EXISTS (SELECT * FROM [Quantity] WHERE ProductId=@Pid)
	BEGIN
		UPDATE [Quantity] SET [Stock] = @amount WHERE ProductId = @Pid
	END
	ELSE 
	BEGIN
		INSERT INTO [Quantity] VALUES(@Pid, null, @amount)
	END
END
GO

ALTER PROCEDURE UpdateUserInfo
@username VARCHAR(15),
@fname VARCHAR(20),
@lname VARCHAR(20),
@number VARCHAR(15),
@pass VARCHAR(15)
AS
BEGIN
	UPDATE Customer SET [FirstName] = @fname, [LastName] = @lname, [ContactNumber] = @number, [Password] = @pass
	WHERE [Username] = @username
END
GO

ALTER PROCEDURE RequestCredit
@username VARCHAR(15),
@amount INT,
@flag INT OUTPUT -- 2 = a request is already pending
AS
BEGIN
	IF EXISTS (SELECT * FROM Requests WHERE [CustomerId] = @username)
	BEGIN
		SET @flag = 2
	END
	ELSE
	BEGIN
		INSERT INTO Requests([CustomerId], [Amount]) VALUES(@username, @amount)
		SET @flag = 0
	END
END
GO

ALTER PROCEDURE HandleCreditRequest
@username VARCHAR(15),
@handleType INT
AS
BEGIN
	IF @handleType = 1
	BEGIN
		DECLARE @amount INT
		SELECT @amount = [Amount] FROM Requests WHERE CustomerId=@username
		IF EXISTS (SELECT * FROM [Accounts] WHERE [UserName]=@username)
		BEGIN
			UPDATE [Accounts] SET [Amount] = [Amount] + @amount WHERE [UserName] = @username
		END
		ELSE 
		BEGIN
			INSERT INTO [Accounts] VALUES(@amount, @username)
		END
		DELETE FROM [Requests] WHERE [CustomerId] = @username
	END
	ELSE
	BEGIN
		DELETE FROM [Requests] WHERE [CustomerId] = @username
	END
END
GO

ALTER PROCEDURE HandleProductRequest 
@username VARCHAR(15),
@request VARCHAR(100)
AS
BEGIN
	BEGIN TRY
		INSERT INTO [ProductRequests](CustomerId, [Desc], [DateOfRequest]) VALUES(@username, @request, getdate())
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() as ErrorMessage
	END CATCH
END
GO

CREATE PROCEDURE DeleteProductRequest 
@reqId VARCHAR(15)
AS
BEGIN
	BEGIN TRY
		DELETE FROM [ProductRequests] WHERE id = @reqId
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() as ErrorMessage
	END CATCH
END
GO