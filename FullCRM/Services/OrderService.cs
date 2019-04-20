using FullCRM.Database;
using FullCRM.Database.Postgre.PostgreEssence;
using FullCRM.Database.Postgre.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullCRM.Exceptions;

namespace FullCRM.Services
{
    public class OrderService
    {
        private readonly PostgreEssence<Order> _postgreEssence;
        private readonly PostgreEssence<OrderView> _orderViewEssence;
        private readonly VersionService _versionService;
        private readonly UserService _userService;
        private readonly NextNumberEssenceService _nextNumberEssenceService;
        private readonly ContractService _contractService;
        private readonly InfoWebSocketService _infoWebSocketService;

        public OrderService(PostgreEssence<Order> postgreEssence,
            PostgreEssence<OrderView> orderViewEssence,
            VersionService versionService,
            UserService userService,
            NextNumberEssenceService nextNumberEssenceService,
            ContractService contractService,
            InfoWebSocketService infoWebSocketService)
        {
            _postgreEssence = postgreEssence;
            _versionService = versionService;
            _userService = userService;
            _nextNumberEssenceService = nextNumberEssenceService;
            _contractService = contractService;
            _infoWebSocketService = infoWebSocketService;
            _orderViewEssence = orderViewEssence;
        }


        #region get
        public IEnumerable<Order> GetAll()
        {
            return _postgreEssence.AsQueryable().Where(x => x.Condition == Condition.Live).ToArray();
        }

        public IEnumerable<OrderView> GetAllView()
        {
            return _orderViewEssence.AsQueryable();
        }

        public Order GetById(long id)
        {
            var order = _postgreEssence
                .AsQueryable()
                .FirstOrDefault(x => x.Id == id);

            return order.Condition == Condition.Live ? order : null;
        }

        public Order GetByNumber(string number)
        {
            var order = _postgreEssence
                .AsQueryable()
                .FirstOrDefault(x => x.Number.Equals(number));

            if (order == null)
                return null;

            return order.Condition == Condition.Live ? order : null;
        }

        public IEnumerable<Order> GetByContractId(long contractId)
        {
            return _postgreEssence.AsQueryable()
                .Where(x => x.ContractId == contractId && x.Condition == Condition.Live);
        }
        #endregion

        #region edit
        public Order InsertOrder(Order order, long userId)
        {
            CheckResponsible(order, userId);

            order.CreateDate = DateTime.Now;
            order.EditDate = DateTime.Now;
            order.Number = _nextNumberEssenceService.GetNumber(Database.Postgre.Model.Enum.IncrementType.Order);

            var contract = _contractService.GetByContractorId(order.ContractorId);
            if (contract == null)
                throw new ContractNotFoundException();

            order.AttachmentNumber = (int)contract.NextNumberAttachment;

            _contractService.UpNextNumberAttachment(contract.Id);

            order.Id = _postgreEssence.InsertWithIdentity(order);
            _versionService.Insert(order.Id.ToString(), order);
            _infoWebSocketService.SendMessage(order, Models.WebSocket.Mode.Create);

            return order;
        }

        public Order Update(Order order)
        {
            order.EditDate = DateTime.Now;

            var contract = _contractService.GetByContractorId(order.ContractorId);
            if (contract == null)
                throw new ContractNotFoundException();

            _versionService.Update(order.Id.ToString(), order);
            _postgreEssence.Update(order);

            _infoWebSocketService.SendMessage(order, Models.WebSocket.Mode.Edit);

            return order;
        }

        public void Delete(long orderId)
        {
            var order = GetById(orderId);
            if (order == null)
                throw new Exception($"Нельзя удалить заказ id{orderId}, он не найден");

            order.Condition = Condition.Delete;
            order.EditDate = DateTime.Now;

            _versionService.Delete(orderId.ToString(), order);
            _postgreEssence.Update(order);

            _infoWebSocketService.SendMessage(order, Models.WebSocket.Mode.Delete);
        }

        #endregion

        /// <summary>
        /// Проверяем наличие ответственного, если его нет, то ставим
        /// </summary>
        /// <param name="order"></param>
        /// <param name="userId"></param>
        void CheckResponsible(Order order, long userId)
        {
            if(order.ResponsibleId == 0 || order.Responsible == null)
            {
                order.ResponsibleId = _userService.GetAccountByUserId(userId).Id;
            }
        }

        
    }
}
