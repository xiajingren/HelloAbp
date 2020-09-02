using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.SettingUi;
using EasyAbp.Abp.SettingUi.Dto;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Xhznl.HelloAbp.Controllers
{
    //TOTO:重写Controller,但是SettingUi方法不是virtual方法
    // [Controller]
    // [Area("setting")]
    // [Route("api/eazyabp")]
    // [RemoteService(false)]
    // [Dependency(ReplaceServices = true)]
    // [ExposeServices(typeof(SettingUiController),typeof(SettingController))]
    // public class SettingController : HelloAbpController, ISettingUiAppService
    // {
    //     protected ISettingUiAppService SettingAppService { get; }
    //
    //     public SettingController(ISettingUiAppService settingAppService)
    //     {
    //         SettingAppService = settingAppService;
    //     }
    //
    //     [HttpGet]
    //     public Task<List<SettingGroup>> GroupSettingDefinitions()
    //     {
    //         return SettingAppService.GroupSettingDefinitions();
    //     }
    //
    //     [HttpPut]
    //     [Route("set-values")]
    //     public Task SetSettingValues(Dictionary<string, string> settingValues)
    //     {
    //         return SettingAppService.SetSettingValues(settingValues);
    //     }
    //
    //     [HttpPut]
    //     [Route("reset-values")]
    //     public Task ResetSettingValues(List<string> settingNames)
    //     {
    //         return SettingAppService.ResetSettingValues(settingNames);
    //     }
    // }
}