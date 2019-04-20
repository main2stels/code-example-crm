using FullCRM.Database.Postgre.Model;
using FullCRM.Database.Sql.Extensions;
using FullCRM.Exceptions;
using FullCRM.Models;
using FullCRM.Models.Frontend.Extension;
using FullCRM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullCRM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService _defaultOrderReadServices;
        private readonly UserService _userService;
        private readonly FrontendBillingService _frontendBillingService;

        private readonly string dateErrorMessage = "Дата начала больше даты окончания";
        private readonly string contractNotFoundMessage = "Договор для выбранного контрагента не найден";

        public OrderController(
            OrderService defaultOrderReadServices,
            UserService userService,
            FrontendBillingService frontendBillingService)
        {
            _defaultOrderReadServices = defaultOrderReadServices;
            _userService = userService;
            _frontendBillingService = frontendBillingService;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _defaultOrderReadServices.GetAll();
        }

        [HttpGet]
        public PageModel<Order> Get(WebApiEntityPageCondition<Order> condition)
        {
            if (condition == null)
            {
                condition = new WebApiEntityPageCondition<Order>
                {
                    Filter = null,
                    Limit = 30,
                    Page = 0,
                    SortDirect = Database.Sql.Models.SortDirectionType.Descending
                };
            }
            var orders = _defaultOrderReadServices.GetAll();

            var count = orders.AsQueryable().Where(condition.Filter).Count();
            var items = orders.AsQueryable().Page(condition).ToArray();

            return new PageModel<Order> { Items = items, Count = count };
        }

        [HttpGet]
        public PageModel<OrderView> GetView(WebApiEntityPageCondition<OrderView> condition)
        {
            if (condition == null)
            {
                condition = new WebApiEntityPageCondition<OrderView>
                {
                    Filter = null,
                    Limit = 30,
                    Page = 0,
                    SortDirect = Database.Sql.Models.SortDirectionType.Descending
                };
            }

            var orders = _defaultOrderReadServices.GetAllView();

            var count = orders.AsQueryable().Where(condition.Filter).Count();
            var items = orders.AsQueryable().Page(condition).ToArray();

            return new PageModel<OrderView> { Items = items, Count = count };
        }

        [HttpGet]
        public Order GetById(long id)
        {
            return _defaultOrderReadServices.GetById(id);
        }


        [Route("Update")]
        public IActionResult Update([FromBody]Order order)
        {
            if (order.StartDate > order.EndDate)
                return BadRequest(dateErrorMessage);

            try
            {
                return Ok(_defaultOrderReadServices.Update(order));
            }
            catch(ContractNotFoundException)
            {
                return BadRequest(contractNotFoundMessage);
            }
        }

        [Route("Delete")]
        public void Delete([FromBody]long orderId)
        {
            _defaultOrderReadServices.Delete(orderId);
        }
    }
}
