using EconomiKids.Domain.AggregatesModel.PiggyBankAggregate;
using MediatR;

namespace EconomiKids.Domain.Events
{
    /// <summary>
    /// Event thown when a deposit is registered
    /// </summary>
    internal class PiggyBankDepositRegisteredDomainEvent : INotification
    {
        /// <summary>
        /// Transaction related to the deposit
        /// </summary>
        public PiggyBankTransaction Transaction { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transaction">Transaction related to the deposit</param>
        public PiggyBankDepositRegisteredDomainEvent(PiggyBankTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}