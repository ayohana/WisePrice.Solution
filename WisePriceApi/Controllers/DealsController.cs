using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WisePriceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WisePriceApi.Controllers
{
  [Route("/[controller]")]
  [ApiController]
  public class DealsController : ControllerBase
  {
    private WisePriceApiContext _db;

    public DealsController(WisePriceApiContext db)
    {
      _db = db;
    }

    // GET api/deals
    [HttpGet]
    public ActionResult<IEnumerable<Deal>> Get(string itemName, string location)
    {
      var query = _db.Deals.Include(entry => entry.Item).Include(entry => entry.Location).AsQueryable();
      
      if (itemName != null)
      {
        query = query.Where(entry => entry.Item.ItemName.Contains(itemName));
      }

      if (location != null)
      {
        query = query.Where(entry => entry.Location.ZipCode.ToString() == location);
      }

      return query.ToList();
    }


    // GET api/deals/5
    [HttpGet("{id}")]
    public ActionResult<Deal> Get(int id)
    {
      return _db.Deals.FirstOrDefault(entry => entry.DealId == id);
    }

    // POST api/deals
    [HttpPost]
    public void Post([FromBody] Deal deal)
    {
      _db.Deals.Add(deal);
      _db.SaveChanges();
    }

    // PUT api/deals/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Deal deal)
    {
        deal.DealId = id;
        _db.Entry(deal).State = EntityState.Modified;
        _db.SaveChanges();
    }

    // DELETE api/deals/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var dealToDelete = _db.Deals.FirstOrDefault(entry => entry.DealId == id);
      _db.Deals.Remove(dealToDelete);
      _db.SaveChanges();
    }
  }
}