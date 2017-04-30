USE myAmazonV2
GO
SELECT * FROM ProductDetails
GO
ALTER VIEW ProductDetails
AS
SELECT p.[id], p.[Name], [Desc],pinfo.[Image] AS [Image], p.[Price], p.[CategoryId] AS CatId, cat.[Name] AS Category, b.[Name] AS Brand, p.[BrandId] AS BrandId, q.[Stock] AS [Quantity]
FROM Product p 
LEFT OUTER JOIN ProductInfo pinfo ON p.[id] = pinfo.[ProductId]
JOIN Category cat ON p.[CategoryId]=cat.[id] 
JOIN Brand b ON p.[BrandId]=b.[id]
LEFT OUTER JOIN [Quantity] q ON p.[id] = q.[ProductId]
GO

SELECT * FROM BrandDetails
GO
ALTER VIEW BrandDetails
AS
SELECT b.[id], b.[Name], [Desc], binfo.[Image] AS [Image], cat.[Name] AS Category, b.[CategoryId] AS [CatId] 
FROM Brand b 
LEFT OUTER JOIN BrandInfo binfo ON b.[id] = binfo.[BrandId]
JOIN Category cat ON b.[CategoryId]=cat.[id]
GO

SELECT * FROM CategoryDetails
GO
ALTER VIEW CategoryDetails
AS
SELECT [id], [Name], [Desc], [Image] 
FROM Category c
LEFT OUTER JOIN CategoryInfo cinfo ON c.[id] = cinfo.[CategoryId]
GO

ALTER VIEW FeaturedDetails
AS
SELECT f.[id], [ProductId], p.[Name], [Level], [CoverImage]
FROM Featured f
JOIN Product p ON [ProductId] = p.[id]

GO

ALTER VIEW CustomerDetails
AS
SELECT [Username], [Password], [FirstName], [LastName], [ContactNumber], [Email], [Image]
FROM Customer

GO

ALTER VIEW CreditRequestsDetails
AS
SELECT [CustomerId] AS [Username], [Amount]
FROM Requests

CREATE VIEW ProductRequestDetails
AS
SELECT pr.[id], pr.[CustomerId], c.FirstName + ' ' + c.LastName AS [Name], pr.[Desc], pr.[DateOfRequest] FROM ProductRequests pr
JOIN Customer c ON c.Username = pr.CustomerId