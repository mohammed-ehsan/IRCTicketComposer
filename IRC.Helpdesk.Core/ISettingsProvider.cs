﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{
    public interface ISettingsProvider
    {
        AppSettings GetSettings();
    }
}
