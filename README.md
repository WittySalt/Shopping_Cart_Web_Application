# Shopping_Cart_Web_Application_V1.0
Attributes Design:
The attributes in MVC Models:
1.Product
	Id (int)
	ProductName (string)
	ProductDescription (string)
	Image (string)
	Price (double)
	ActivationCode (Guid)

2.Cart
	Id (int)
	UserId (string)
	ProductId (int)
	Quantity (int)
	UnitPrice (double)
	Product (Product)

3.Order
	Id (int)
	UserId (string)
	CreateDate (DateTime)
	IsDeleted (bool)
	OrderDetails (List<OrderDetail>)

4.OrderDetail
	Id (int)
	OrderId (int)
	ProductId (int)
	Quantity (int)
	Order (Order)
	Product (Product)
	