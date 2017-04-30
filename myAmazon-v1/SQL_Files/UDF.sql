USE myAmazonV2
GO
ALTER FUNCTION GetProductsByUser(@CustomerId VARCHAR(15))
RETURNS TABLE
AS
RETURN SELECT * FROM ProductDetails WHERE id IN (SELECT [ProductId] FROM UserProducts WHERE CustomerId = @CustomerId)

SELECT * FROm GetProductsByUser('xavi')