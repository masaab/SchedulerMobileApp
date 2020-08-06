using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.AppSetting
{
    public interface IApplicationSettingManager
    {
        AppSettingDetail AppSettingDetails { get; }
    }
}
