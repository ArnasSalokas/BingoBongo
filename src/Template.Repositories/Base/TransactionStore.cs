using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Template.Repositories.Base.Contracts;

namespace Template.Repositories.Base
{
    public class TransactionStore : ITransactionStore
    {
        private readonly IServiceProvider _services;

        public SqlTransaction Transaction { get; set; }

        public TransactionStore(IServiceProvider services) => _services = services;

        public void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            var connection = _services.GetService<SqlConnection>();

            if (connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed)
                connection.Open();

            Transaction = connection.BeginTransaction(level);
        }

        public void CommitTransaction() => Transaction.Commit();
    }
}
