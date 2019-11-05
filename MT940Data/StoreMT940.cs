namespace programmersdigest.MT940Parser.Store
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MT940Data.Entities;
    using System;
    using System.Threading.Tasks;
    using Statement = Model.Statement;
    using StatementLine = Model.StatementLine;

    public class StoreMT940 : IStoreMT940
    {
        private AbnAmroNL _abnAmroNL;
        private IMapper _mapper;

        public StoreMT940(AbnAmroNL abnAmroNL, IMapper mapper)
        {
            _abnAmroNL = abnAmroNL;
            this._mapper = mapper;
        }

        public async Task SaveMT940StatementAsync(Statement statement)
        {
            if (statement == null) throw new ArgumentNullException(nameof(statement));

            MT940Data.Entities.Statement newStatement = await _abnAmroNL
                .Statement
                .SingleOrDefaultAsync<MT940Data.Entities.Statement>(s => statement.AccountIdentification == s.AccountIdentification && s.StatementNumber == statement.StatementNumber).ConfigureAwait(false);

            if (newStatement != null)
            {
                await _abnAmroNL.Database.ExecuteSqlRawAsync("EXECUTE [mt940].[DeleteStatement] @Id = {0}", new object[] { newStatement.Id }).ConfigureAwait(false);
            }

            newStatement = _mapper.Map<MT940Data.Entities.Statement>(statement);
            _abnAmroNL.Statement.Add(newStatement);
            
            if (statement.OpeningBalance != null)
            {
                newStatement.StatementBalance.Add(_mapper.Map<StatementBalance>(statement.OpeningBalance));
            }
            if (statement.ClosingAvailableBalance != null)
            {
                newStatement.StatementBalance.Add(_mapper.Map<StatementBalance>(statement.ClosingAvailableBalance));
            }
            if (statement.ClosingBalance != null)
            {
                newStatement.StatementBalance.Add(_mapper.Map<StatementBalance>(statement.ClosingBalance));
            }
            foreach (var item in statement.ForwardAvailableBalances)
            {
                newStatement.StatementBalance.Add(_mapper.Map<StatementBalance>(item));
            }

            if (statement.InformationToOwner != null)
            {
                newStatement.StatementInformation.Add(_mapper.Map<StatementInformation>(statement.InformationToOwner));
            }

            foreach (StatementLine line in statement.Lines)
            {
                var dbLine = _mapper.Map<MT940Data.Entities.StatementLine>(line);
                if (line.InformationToOwner != null)
                {
                    dbLine.StatementLineInformation.Add(_mapper.Map<MT940Data.Entities.StatementLineInformation>(line.InformationToOwner));
                }
                newStatement.StatementLine.Add(dbLine);
            }
        }

        public async Task<int> CommitAsync()
        {
            if (_abnAmroNL.ChangeTracker.HasChanges())
            {
                return await _abnAmroNL.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                return -1;
            }
        }
    }
}
