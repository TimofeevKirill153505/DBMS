USE [PizzaPlace]
GO
/****** Object:  StoredProcedure [dbo].[GetUserOrders]    Script Date: 13.12.2023 12:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetUserOrders]
@usr INT
AS
BEGIN
SELECT * FROM Orders 
WHERE Orders.userId = @usr
END;
