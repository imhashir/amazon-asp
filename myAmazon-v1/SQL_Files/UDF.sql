USE myAmazonV2
GO
ALTER FUNCTION GetProductsByUser(@CustomerId VARCHAR(15))
RETURNS TABLE
AS
RETURN SELECT * FROM ProductDetails WHERE id IN (SELECT [ProductId] FROM UserProducts WHERE CustomerId = @CustomerId)

SELECT * FROm GetProductsByUser('xavi')

ALTER FUNCTION GetCommentsOnProduct(@ProductId INT)
RETURNS TABLE
AS
RETURN 
SELECT c.FirstName + ' ' + c.LastName AS Name, CustomerId AS Username, [text], c.[Image] FROM Comment cmnt 
JOIN Customer c ON c.Username = cmnt.CustomerId
WHERE ProductId = @ProductId 

SELECT * FROM GetCommentsOnProduct(54)
