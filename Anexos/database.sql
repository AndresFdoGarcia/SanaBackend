--------------------------------------------------------------------
USE master;

-- Drop database
IF DB_ID('SanaEcDB') IS NOT NULL DROP DATABASE SanaEcDB;

-- If database could not be created due to open connections, abort
IF @@ERROR = 3702 
   RAISERROR('Database cannot be dropped because there are still open connections.', 127, 127) WITH NOWAIT, LOG;

-- Create database
CREATE DATABASE SanaEcDB;
GO

USE SanaEcDB;
GO
---------------------------------------------------------------------
-- Create Schemas
---------------------------------------------------------------------

CREATE SCHEMA Production AUTHORIZATION dbo;
GO
CREATE SCHEMA Sales AUTHORIZATION dbo;
GO

---------------------------------------------------------------------
-- Create Tables
---------------------------------------------------------------------
CREATE TABLE Sales.Customers (
  custid INT NOT NULL IDENTITY,
  firstname VARCHAR(100) NOT NULL,
  lastname VARCHAR(100) NOT NULL,
  loginname VARCHAR(100)NOT NULL
  CONSTRAINT PK_Customers PRIMARY KEY(custid)
);

CREATE TABLE Production.Categories (
  categoryid INT NOT NULL PRIMARY KEY,
  categoryname VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Production.Products (
  productid INT NOT NULL IDENTITY,
  productname VARCHAR(100) NOT NULL,   
  price MONEY NOT NULL,
  stockqty INT,
  image VARCHAR(100),
  descript text,
  CONSTRAINT PK_Products PRIMARY KEY(productid),  
);

CREATE NONCLUSTERED INDEX idx_nc_productname ON Production.Products(price);

CREATE TABLE Production.ProductCategories (
  productid INT,
  categoryid INT,
  PRIMARY KEY (productid, categoryid),
  FOREIGN KEY (productid) REFERENCES Production.Products(productid),
  FOREIGN KEY (categoryid) REFERENCES Production.Categories(categoryid)
);

CREATE TABLE Sales.Orders (
  orderid INT IDENTITY(1000,1) NOT NULL PRIMARY KEY,
  custid INT NOT NULL,
  orderdate DATETIME NOT NULL,
  totalamount MONEY NOT NULL,
  CONSTRAINT FK_Orders_Customers FOREIGN KEY(custid) REFERENCES Sales.Customers(custid)
);

CREATE NONCLUSTERED INDEX idx_nc_custid ON Sales.Orders(custid);
CREATE NONCLUSTERED INDEX idx_nc_orderdate ON Sales.Orders(orderdate);


CREATE TABLE Sales.OrderDetails (
  orderid INT NOT NULL,
  productid INT NOT NULL,
  Quantity INT NOT NULL,
  unitprice MONEY NOT NULL,
  CONSTRAINT PK_OrderDetails PRIMARY KEY(orderid, productid),
  CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY(orderid)
    REFERENCES Sales.Orders(orderid), 
  CONSTRAINT FK_OrderDetails_Products FOREIGN KEY(productid)
    REFERENCES Production.Products(productid), 
  FOREIGN KEY (orderid) REFERENCES Sales.Orders(orderid), 
  FOREIGN KEY (productid) REFERENCES Production.Products(productid)
);

CREATE NONCLUSTERED INDEX idx_nc_orderid   ON Sales.OrderDetails(orderid);
CREATE NONCLUSTERED INDEX idx_nc_productid ON Sales.OrderDetails(productid);

---------------------------------------------------------------------------------------
INSERT INTO Sales.Customers (firstname, lastname, loginname)
VALUES 
('John', 'Doe', 'johndoe'),
('Jane', 'Smith', 'janesmith'),
('Michael', 'Brown', 'michaelbrown'),
('Emily', 'Davis', 'emilydavis'),
('David', 'Wilson', 'davidwilson');

-------------------------------------------------
INSERT INTO Production.Categories (categoryid, categoryname)
VALUES 
(1, 'Laptops'),
(2, 'Smartphones'),
(3, 'Gaming Consoles'),
(4, 'Accessories'),
(5, 'Software Licenses');

-------------------------------------------------
INSERT INTO Production.Products (productname, price, stockqty, image, descript)
VALUES 
('Gaming Laptop', 1499.99, 25, 'https://i.imgur.com/OKn1KFI.jpeg', 'High-performance laptop for gaming and productivity.'), 
('Ultrabook Laptop', 1199.99, 30, 'https://i.imgur.com/ItHcq7o.jpeg', 'Lightweight, powerful laptop for on-the-go use.'), 
('Smartphone - Android', 799.99, 50, 'https://i.imgur.com/5INKwQ9.jpeg', 'Feature-rich Android smartphone with a sleek design.'),
('Smartphone - iOS', 999.99, 40, 'https://i.imgur.com/yb9UQKL.jpeg', 'Latest iOS smartphone with advanced features.'),
('Smartwatch', 399.99, 60, 'https://i.imgur.com/LGk9Jn2.jpeg', 'Wearable device with health and fitness tracking.'),
('Xbox Series X', 499.99, 20, 'https://i.imgur.com/ZANVnHE.jpeg', 'Next-gen gaming console with 4K graphics.'),
('PlayStation 5', 499.99, 15, 'https://i.imgur.com/3FBkBfa.jpeg', 'High-performance console for immersive gaming.'),
('Windows 10 License', 139.99, 200, 'https://digitallicense.shop/wp-content/uploads/2023/06/microsoft-windows-10-pro-large-2.jpg', 'Official license for Windows 10 operating system.'),
('Microsoft Office License', 199.99, 150, 'https://i0.wp.com/www.fastsoftwares.com/wp-content/uploads/2020/07/Microsoft-Office-2019-Standard-Open-License-1-2.png', 'Full suite of Office productivity tools.'),
('Gaming Headset', 79.99, 100, 'https://i.imgur.com/yVeIeDa.jpeg', 'Noise-canceling headset for an enhanced gaming experience.'),
('External Hard Drive 1TB', 59.99, 120, 'https://i.imgur.com/VzapuIw.jpeg', 'Portable storage with fast transfer speeds.'),
('USB-C Hub', 29.99, 80, 'https://i.imgur.com/MH2vrJL.jpeg', 'Multi-port hub for USB-C devices.'),
('Wireless Mouse', 49.99, 200, 'https://i.imgur.com/w3Y8NwQ.jpeg', 'Ergonomic wireless mouse with high precision.'),
('Gaming Keyboard', 129.99, 100, 'https://i.imgur.com/qCMQgob.jpeg', 'Mechanical keyboard with customizable RGB lighting.'),
('Monitor 24"', 179.99, 35, 'https://i.imgur.com/bX9V0hw.jpeg', 'Full HD monitor with vibrant color display.'),
('Xbox Game Pass (1 year)', 119.99, 300, 'https://cdn.cdkeys.com/700x700/media/catalog/product/d/v/dvs51v56ds_1.jpg', 'Access to a library of Xbox games for one year.'),
('PlayStation Plus (1 year)', 59.99, 300, 'https://gamersdealz.com/wp-content/uploads/2024/02/playstation-plus-psn-365-days-12-months-1-year-usa-membership-cd-key.jpg', 'One-year membership for online gaming and free games.'),
('Antivirus Software', 39.99, 400, 'https://i5.walmartimages.com/seo/BullZIGA-Antivirus-Software-for-Windows-Mac-OS-Android-iOS-1-Year-1-Device_d1f70b45-8895-44f1-bff3-66c4fdf725e9.e290a103938bfb946f3767296a50c015.jpeg', 'Comprehensive protection against malware and viruses.'),
('VR Headset', 299.99, 50, 'https://i.imgur.com/0qQBkxX.jpg', 'Immersive virtual reality headset for gaming.'),
('Bluetooth Speaker', 149.99, 75, 'https://i.imgur.com/x8u3aK6.jpeg', 'Portable speaker with high-quality sound.');

-----------------------------------------------------------
INSERT INTO Production.ProductCategories (productid, categoryid)
VALUES 

(1, 1), (2, 1), 
(3, 2), (4, 2), 
(5, 2), 
(6, 3), (7, 3), 
(8, 5), (9, 5), (10, 4), 
(11, 4), (12, 4), (13, 4), (14, 4), 
(15, 4), 
(16, 5), (17, 5), 
(18, 5), 
(19, 4), (20, 4), 


(1, 4),  
(3, 4),  
(6, 4),  
(8, 4)  

---------------------------------------------------------------------
INSERT INTO Sales.Orders (custid, orderdate, totalamount)
VALUES 
(1, '2024-09-01', 2549.95), 
(1, '2024-09-10', 1399.95), 
(2, '2024-09-02', 1999.95), 
(2, '2024-09-12', 1199.95), 
(3, '2024-09-03', 1599.95), 
(3, '2024-09-11', 999.95), 
(4, '2024-09-04', 849.95), 
(4, '2024-09-13', 1999.95), 
(5, '2024-09-05', 2499.95), 
(5, '2024-09-14', 1899.95);



INSERT INTO Sales.OrderDetails (orderid, productid, quantity, unitprice)
VALUES 
-- Order 1000 - John Doe
(1000, 1, 1, 1499.99), (1000, 5, 1, 399.99), (1000, 10, 1, 79.99), (1000, 8, 1, 139.99),
-- Order 1001 - John Doe
(1001, 3, 1, 799.99), (1001, 12, 1, 29.99), (1001, 13, 1, 49.99), (1001, 9, 1, 199.99),
-- Order 1002 - Jane Smith
(1002, 2, 1, 1199.99), (1002, 6, 1, 499.99), (1002, 14, 1, 129.99), (1002, 15, 1, 179.99),
-- Order 1003 - Jane Smith
(1003, 4, 1, 999.99), (1003, 18, 2, 39.99), (1003, 11, 1, 59.99), (1003, 7, 1, 499.99),
-- Order 1004 - Michael Brown
(1004, 16, 2, 119.99), (1004, 9, 1, 199.99), (1004, 17, 2, 59.99), (1004, 19, 1, 299.99),
-- Order 1005 - Michael Brown
(1005, 1, 1, 1499.99), (1005, 3, 1, 799.99), (1005, 20, 1, 149.99), (1005, 8, 1, 139.99),
-- Order 1006 - Emily Davis
(1006, 14, 1, 129.99), (1006, 5, 1, 399.99), (1006, 16, 1, 119.99), (1006, 12, 1, 29.99),
-- Order 1007 - Emily Davis
(1007, 2, 1, 1199.99), (1007, 10, 1, 79.99), (1007, 15, 1, 179.99), (1007, 17, 2, 59.99),
-- Order 1008 - David Wilson
(1008, 6, 1, 499.99), (1008, 13, 2, 49.99), (1008, 20, 1, 149.99), (1008, 8, 1, 139.99),
-- Order 1009 - David Wilson
(1009, 7, 1, 499.99), (1009, 11, 1, 59.99), (1009, 9, 1, 199.99), (1009, 19, 1, 299.99);

-------------------------------------------------------------------------------------------------------
CREATE TYPE OrderDetailsType AS TABLE (
    productid INT,
    quantity INT
);

------------------------------------------------------------------------------------------------------

CREATE PROCEDURE CreateOrder
    @CustomerId INT,
    @TotalAmount MONEY,
    @OrderDetails OrderDetailsType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO Sales.Orders (custid, orderdate, totalamount)
        VALUES (@CustomerId, GETDATE(), @TotalAmount);

        DECLARE @NewOrderId INT = SCOPE_IDENTITY();
        
        INSERT INTO Sales.OrderDetails (orderid, productid, Quantity, unitprice)
        SELECT 
            @NewOrderId,
            od.productid,
            od.quantity,
            p.price
        FROM @OrderDetails od
        INNER JOIN Production.Products p ON od.productid = p.productid;
        
        UPDATE p
        SET p.stockqty = p.stockqty - od.quantity
        FROM Production.Products p
        INNER JOIN @OrderDetails od ON p.productid = od.productid;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
------------------------------------------