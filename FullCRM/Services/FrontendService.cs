using FullCRM.Models.Frontend;
using FullCRM.Settings.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Services
{
    public class FrontendService
    {
        private readonly UserService _userService;

        public FrontendService(UserService userService)
        {
            _userService = userService;
        }
        public FrontendSettingsModel GetSettings(long userId)
        {
            var account = _userService.GetAccountByUserId(userId);

            var accountInfo = new AccountInfoModel(account);

            return new FrontendSettingsModel
            {
                Enums = new FrontendSettings().Enums,
                AccountInfo = accountInfo
            };
        }
    }
}
