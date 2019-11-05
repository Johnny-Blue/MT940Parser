namespace programmersdigest.MT940Parser.Store
{
    using programmersdigest.MT940Parser.Model;
    using System.Threading.Tasks;

    public interface IStoreMT940
    {
        Task SaveMT940StatementAsync(Statement statement);
        Task<int> CommitAsync();
    }
}
