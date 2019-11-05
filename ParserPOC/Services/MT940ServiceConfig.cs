using System;
using System.Collections.Generic;
using System.Text;

namespace ParserPOC.Services
{
    public class MT940ServiceConfig
    {
        public string SourcePath { get; set; }
        public string ArchivePath { get; set; }
        public string FailedPath { get; set; }
        public bool Archive { get; set; }
    }
}
