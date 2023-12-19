using DbmsApp.Context;
using DbmsApp.Models;
using DbmsApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Configuration;

namespace DbmsApp.Controllers;

public class CartController : Controller
{
	private readonly PizzaPlaceContext _db;
	private readonly IUserService _us;
	public CartController(PizzaPlaceContext db, IUserService us)
	{
		_db = db;
		_us = us;
	}
	// GET
	public IActionResult Index()
	{
		List<(Good, int)> lsg = new();
		List<(Coupon, int)> lsc = new();
		Console.WriteLine("From cart contr");
		
		foreach ((var key, var value) in _us.GoodsInCart)
		{
			Console.WriteLine($"GoodId {key} - count = {value}");
		}
		
		foreach ((var goodId, var count) in _us.GoodsInCart)
		{
			if (count <= 0) continue; 
			Good gd = _db.Goods.Find(goodId);
			gd.Product = _db.Products.Find(gd.ProductId);
			lsg.Add((gd, count));
		}
		
		foreach ((var couponId, var count) in _us.CouponsInCart)
		{
			if (count <= 0) continue; 
			Coupon cp = _db.Coupons.Find(couponId);
			lsc.Add((cp, count)!);
		}
		// foreach ((var good, var count) in lsg)
		// {
		// 	Console.WriteLine($"lsg{good.Product.Name} - lsg{count}");
		// }
		
		var res = (lsg, lsc);
		return View((lsg, lsc));
	}

	public IActionResult AddOneProductTo(int id)
	{
		_us.GoodsInCart[id] += 1;
		return RedirectToAction("Index");
	}

	public IActionResult RemoveOneProduct(int id)
	{
		_us.GoodsInCart[id] -= 1;
		return RedirectToAction("Index");
	}

	public IActionResult ClearOneProduct(int id)
	{
		_us.GoodsInCart[id] = 0;
		return RedirectToAction("Index");
	}

	[HttpPost]
	public IActionResult Index(string address, string? entrance, string? number)
	{
		//Console.WriteLine($"{address} - {entrance} - {number}");
		if (_us.CurrentUser is null) return RedirectToAction("Login", "Auth");
		
		var addr = new Address()
		{
			Adress = address,
			Entrance = entrance,
			Number = number
		};
		
		//CHANGE
		var ent = _db.Addresses.Add(addr);
		_db.SaveChanges();
		Console.WriteLine($"id of new address {ent.Entity.Id}");
		var ordr = new Order()
		{
			AddressId = ent.Entity.Id,
			DateOfOrder = DateTime.Now,
			UserId = _us.CurrentUser.Id
		};
		
		var eo = _db.Orders.Add(ordr);
		_db.SaveChanges();
		Console.WriteLine("Is there anybody here...");
		Console.WriteLine($"id of new order is {eo.Entity.Id}");
		foreach ((var goodId, var count) in _us.GoodsInCart)
		{
			// GoodsToOrder gto = new GoodsToOrder()
			// {
			// 	OrderId = eo.Entity.Id,
			// 	ProductId = goodId,
			// 	Count = count
			// };

			_db.GoodsToOrders.FromSqlRaw($"INSERT INTO GoodsToOrders(orderId, productId, count) " +
									$"VALUES ({eo.Entity.Id}, {goodId}, {count})");
			
		}

		_db.SaveChanges();

		return Redirect("/");
	}
}