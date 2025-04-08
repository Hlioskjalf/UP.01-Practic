--CREATE DATABASE Ovtsin_shop;
--GO

USE Ovtsin_Shop;
GO

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(30)
);
GO

INSERT INTO Category (CategoryName) VALUES
('Hats'),
('Gloves'),
('Scarves');
GO

CREATE TABLE Good (
    GoodId INT PRIMARY KEY IDENTITY(1,1),
    GoodName NVARCHAR(255),
    Price FLOAT,
    Picture NVARCHAR(50) NULL,
    Description NVARCHAR(255) NULL,
    CountGood INT,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

INSERT INTO Good (GoodName, Price, Picture, Description, CountGood, CategoryId) VALUES
('Knitted Hat', 500.00, 'hat1.jpg', 'Warm knitted hat', 10, 1),
('Leather Gloves', 800.00, 'gloves1.jpg', 'Leather gloves with warm lining', 5, 2),
('Wool Scarf', 600.00, 'scarf1.jpg', 'Wool scarf for cold weather', 8, 3),
('Felt Hat', 750.00, 'hat2.jpg', 'Felt hat with brim', 7, 1),
('Cotton Gloves', 350.00, 'gloves2.jpg', 'Cotton gloves for mild weather', 12, 2),
('Silk Scarf', 900.00, 'scarf2.jpg', 'Silk scarf for elegant look', 4, 3);
GO

CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(30),
    Password NVARCHAR(30),
    Role NVARCHAR(30)
);
GO

INSERT INTO [User] (UserName, Password, Role) VALUES
('admin', 'admin123', 'Admin'),
('user1', 'user123', 'User'),
('user2', 'user456', 'User');
GO

CREATE TABLE Sell (
    SellId INT PRIMARY KEY IDENTITY(1,1),
    GoodId INT,
    DateSell DATETIME,
    SellCount INT,
    FOREIGN KEY (GoodId) REFERENCES Good(GoodId)
);
GO

INSERT INTO Sell (GoodId, DateSell, SellCount) VALUES
(1, '2023-10-26 10:00:00', 2),
(2, '2023-10-26 11:30:00', 1),
(3, '2023-10-26 14:00:00', 3),
(1, '2023-10-27 09:00:00', 1),
(4, '2023-10-27 12:00:00', 2),
(5, '2023-10-27 15:00:00', 4);
GO

SELECT * FROM Good;
SELECT * FROM [User];
SELECT * FROM Sell;
