USE [PetProducts]
GO

/****** Object:  StoredProcedure [dbo].[SelectProductByName]    Script Date: 18/07/2021 13:11:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectProductByName] @ProductName nvarchar(30)
AS
Select Product.ID, Product.Name, Product.Description, Product.CategoryID, Product.Price, Product.ProductBrandID, Category.CategoryName FROM Product 
LEFT Outer Join Category ON Product.CategoryID = Category.ID
WHERE Product.Name LIKE '%' + @ProductName + '%'
GO

