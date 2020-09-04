using EconomiKids.Domain.SeedWork;
using System;

namespace EconomiKids.Domain.AggregatesModel.PiggyBankAggregate
{
    /// <summary>
    /// Representes a withdraw or deposit transaction
    /// </summary>
    public class PiggyBankTransaction : Entity
    {
        /// <summary>
        /// Value of the transaction
        /// </summary>
        private decimal transactionValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value of the transaction</param>
        public PiggyBankTransaction(decimal value)
        {
            Id = Guid.NewGuid();
            TransactionValue = value;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Transaction identifier</param>
        /// <param name="transactionValue">Value of the transaction</param>
        /// <param name="date">Date and time that the transaction occured</param>
        public PiggyBankTransaction(Guid id, decimal transactionValue, DateTime date) : this(transactionValue)
        {
            Id = id;
            Date = date;
        }

        /// <summary>
        /// Date and time that the transaction occured
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Indicate the transaction type
        /// </summary>
        public PiggyBankTransactionType TransactionType => TransactionValue < 0 ? PiggyBankTransactionType.Withdraw : PiggyBankTransactionType.Deposit;

        /// <summary>
        /// Value of the transaction
        /// </summary>
        public decimal TransactionValue
        {
            get { return transactionValue; }
            private set
            {
                //Transaction cant be zero
                if (value == 0)
                    throw new ArgumentException(Properties.Resources.ValueCannotBeZero);

                transactionValue = value;
            }
        }
    }
}