using Microsoft.Extensions.Logging;
using MT940Data;
using programmersdigest.MT940Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParserPOC.Services
{
    interface IMT940Service
    {
        //ILogger Logger { get; set; }
        Task ExecuteAsync();
    }
}
