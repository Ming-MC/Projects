<Query Kind="Statements">
  <Connection>
    <ID>9ab5e237-f18d-47e9-9176-d4663d571627</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Query 1
var queryOne = from p in Products
			   orderby p.OrderLists.Count() descending
			   select new
			   {
			       Product = p.Description,
				   TimesPurchased = p.OrderLists.Count()
			   };
queryOne.Dump();
		   

// Query 2
var queryTwo = from s in Stores
			   orderby s.Location ascending
			   select new
			   {
				   Location = s.Location,
				   Clients = (from o in s.Orders
					 		  select new
							  {
							      address = o.Customer.Address,
								  city = o.Customer.City,
								  province = o.Customer.Province
							  }
							 ).Distinct()
			   };
queryTwo.Dump();

// Query 3
var queryThree = from s in Stores
				 orderby s.City, s.Location
				 select new
				 {
				 	 City = s.City,
					 Location = s.Location,
					 Sales = from o in s.Orders
					 		 where o.OrderDate.Month == 12
							 group o by o.OrderDate into groupOrderDate
							 select new
							 {
							 	 Date = groupOrderDate.Key,
								 NumberOfOrders = groupOrderDate.Count(),
								 ProductSales = groupOrderDate.Sum(s => s.SubTotal),
								 GST = groupOrderDate.Sum(g => g.GST)
							 }
				 };
queryThree.Dump();
			     


// Query 4
var queryFour = from ol in OrderLists
				where ol.OrderID == 33
				group ol by ol.Product.Category into groupCategory
				orderby groupCategory.Key.Description
				select new
				{
					Category = groupCategory.Key.Description,
					OrderProducts = from g in groupCategory
									orderby g.Product.Description
									select new
									{
										Product = g.Product.Description,
										Price = g.Price,
										PickedQty = g.QtyPicked,
										Discount = g.Discount,
										Subtotal = (g.Price - g.Discount) * (decimal)g.QtyPicked,
										Tax = g.Product.Taxable ? ((g.Price - g.Discount) * (decimal)g.QtyPicked) * 0.05m: 0,
										ExtendedPrice = g.Product.Taxable ? ((g.Price - g.Discount) * (decimal)g.QtyPicked) * 1.05m :
																			(g.Price - g.Discount) * (decimal)g.QtyPicked
									}
				};
queryFour.Dump();		

// Query 5
var queryFive = from p in Pickers
   				orderby p.LastName + ", " + p.FirstName
				select new
				{
					Picker = p.LastName + ", " + p.FirstName,
					PickDates = from o in p.Store.Orders
							    where o.OrderDate >= DateTime.Parse("Dec 17, 2017") &&
									  o.OrderDate <= DateTime.Parse("Dec 23, 2017") &&
									  o.PickerID == p.PickerID
								orderby o.OrderDate
								select new
								{
									ID = o.OrderID,
									Date = o.OrderDate
								}
				};
queryFive.Dump();


// Query 6
var querySix = from c in Customers
			   where c.CustomerID == 1
			   orderby c.LastName + ", " + c.FirstName
			   select new
			   {
			   	   Customer = c.LastName + ", " + c.FirstName,
				   OrderCount = c.Orders.Count(),
				   Items = from ol in OrderLists
				   		   where ol.Order.CustomerID == c.CustomerID
						   group ol by ol.Product into groupProduct
						   orderby groupProduct.Key.Description, groupProduct.Count() descending
						   select new
						   {
						   	    Description = groupProduct.Key.Description,
								TimeBought = groupProduct.Count()
						   }
			   };
querySix.Dump();











