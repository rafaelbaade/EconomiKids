using EconomiKids.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace EconomiKids.Domain.AggregatesModel.PiggyBankAggregate
{
    /// <summary>
    /// Repository for Piggy Bank
    /// </summary>
    public interface IPiggyBankRepository : IRepository<PiggyBank>
    {
        /// <summary>
        /// Persist the new piggy bank
        /// </summary>
        /// <param name="piggyBank">Piggy bank instance</param>
        /// <returns>Piggy bank after been persisted</returns>
        Task<PiggyBank> AddAsync(PiggyBank piggyBank);

        /// <summary>
        /// Persist the piggy bank changes
        /// </summary>
        /// <param name="piggyBank">Piggy bank instance</param>
        /// <returns></returns>
        Task UpdateAsync(PiggyBank piggyBank);

        /// <summary>
        /// Get a piggy bank by identifier
        /// </summary>
        /// <param name="piggyBanckId">Piggy banck identifier</param>
        /// <returns>Located piggy bank</returns>
        Task<PiggyBank> GetAsync(Guid piggyBanckId);
    }
}