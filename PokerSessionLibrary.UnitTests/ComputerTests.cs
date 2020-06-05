using NUnit.Framework;

namespace PokerSessionLibrary.UnitTests
{
    [TestFixture]
    class ComputerTests
    {
        private IPlayer computer;

        [SetUp]
        public void TestSetup()
        {
            computer = GetTestPlayer();
        }

        private static IPlayer GetTestPlayer()
        {
            return PlayerFactory.CreatePlayer(PlayerType.Computer);
        }

        [Test]
        public void Ante_ResetsStackWhenNegative_ReturnsTrue()
        {
            computer.Stack = 0;

            decimal expected = 1;
            decimal actual = computer.Ante();

            Assert.That(actual == expected);

        }

        [Test]
        public void Bet_ResetsStackWhenNegative_ReturnsTrue()
        {
            computer.Stack = 0;

            decimal expected = 1;
            decimal actual = computer.Bet();

            Assert.That(actual == expected);

        }

    }
}
