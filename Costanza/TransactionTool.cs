using System;
using System.Transactions;

namespace Costanza
{
    /// <summary>
    /// Provides common utility methods for Transactions.
    /// </summary>
    public static class TransactionTool
    {
        /// <summary>
        /// Creates a new TransactionScope without the harmless defaults. 
        /// </summary>
        /// <remarks>
        /// The TransactionScope's default constructor sets values that are 
        /// considered harmful. The default Timeout setting can lead to unexpected behaviour, while
        /// the default IsolationLevel is prone to deadlocks.
        /// Source: http://blogs.msdn.com/b/dbrowne/archive/2010/06/03/using-new-transactionscope-considered-harmful.aspx
        /// </remarks>
        /// <returns>
        /// An new instance of the <c>TransactionScope</c> class, with IsolationLevel set to 
        /// ReadCommitted, and Timeout is as large as possible.
        /// </returns>
        public static TransactionScope CreateTransactionScope()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.MaxValue
            };
            return new TransactionScope( TransactionScopeOption.Required, options );
        }
    }
}