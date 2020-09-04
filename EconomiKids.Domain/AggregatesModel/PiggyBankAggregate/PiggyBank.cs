using EconomiKids.Domain.Events;
using EconomiKids.Domain.SeedWork;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EconomiKids.Domain.AggregatesModel.PiggyBankAggregate
{
    /// <summary>
    /// Represents a piggy bank with balance and transactions
    /// </summary>
    public class PiggyBank : Entity, IAggregateRoot
    {
        /// <summary>
        ///  Start value to compute the current balance
        /// </summary>
        private decimal initialBalance;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialBalance">Start value to compute the current balance</param>
        public PiggyBank(decimal initialBalance)
        {
            Id = Guid.NewGuid();
            _transactions = new ConcurrentDictionary<Guid, PiggyBankTransaction>();
            InitialBalance = initialBalance;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PiggyBank()
        {
            Id = Guid.NewGuid();
            _transactions = new ConcurrentDictionary<Guid, PiggyBankTransaction>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Piggy bank Identifier</param>
        /// <param name="initialBalance">Start value to compute the current balance</param>
        /// <param name="transactions">Withdraw and deposit transactions that affect the current balance</param>
        public PiggyBank(Guid id, decimal initialBalance = 0, ConcurrentDictionary<Guid, PiggyBankTransaction> transactions = null)
        {
            Id = id;
            _transactions = transactions ?? new ConcurrentDictionary<Guid, PiggyBankTransaction>();
            InitialBalance = initialBalance;
        }

        /// <summary>
        /// Actual value concidering the initial balance and transactions
        /// </summary>
        public decimal CurrentBalance => InitialBalance + _transactions.Values.Sum(a => a.TransactionValue);

        /// <summary>
        /// Start value to compute the current balance
        /// </summary>
        public decimal InitialBalance
        {
            get { return initialBalance; }
            private set
            {
                //Initial balance cant be negative
                if (value < 0)
                    throw new ArgumentException(Properties.Resources.ValueCannotBeNegative, nameof(InitialBalance));

                initialBalance = value;
            }
        }

        /// <summary>
        /// Withdraw and deposit transactions that affect the current balance
        /// </summary>
        public IEnumerable<PiggyBankTransaction> Transactions => _transactions.Values.ToList().AsReadOnly();

        /// <summary>
        /// Withdraw and deposit transactions that affect the current balance
        /// </summary>
        private ConcurrentDictionary<Guid, PiggyBankTransaction> _transactions { get; set; }

        /// <summary>
        /// Deposit the given value into the Piggy Bank balance
        /// </summary>
        /// <param name="value"></param>
        public void Deposit(decimal value)
        {
            //Allow only greater than zero
            if (value <= 0)
                throw new ArgumentException(Properties.Resources.ValueMustBeGreaterThanZero);

            //Create transaction
            PiggyBankTransaction transaction = new PiggyBankTransaction(value);

            //Add transaction to piggy bank transactions list
            _transactions[transaction.Id] = transaction;

            AddDomainEvent(new PiggyBankDepositRegisteredDomainEvent(transaction));
        }

        /// <summary>
        /// Withraw the given value from the Piggy Bank balance
        /// </summary>
        /// <param name="value"></param>
        public void Withdraw(decimal value)
        {
            //To allow recieving positive or negative value
            if (value > 0)
            {
                value *= -1m;
            }

            //Verify sufficient balance
            if ((CurrentBalance - value) < 0)
                throw new InvalidOperationException(Properties.Resources.InsufficientBalanceToWithdraw);

            //Create transaction
            PiggyBankTransaction transaction = new PiggyBankTransaction(value);

            //Add transaction to piggy bank transactions list
            _transactions[transaction.Id] = transaction;

            AddDomainEvent(new PiggyBankWithdrawRegisteredDomainEvent(transaction));
        }
    }
}