USE [PizzaPlace]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderProducts]    Script Date: 13.12.2023 13:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE PizzaPlace
--CREATE PROCEDURE GetUserOrders
--@userId INT
--AS
--BEGIN
--SELECT * FROM Orders 
--WHERE Orders.userId = @userId
--END;

--EXEC GetUserOrders @usr = 65

ALTER PROCEDURE [dbo].[GetOrderProducts]
@ordr BIGINT
AS
BEGIN
SELECT Products.[name], Goods.size, GoodsToOrders.[count], Goods.price FROM GoodsToOrders
JOIN Goods ON Goods.id = GoodsToOrders.productId
JOIN Products ON Goods.productId = Products.id
WHERE GoodsToOrders.orderId = @ordr
END;
