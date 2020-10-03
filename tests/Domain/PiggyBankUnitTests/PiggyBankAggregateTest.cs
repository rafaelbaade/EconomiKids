using EconomiKids.Domain.AggregatesModel.PiggyBankAggregate;
using System;
using Xunit;

namespace PiggyBankUnitTests
{
    public class PiggyBankAggregateTest
    {
        [Fact]
        public void Create_transaction_success()
        {
            //Arrange
            decimal value = 10;

            //Act
            var piggyBankTransaction = new PiggyBankTransaction(value);

            //Assert
            Assert.NotNull(piggyBankTransaction);
        }

        [Fact]
        public void Invalid_transaction_value()
        {
            //Arrange
            decimal value = 0;

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new PiggyBankTransaction(value));
        }

        [Fact]
        public void Deposit_increases_balance()
        {
            //Arrange
            var value = 10;
            var expectedResult = 10;

            //Act 
            var fakePiggyBank = new PiggyBank();
            fakePiggyBank.Deposit(value);

            //Assert
            Assert.Equal(fakePiggyBank.CurrentBalance, expectedResult);
        }

        [Fact]
        public void Withdraw_decreases_balance()
        {
            //Arrange
            var initialBalance = 10;
            var value = 10;
            var expectedResult = 0;

            //Act 
            var fakePiggyBank = new PiggyBank(initialBalance);
            fakePiggyBank.Withdraw(value);

            //Assert
            Assert.Equal(fakePiggyBank.CurrentBalance, expectedResult);
        }

        [Fact]
        public void Cant_withdraw_more_than_current_balance()
        {
            //Arrange
            var initialBalance = 10;
            var value = 11;

            //Act 
            var fakePiggyBank = new PiggyBank(initialBalance);

            //Act - Assert
            Assert.Throws<InvalidOperationException>(() => fakePiggyBank.Withdraw(value));
        }


        [Fact]
        public void Deposit_raises_new_event()
        {
            //Arrange
            var value = 10;
            var expectedResult = 1;

            //Act 
            var fakePiggyBank = new PiggyBank();
            fakePiggyBank.Deposit(value);

            //Assert
            Assert.Equal(fakePiggyBank.DomainEvents.Count, expectedResult);
        }

        [Fact]
        public void Withdraw_raises_new_event()
        {
            //Arrange
            var initialBalance = 10;
            var value = 10;
            var expectedResult = 1;

            //Act 
            var fakePiggyBank = new PiggyBank(initialBalance);
            fakePiggyBank.Withdraw(value);

            //Assert
            Assert.Equal(fakePiggyBank.DomainEvents.Count, expectedResult);
        }
    }
}