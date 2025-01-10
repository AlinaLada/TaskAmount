using TaskAmount;

namespace TaskAmountTest
{
    [TestClass]
    public class AmountMoneyTest
    {
        [TestMethod]
        // Тест перевіряє, що порівняння двох однакових сум повертає 0.
        public void Compare_EqualAmounts_ShouldReturnZero()
        {
            // Arrange
            var money1 = new AmountMoney(100, "USD", 1); // Створення першої суми
            var money2 = new AmountMoney(100, "USD", 1); // Створення другої суми

            // Act
            var result = money1.Compare(money2); // Виклик методу Compare

            // Assert
            Assert.AreEqual(0, result); // Перевірка, що результат дорівнює 0
        }

        [TestMethod]
        // Тест перевіряє, що порівняння меншої суми з більшою повертає -1.
        public void Compare_LessAmounts_ShouldReturnMinusOne()
        {
            // Arrange
            var money1 = new AmountMoney(100, "USD", 1); // Менша сума
            var money2 = new AmountMoney(300, "USD", 1); // Більша сума

            // Act
            var result = money1.Compare(money2); // Виклик методу Compare

            // Assert
            Assert.AreEqual(-1, result); // Перевірка, що результат дорівнює -1
        }

        [TestMethod]
        // Тест перевіряє, що порівняння більшої суми з меншою повертає 1.
        public void Compare_LangerAmounts_ShouldReturnOne()
        {
            // Arrange
            var money1 = new AmountMoney(500, "USD", 1); // Більша сума
            var money2 = new AmountMoney(300, "USD", 1); // Менша сума

            // Act
            var result = money1.Compare(money2); // Виклик методу Compare

            // Assert
            Assert.AreEqual(1, result); // Перевірка, що результат дорівнює 1
        }

        [TestMethod]
        // Тест перевіряє порівняння сум у різних валютах із різними курсами.
        public void Compare_LangerAmountsWithDifferentCurrencies_ShouldReturnOne()
        {
            // Arrange
            var money1 = new AmountMoney(450, "EUR", 1.2m); // Сума в євро
            var money2 = new AmountMoney(500, "USD", 1);    // Сума в доларах

            // Act
            var result = money1.Compare(money2); // Виклик методу Compare

            // Assert
            Assert.AreEqual(1, result); // Перевірка, що результат дорівнює 1
        }

        [TestMethod]
        // Тест перевіряє, що виклик методу Compare з null параметром викидає виняток.
        public void Compare_NullOther_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(50m, "USD", 1m); 

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => money.Compare(null)); // Перевірка на виняток
        }

        [TestMethod]
        // Тест перевіряє додавання сум у тій самій валюті.
        public void Add_SameCurrency_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50, "USD", 1); // Перша сума
            var money2 = new AmountMoney(50, "USD", 1); // Друга сума

            // Act
            var result = money1.Add(money2); // Виклик методу Add

            // Assert
            Assert.AreEqual(100, result.SizeOfAmount); // Перевірка суми
            Assert.AreEqual("USD", result.CurrencyCode); // Перевірка валюти
        }

        [TestMethod]
        // Тест перевіряє додавання сум у різних валютах із курсом конвертації.
        public void Add_SameCurrencyWithDifferentCurrencies_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50, "USD", 1); // Сума в доларах
            var money2 = new AmountMoney(50, "EUR", 1.2m); // Сума в євро

            // Act
            var result = money1.Add(money2); // Виклик методу Add

            // Assert
            Assert.AreEqual(110, result.SizeOfAmount); // Перевірка суми
            Assert.AreEqual("USD", result.CurrencyCode); // Перевірка валюти
        }

        [TestMethod]
        // Тест перевіряє додавання дробових чисел.
        public void Add_SameDecimalNumbers_ShouldReturnCorrectAmount()
        {
            // Arrange
            var money1 = new AmountMoney(50.5m, "USD", 1); // Перша сума
            var money2 = new AmountMoney(50.6m, "USD", 1); // Друга сума

            // Act
            var result = money1.Add(money2); // Виклик методу Add

            // Assert
            Assert.AreEqual(101.1m, result.SizeOfAmount); // Перевірка суми
            Assert.AreEqual("USD", result.CurrencyCode); // Перевірка валюти
        }

        [TestMethod]
        // Тест перевіряє, що виклик методу Add з null параметром викидає виняток.
        public void Add_NullOther_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(50m, "USD", 1m);

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => money.Add(null)); // Перевірка на виняток
        }

        [TestMethod]
        // Тест перевіряє конвертацію валюти з правильним курсом.
        public void ConvertToUSD_ValidExchangeRate_ShouldReturnCorrectValue()
        {
            // Arrange
            var money = new AmountMoney(50, "EUR", 1.2m); // Сума в євро

            // Act
            var result = money.ConvertToUSD(); // Виклик методу ConvertToUSD

            // Assert
            Assert.AreEqual(60, result); // Перевірка результату
        }

        [TestMethod]
        // Тест перевіряє, що конвертація з неправильним курсом викидає виняток.
        public void ConvertToUSD_InvalidExchangeRate_ShouldThrowException()
        {
            // Arrange
            var money = new AmountMoney(100m, "USD", 0); // Нульовий курс

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToUSD()); // Перевірка на виняток
        }

        [TestMethod]
        // Тест перевіряє, чи метод Equals повертає true для двох об'єктів з однаковими властивостями.
        public void Equals_SameProperties_ShouldReturnTrue()
        {
            // Arrange
            var money1 = new AmountMoney(100m, "USD", 1m); // Перший об'єкт з сумою, валютою та курсом
            var money2 = new AmountMoney(100m, "USD", 1m); // Другий об'єкт з такими самими значеннями

            // Act
            var result = money1.Equals(money2); // Виклик методу Equals

            // Assert
            Assert.IsTrue(result); // Перевірка, що об'єкти рівні
        }

        [TestMethod]
        // Тест перевіряє, чи метод Equals повертає false для об'єктів з різними властивостями.
        public void Equals_DifferentProperties_ShouldReturnFalse()
        {
            // Arrange
            var money1 = new AmountMoney(100m, "USD", 1m); // Перший об'єкт
            var money2 = new AmountMoney(200m, "USD", 1m); // Другий об'єкт з іншою сумою

            // Act
            var result = money1.Equals(money2); // Виклик методу Equals

            // Assert
            Assert.IsFalse(result); // Перевірка, що об'єкти не рівні
        }

        [TestMethod]
        // Тест перевіряє, чи метод ToString повертає відформатований рядок з даними об'єкта.
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var money = new AmountMoney(100m, "USD", 1m); // Об'єкт із сумою, валютою та курсом

            // Act
            var result = money.ToString(); // Виклик методу ToString

            // Assert
            Assert.AreEqual("100 USD (Rate: 1)", result); // Перевірка, що рядок відформатований правильно
        }
    }
}