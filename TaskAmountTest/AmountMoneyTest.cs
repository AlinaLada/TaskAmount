using TaskAmount;

namespace TaskAmountTest
{
    [TestClass]
    public class AmountMoneyTest
    {
        [TestMethod]
        // ���� ��������, �� ��������� ���� ��������� ��� ������� 0.
        public void Compare_EqualAmounts_ShouldReturnZero()
        {
            // Arrange
            var money1 = new AmountMoney(100, "USD", 1); // ��������� ����� ����
            var money2 = new AmountMoney(100, "USD", 1); // ��������� ����� ����

            // Act
            var result = money1.Compare(money2); // ������ ������ Compare

            // Assert
            Assert.AreEqual(0, result); // ��������, �� ��������� ������� 0
        }

        [TestMethod]
        // ���� ��������, �� ��������� ����� ���� � ������ ������� -1.
        public void Compare_LessAmounts_ShouldReturnMinusOne()
        {
            // Arrange
            var money1 = new AmountMoney(100, "USD", 1); // ����� ����
            var money2 = new AmountMoney(300, "USD", 1); // ������ ����

            // Act
            var result = money1.Compare(money2); // ������ ������ Compare

            // Assert
            Assert.AreEqual(-1, result); // ��������, �� ��������� ������� -1
        }

        [TestMethod]
        // ���� ��������, �� ��������� ����� ���� � ������ ������� 1.
        public void Compare_LangerAmounts_ShouldReturnOne()
        {
            // Arrange
            var money1 = new AmountMoney(500, "USD", 1); // ������ ����
            var money2 = new AmountMoney(300, "USD", 1); // ����� ����

            // Act
            var result = money1.Compare(money2); // ������ ������ Compare

            // Assert
            Assert.AreEqual(1, result); // ��������, �� ��������� ������� 1
        }

        [TestMethod]
        // ���� �������� ��������� ��� � ����� ������� �� ������ �������.
        public void Compare_LangerAmountsWithDifferentCurrencies_ShouldReturnOne()
        {
            // Arrange
            var money1 = new AmountMoney(450, "EUR", 1.2m); // ���� � ����
            var money2 = new AmountMoney(500, "USD", 1);    // ���� � �������

            // Act
            var result = money1.Compare(money2); // ������ ������ Compare

            // Assert
            Assert.AreEqual(1, result); // ��������, �� ��������� ������� 1
        }

        [TestMethod]
        // ���� ��������, �� ������ ������ Compare � null ���������� ������ �������.
        public void Compare_NullOther_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(50m, "USD", 1m); 

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => money.Compare(null)); // �������� �� �������
        }

        [TestMethod]
        // ���� �������� ��������� ��� � �� ���� �����.
        public void Add_SameCurrency_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50, "USD", 1); // ����� ����
            var money2 = new AmountMoney(50, "USD", 1); // ����� ����

            // Act
            var result = money1.Add(money2); // ������ ������ Add

            // Assert
            Assert.AreEqual(100, result.SizeOfAmount); // �������� ����
            Assert.AreEqual("USD", result.CurrencyCode); // �������� ������
        }

        [TestMethod]
        // ���� �������� ��������� ��� � ����� ������� �� ������ �����������.
        public void Add_SameCurrencyWithDifferentCurrencies_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50, "USD", 1); // ���� � �������
            var money2 = new AmountMoney(50, "EUR", 1.2m); // ���� � ����

            // Act
            var result = money1.Add(money2); // ������ ������ Add

            // Assert
            Assert.AreEqual(110, result.SizeOfAmount); // �������� ����
            Assert.AreEqual("USD", result.CurrencyCode); // �������� ������
        }

        [TestMethod]
        // ���� �������� ��������� �������� �����.
        public void Add_SameDecimalNumbers_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50.5m, "USD", 1); // ����� ����
            var money2 = new AmountMoney(50.6m, "USD", 1); // ����� ����

            // Act
            var result = money1.Add(money2); // ������ ������ Add

            // Assert
            Assert.AreEqual(101.1m, result.SizeOfAmount); // �������� ����
            Assert.AreEqual("USD", result.CurrencyCode); // �������� ������
        }

        [TestMethod]
        // ���� ��������, �� ������ ������ Add � null ���������� ������ �������.
        public void Add_NullOther_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(50m, "USD", 1m);

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => money.Add(null)); // �������� �� �������
        }

        [TestMethod]
        // ���� �������� ����������� ������ � ���������� ������.
        public void ConvertToUSD_ValidExchangeRate_ShouldReturnCorrectValue()
        {
            // Arrange
            var money = new AmountMoney(50, "EUR", 1.2m); // ���� � ����

            // Act
            var result = money.ConvertToUSD(); // ������ ������ ConvertToUSD

            // Assert
            Assert.AreEqual(60, result); // �������� ����������
        }

        [TestMethod]
        // ���� ��������, �� ����������� � ������������ ������ ������ �������.
        public void ConvertToUSD_InvalidExchangeRate_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(100m, "USD", 0); // �������� ����

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToUSD()); // �������� �� �������
        }

        [TestMethod]
        // ���� ��������, �� ����� Equals ������� true ��� ���� ��'���� � ���������� �������������.
        public void Equals_SameProperties_ShouldReturnTrue()
        {
            // Arrange
            var money1 = new AmountMoney(100m, "USD", 1m); // ������ ��'��� � �����, ������� �� ������
            var money2 = new AmountMoney(100m, "USD", 1m); // ������ ��'��� � ������ ������ ����������

            // Act
            var result = money1.Equals(money2); // ������ ������ Equals

            // Assert
            Assert.IsTrue(result); // ��������, �� ��'���� ���
        }

        [TestMethod]
        // ���� ��������, �� ����� Equals ������� false ��� ��'���� � ������ �������������.
        public void Equals_DifferentProperties_ShouldReturnFalse()
        {
            // Arrange
            var money1 = new AmountMoney(100m, "USD", 1m); // ������ ��'���
            var money2 = new AmountMoney(200m, "USD", 1m); // ������ ��'��� � ����� �����

            // Act
            var result = money1.Equals(money2); // ������ ������ Equals

            // Assert
            Assert.IsFalse(result); // ��������, �� ��'���� �� ���
        }

        [TestMethod]
        // ���� ��������, �� ����� ToString ������� �������������� ����� � ������ ��'����.
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var money = new AmountMoney(100m, "USD", 1m); // ��'��� �� �����, ������� �� ������

            // Act
            var result = money.ToString(); // ������ ������ ToString

            // Assert
            Assert.AreEqual("100 USD (Rate: 1)", result); // ��������, �� ����� �������������� ���������
        }
    }
}