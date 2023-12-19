using System.Runtime.InteropServices.JavaScript;
using DbmsApp.Context;
using DbmsApp.Models;
using DbmsApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace DbmsApp.Controllers;

public class CouponController: Controller
{
	private IUserService _us;
	private readonly PizzaPlaceContext _db;
	public CouponController(IUserService us, PizzaPlaceContext db)
	{
		_us = us;
		_db = db;
	}
	
	
	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Index(int coup)
	{
		//CHANGE
		var res = _db.Coupons.FirstOrDefault(coupon => coupon.Number == coup && coupon.DateOfExpiration >= DateTime.Now);
		if (res is null) return RedirectToAction("Index");
		
		try
		{
			_us.GoodsInCart[res.Id] += 1;
		}
		catch (KeyNotFoundException)
		{
			_us.GoodsInCart[res.Id] = 1;
		}

		return RedirectToAction("Index", "Cart");
	}
}