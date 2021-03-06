﻿/* MIL-STD-1553 <-> SPI bridge
 * Copyright(c) 2016 Stanislav Zhelnio
 * Licensed under the MIT license (MIT)
 * https://github.com/zhelnio/mil1553-spi
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilTest.MilLight.ServiceProtocol
{
    public class SPResetRequest : SPRequest
    {
        public override SPCommand Command
        {
            get { return SPCommand.Reset; }
        }
    }
}
