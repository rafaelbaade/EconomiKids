using EconomiKids.Domain.AggregatesModel.PiggyBankAggregate;
using MediatR;

namespace EconomiKids.Domain.Events
{
    /// <summary>
    /// Event thown when a withraw is registered
    /// </summary>
    public class PiggyBankWithdrawRegisteredDomainEvent : INotification
    {
        /// <summary>
        /// Transaction related to the withdraw
        /// </summary>
        public PiggyBankTransaction Transaction { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transaction">Transaction related to the withdraw</param>
        public PiggyBankWithdrawRegisteredDomainEvent(PiggyBankTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}