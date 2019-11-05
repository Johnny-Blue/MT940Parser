namespace ParserPOC.Services
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MT940Data;
    using programmersdigest.MT940Parser.Model;
    using programmersdigest.MT940Parser.Parsing;
    using programmersdigest.MT940Parser.Store;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;


    public class MT940Service : IMT940Service
    {
        private readonly IParser _parser;
        private readonly IStoreMT940 _storeMT940;
        private readonly IOptions<MT940ServiceConfig> _configuration;
        private readonly ILogger<MT940Service> _logger;

        public MT940Service(IParser parser, IStoreMT940 storeMT940, IOptions<MT940ServiceConfig> configuration, ILogger<MT940Service> logger)
        {
            this._parser = parser;
            this._storeMT940 = storeMT940;
            this._configuration = configuration;
            this._logger = logger;
        }

        //public ILogger Logger { get; set; }

        public Task ExecuteAsync()
        {
            string sourcePath = _configuration.Value.SourcePath;
            string archivePath = _configuration.Value.ArchivePath;
            string failedPath = _configuration.Value.FailedPath;
            bool archive = _configuration.Value.Archive;
            return Task.Run(async () =>
            {
                FileInfo[] files = null;
                if (System.IO.Path.GetFullPath(sourcePath) == sourcePath)
                {
                    DirectoryInfo di = new DirectoryInfo(sourcePath);
                    files = di.GetFiles();
                }
                else
                {
                    FileInfo fi = new FileInfo(sourcePath);
                    files = new FileInfo[] { fi };
                }

                foreach (var file in files)
                {
                    try
                    {
                        _parser.Path = file.FullName;
                        IEnumerable<Statement> statements = _parser.Parse().Select(r => r);
                        foreach (var statement in statements)
                        {
                            statement.FileName = file.Name;
                            await _storeMT940.SaveMT940StatementAsync(statement).ConfigureAwait(false);
                            await _storeMT940.CommitAsync().ConfigureAwait(false);
                        }
                        if (archive)
                        {
                            File.Move(file.FullName, Path.Combine(archivePath, file.Name), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        File.Move(file.FullName, Path.Combine(failedPath, file.Name), true);
                        _logger.LogError(ex, $"Error in file {file.Name}.", null);
                    }
                }
            });
        }
    }
}

/** 
   foreach (var item in stmt)
   {
       Console.WriteLine($"AccountIdentification: {item.AccountIdentification}");
       Console.WriteLine($"OpeningBalance: {item.OpeningBalance}");
       Console.WriteLine($"ClosingAvailableBalance: {item.ClosingAvailableBalance}");
       Console.WriteLine($"ClosingBalance: {item.ClosingBalance}");
       Console.WriteLine("=========== ForwardAvailableBalances:");

       foreach (var fab in item.ForwardAvailableBalances)
       {
           Console.WriteLine(fab.ToString());
       }
       Console.WriteLine("END ForwardAvailableBalances.");

       Console.WriteLine($"InformationToOwner: {item.InformationToOwner}");
       Console.WriteLine($"RelatedReference: {item.RelatedReference}");
       Console.WriteLine($"SequenceNumber: {item.SequenceNumber}");
       Console.WriteLine($"StatementNumber: {item.StatementNumber}");
       Console.WriteLine($"TransactionReferenceNumber: {item.TransactionReferenceNumber}");
       Console.WriteLine("=========== Lines:");
       foreach (var line in item.Lines)
       {
           Console.WriteLine(line.ToString());
       }
       Console.WriteLine("=========== END Lines:");
   }

   throw new NotImplementedException(); 
*/
