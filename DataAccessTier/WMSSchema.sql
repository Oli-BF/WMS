CREATE TABLE [warehouses] (
  [warehouse_id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [locations] (
  [location_id] int PRIMARY KEY IDENTITY(1, 1),
  [warehouse_id] int NOT NULL,
  [location_code] nvarchar(10) NOT NULL,
  [allocated] bit NOT NULL DEFAULT (0),
  [put_sequence] int NOT NULL,
  [pick_sequence] int NOT NULL
)
GO

CREATE TABLE [accounts] (
  [account_id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [products] (
  [product_id] int PRIMARY KEY IDENTITY(1, 1),
  [account_id] int NOT NULL,
  [title] nvarchar(255) NOT NULL,
  [sku] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [stock] (
  [stock_id] int PRIMARY KEY IDENTITY(1, 1),
  [account_id] int NOT NULL,
  [product_id] int NOT NULL,
  [warehouse_id] int NOT NULL,
  [location_id] int NOT NULL,
  [quantity] int NOT NULL,
  [allocated_quantity] int NOT NULL DEFAULT (0),
  [availability_status] bit NOT NULL DEFAULT (1),
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

CREATE TABLE [receipts] (
  [receipt_id] int PRIMARY KEY IDENTITY(1, 1),
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  [receipt_reference] nvarchar(255) NOT NULL,
  [account_id] int NOT NULL,
  [warehouse_id] int NOT NULL,
  [expected_date] datetime NOT NULL,
  [receipted_date] datetime
)
GO

CREATE TABLE [receipt_lines] (
  [receipt_line_id] int PRIMARY KEY IDENTITY(1, 1),
  [receipt_id] int NOT NULL,
  [product_id] int NOT NULL,
  [quantity] int NOT NULL,
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

CREATE TABLE [orders] (
  [order_id] int PRIMARY KEY IDENTITY(1, 1),
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  [order_reference] nvarchar(255) NOT NULL,
  [account_id] int NOT NULL,
  [warehouse_id] int NOT NULL,
  [status] nvarchar(255) NOT NULL DEFAULT 'created',
  [dispatch_date] datetime NOT NULL,
  [first_name] nvarchar(50) NOT NULL,
  [last_name] nvarchar(50) NOT NULL,
  [address_line_1] nvarchar(255) NOT NULL,
  [address_line_2] nvarchar(255) NOT NULL,
  [city] nvarchar(85) NOT NULL,
  [postcode] nvarchar(10) NOT NULL
)
GO

CREATE TABLE [order_lines] (
  [order_line_id] int PRIMARY KEY IDENTITY(1, 1),
  [order_id] int NOT NULL,
  [product_id] int NOT NULL,
  [quantity] int NOT NULL,
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

CREATE TABLE [picks] (
  [pick_id] int PRIMARY KEY IDENTITY(1, 1),
  [order_id] int NOT NULL,
  [order_line_id] int NOT NULL,
  [location_id] int NOT NULL,
  [location_code] nvarchar(255) NOT NULL,
  [product_id] int NOT NULL,
  [product_title] nvarchar(255) NOT NULL,
  [quantity] int NOT NULL,
  [date_added] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

CREATE TABLE [users] (
  [user_id] int PRIMARY KEY IDENTITY(1, 1),
  [first_name] nvarchar(50) NOT NULL,
  [last_name] nvarchar(50) NOT NULL,
  [telephone] bigint NOT NULL,
  [email] nvarchar(320) UNIQUE NOT NULL,
  [role] nvarchar(50) NOT NULL,
  [password] nvarchar(50) NOT NULL
)
GO

ALTER TABLE [locations] ADD FOREIGN KEY ([warehouse_id]) REFERENCES [warehouses] ([warehouse_id])
GO

ALTER TABLE [products] ADD FOREIGN KEY ([account_id]) REFERENCES [accounts] ([account_id])
GO

ALTER TABLE [stock] ADD FOREIGN KEY ([account_id]) REFERENCES [accounts] ([account_id])
GO

ALTER TABLE [stock] ADD FOREIGN KEY ([product_id]) REFERENCES [products] ([product_id])
GO

ALTER TABLE [stock] ADD FOREIGN KEY ([warehouse_id]) REFERENCES [warehouses] ([warehouse_id])
GO

ALTER TABLE [stock] ADD FOREIGN KEY ([location_id]) REFERENCES [locations] ([location_id])
GO

ALTER TABLE [receipts] ADD FOREIGN KEY ([account_id]) REFERENCES [accounts] ([account_id])
GO

ALTER TABLE [receipts] ADD FOREIGN KEY ([warehouse_id]) REFERENCES [warehouses] ([warehouse_id])
GO

ALTER TABLE [receipt_lines] ADD FOREIGN KEY ([receipt_id]) REFERENCES [receipts] ([receipt_id])
GO

ALTER TABLE [receipt_lines] ADD FOREIGN KEY ([product_id]) REFERENCES [products] ([product_id])
GO

ALTER TABLE [orders] ADD FOREIGN KEY ([account_id]) REFERENCES [accounts] ([account_id])
GO

ALTER TABLE [orders] ADD FOREIGN KEY ([warehouse_id]) REFERENCES [warehouses] ([warehouse_id])
GO

ALTER TABLE [order_lines] ADD FOREIGN KEY ([order_id]) REFERENCES [orders] ([order_id])
GO

ALTER TABLE [order_lines] ADD FOREIGN KEY ([product_id]) REFERENCES [products] ([product_id])
GO

ALTER TABLE [picks] ADD FOREIGN KEY ([order_id]) REFERENCES [orders] ([order_id])
GO

ALTER TABLE [picks] ADD FOREIGN KEY ([order_line_id]) REFERENCES [order_lines] ([order_line_id])
GO

ALTER TABLE [picks] ADD FOREIGN KEY ([location_id]) REFERENCES [locations] ([location_id])
GO

ALTER TABLE [picks] ADD FOREIGN KEY ([product_id]) REFERENCES [products] ([product_id])
GO
