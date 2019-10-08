using System.Data;
using System.Data.SqlClient;

namespace Template.Repositories.Base.Contracts
{
    public interface ITransactionStore
    {
        SqlTransaction Transaction { get; set; }
        void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
        void CommitTransaction();
    }
}
