using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db; 
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
            //_db.Update<Category>(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id); 
            if(orderFromDb != null) 
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null) 
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
		}
	}
}
