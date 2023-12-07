--���� �������
SELECT Orders.id, Orders.price, SUM(ISNULL(Coupons.price,0) * ISNULL(CouponsToOrders.[count], 0)
+ ISNULL(Goods.price,0) * ISNULL(GoodsToOrders.[count],0)) as 'sum', 
Coupons.price as 'coupon price', CouponsToOrders.[count] as 'coupon count',
Goods.price as 'goods price', GoodsToOrders.count as 'goods count'
FROM Orders
LEFT JOIN CouponsToOrders ON Orders.Id = CouponsToOrders.orderId
LEFT JOIN Coupons ON CouponsToOrders.couponId = Coupons.id
LEFT JOIN GoodsToOrders ON GoodsToOrders.orderId = Orders.Id
LEFT JOIN Goods ON GoodsToOrders.productId = Goods.id
GROUP BY Orders.id, Orders.price, Coupons.price, CouponsToOrders.[count], Goods.price, GoodsToOrders.[count]

--like query
SELECT * FROM Products WHERE name NOT LIKE N'[�-�]%' and [type] IN (N'�����', N'�����')

--���� � ����������
SELECT * FROM Goods WHERE price BETWEEN 5 AND 15

--�������� ������
SELECT * FROM Orders WHERE dateOfDelivery = NULL

--������������
SELECT * FROM Users

--�������� ������
SELECT * FROM Coupons WHERE dateOfExpiration > GETDATE()

--���
SELECT * FROM Products WHERE [type] in (N'�����', N'�������', N'�����')

--������
SELECT * FROM Addresses