using System;
using System.Transactions;

namespace Costanza
{
    /// <summary>
    /// Provides utility methods for Transactions.
    /// </summary>
    public static class TransactionTool
    {
        /// <summary>
        /// Creates a new TransactionScope without the harmful defaults. 
        /// </summary>
        /// <remarks>
        /// The TransactionScope's default constructor sets values that are 
        /// considered harmful. The default Timeout setting can lead to unexpected behaviour, while
        /// the default IsolationLevel is prone to deadlocks.
        /// Source: http://blogs.msdn.com/b/dbrowne/archive/2010/06/03/using-new-transactionscope-considered-harmful.aspx
        /// </remarks>
        /// <returns>
        /// An new instance of the <c>TransactionScope</c> class, with IsolationLevel set to 
        /// ReadCommitted, a and Timeout that's as large as possible.
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