USE [BookSellingDB]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 2023-07-31 11:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[SellerId] [int] NOT NULL,
	[SoldToCustomer] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2023-07-31 11:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2023-07-31 11:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[BookId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sellers]    Script Date: 2023-07-31 11:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sellers](
	[SellerId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (1, N'Book 1', N'Author A', CAST(19.99 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (2, N'Book 2', N'Author B', CAST(15.50 AS Decimal(10, 2)), 2, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (3, N'Book 3', N'Author C', CAST(12.99 AS Decimal(10, 2)), 3, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (4, N'Book 4', N'Author D', CAST(9.99 AS Decimal(10, 2)), 4, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (5, N'Book 5', N'Author E', CAST(14.95 AS Decimal(10, 2)), 5, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (6, N'Book 6', N'Author F', CAST(21.75 AS Decimal(10, 2)), 6, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (7, N'Book 7', N'Author G', CAST(18.99 AS Decimal(10, 2)), 7, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (8, N'Book 8', N'Author H', CAST(17.50 AS Decimal(10, 2)), 8, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (9, N'Book 9', N'Author I', CAST(11.99 AS Decimal(10, 2)), 9, NULL)
GO
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Price], [SellerId], [SoldToCustomer]) VALUES (10, N'Book 10', N'Author J', CAST(16.99 AS Decimal(10, 2)), 10, NULL)
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (1, N'customer1', N'password1')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (2, N'customer2', N'password2')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (3, N'customer3', N'password3')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (4, N'customer4', N'password4')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (5, N'customer5', N'password5')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (6, N'customer6', N'password6')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (7, N'customer7', N'password7')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (8, N'customer8', N'password8')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (9, N'customer9', N'password9')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (10, N'customer10', N'password10')
GO
INSERT [dbo].[Customers] ([CustomerId], [Username], [Password]) VALUES (11, N'a', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (1, N'Seller A', N'Location A', N'seller1', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (2, N'Seller B', N'Location B', N'seller2', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (3, N'Seller C', N'Location C', N'seller3', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (4, N'Seller D', N'Location D', N'seller4', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (5, N'Seller E', N'Location E', N'seller5', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (6, N'Seller F', N'Location F', N'seller6', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (7, N'Seller G', N'Location G', N'seller7', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (8, N'Seller H', N'Location H', N'seller8', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (9, N'Seller I', N'Location I', N'seller9', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (10, N'Seller J', N'Location J', N'seller10', N'a')
GO
INSERT [dbo].[Sellers] ([SellerId], [Name], [Location], [Username], [Password]) VALUES (11, N'Seller K', N'Location K', N'seller11', N'a')
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Sellers] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Sellers] ([SellerId])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Sellers]
GO
