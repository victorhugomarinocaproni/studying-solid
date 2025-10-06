using Microsoft.Data.Sqlite;
using Solid.Database;
using Solid.Entities;
using Solid.Repositories;
using Solid.Services;
using Solid.Strategies;

var inventoryItem1 = new InventoryItem("produto1", 100);
var inventoryItem2 = new InventoryItem("produto2", 50);
var inventoryItem3 = new InventoryItem("produto3", 75);

var inventoryItems = new List<InventoryItem>
{
    inventoryItem1,
    inventoryItem2,
    inventoryItem3
};

var inventory = new Inventory(inventoryItems);

var orderItem1 = new OrderItem(
    "produto1",
    100f,
    2,
    Discount.Normal);
    
var orderItem2 = new OrderItem(
    "produto2",
    50f,
    1,
    Discount.TenPercent);

var orderItems1 = new List<OrderItem>
{
    orderItem1,
    orderItem2
};

var inventoryService = new InventoryService(new DefaultInventoryStrategy());

var dbContext = new SqliteDbContext
{
    Connection = new SqliteConnection("DataSource=loja.db")
};
dbContext.Connect();

var emailSender = new EmailSender();
var notificationService = new NotificationService(emailSender);

var defaultOrderCreator = new DefaultOrderCreator(new DiscountService());

var orderRepository = new OrderRepository(dbContext);
var orderService = new OrderService(orderRepository, notificationService, defaultOrderCreator);

var cardPaymentProcessor = new CardPaymentProcessor(orderService);
var paymentProcessorService = new PaymentProcessorService(cardPaymentProcessor);

var isInventoryValidOrder1 = inventoryService.ValidateInventory(inventory, orderItems1);

if (isInventoryValidOrder1)
{
    var orderId = orderService.CreateOrder("João Silva", orderItems1, CustomerCategory.Normal);
    Console.WriteLine($"Order {orderId} created!");
    
    var order = orderService.GetOrder(orderId);
    paymentProcessorService.ProcessPayment(order, 250f);
    
    orderService.UpdateOrderStatus(order.Id, OrderStatus.Shipped);
    orderService.UpdateOrderStatus(order.Id, OrderStatus.Delivered);
}

var orderItem3 = new OrderItem(
    "produto3",
    200f,
    1,
    Discount.TwentyPercent);

var orderItems2 = new List<OrderItem>
{
    orderItem3
};

var isInventoryValidOrder2 = inventoryService.ValidateInventory(inventory, orderItems2);

var pixPaymentProcessor = new PixPaymentProcessor(orderService);
var paymentProcessorServicePix = new PaymentProcessorService(pixPaymentProcessor);

if (isInventoryValidOrder2)
{
    var orderId = orderService.CreateOrder("Maria Santos", orderItems2, CustomerCategory.Vip);
    Console.WriteLine($"Order {orderId} created!");
    var order = orderService.GetOrder(orderId);
    paymentProcessorServicePix.ProcessPayment(order, 160f);
}

var customerSpendingCalculator = new CustomerSpendingCalculator(orderRepository);
var customerReportGenerator = new CustomerReportGenerator(customerSpendingCalculator, orderRepository);
var salesReportGenerator = new SalesReportGenerator(orderRepository);

var reportGeneratorServiceCustomerInstance = new ReportGeneratorService(customerReportGenerator);
var reportGeneratorServiceSalesInstance = new ReportGeneratorService(salesReportGenerator);

reportGeneratorServiceSalesInstance.GenerateReport();
reportGeneratorServiceCustomerInstance.GenerateReport();
