CREATE TABLE [dbo].[CarTbl] (
    [RegNum]    INT IDENTITY(1,1) NOT NULL,
    [Brand]     VARCHAR(50) NOT NULL,
    [Model]     VARCHAR(50) NOT NULL,
    [Available] VARCHAR(50) NOT NULL,
    [Price]     VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RegNum] ASC)
);

CREATE TABLE [dbo].[BikeTbl] (
    [RegNum]    INT IDENTITY(1,1) NOT NULL,
    [Brand]     VARCHAR(50) NOT NULL,
    [Model]     VARCHAR(50) NOT NULL,
    [Available] VARCHAR(50) NOT NULL,
    [Price]     VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RegNum] ASC)
);

CREATE TABLE [dbo].[CustomerTbl] (
    [CustId]   INT IDENTITY(1,1) NOT NULL,
    [CustName] VARCHAR(50) NOT NULL,
    [CustAdd]  VARCHAR(50) NOT NULL,
    [Phone]    VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CustId] ASC)
);


CREATE TABLE [dbo].[RentalTbl] (
    [RentId]     INT IDENTITY(1,1) NOT NULL,
    [CarReg]     VARCHAR(50) NOT NULL,
    [CustName]   VARCHAR(50) NOT NULL,
    [RentDate]   VARCHAR(50) NOT NULL,
    [ReturnDate] VARCHAR(50) NOT NULL,
    [RentFee]    VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RentId] ASC)
);

CREATE TABLE [dbo].[ReturnTbl] (
    [ReturnId]   INT IDENTITY(1,1) NOT NULL,
    [CarReg]     VARCHAR(50) NOT NULL,
    [CustName]   VARCHAR(50) NOT NULL,
    [ReturnDate] VARCHAR(50) NOT NULL,
    [Delay]      VARCHAR(50) NOT NULL,
    [Fine]       VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ReturnId] ASC)
);

CREATE TABLE [dbo].[UserTbl] (
    [Id]    INT IDENTITY(1,1) NOT NULL,
    [Uname] VARCHAR(50) NOT NULL,
    [Upass] VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);





CREATE TABLE [dbo].[BikeCustomerTbl] (
    [CustId]   INT          IDENTITY (1, 1) NOT NULL,
    [CustName] VARCHAR (50) NOT NULL,
    [CustAdd]  VARCHAR (50) NOT NULL,
    [Phone]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CustId] ASC)
);

CREATE TABLE [dbo].[BikeRentalTbl] (
    [RentId]     INT          IDENTITY (1, 1) NOT NULL,
    [CarReg]     VARCHAR (50) NOT NULL,
    [CustName]   VARCHAR (50) NOT NULL,
    [RentDate]   VARCHAR (50) NOT NULL,
    [ReturnDate] VARCHAR (50) NOT NULL,
    [RentFee]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RentId] ASC)
);

CREATE TABLE [dbo].[BikeReturnTbl] (
    [ReturnId]   INT          IDENTITY (1, 1) NOT NULL,
    [CarReg]     VARCHAR (50) NOT NULL,
    [CustName]   VARCHAR (50) NOT NULL,
    [ReturnDate] VARCHAR (50) NOT NULL,
    [Delay]      VARCHAR (50) NOT NULL,
    [Fine]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ReturnId] ASC)
);


CREATE TABLE [dbo].[PaymentTbl] (
    [PaymentId]     INT IDENTITY(1,1) NOT NULL,
    [RentalId]      INT NULL,
    [CustId]        INT NULL,
    [Amount]        DECIMAL(10, 2) NULL,
    [PaymentDate]   DATE NULL,
    [PaymentMethod] VARCHAR(50) NULL,
    PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    FOREIGN KEY ([RentalId]) REFERENCES [dbo].[RentalTbl] ([RentId]),
    FOREIGN KEY ([CustId]) REFERENCES [dbo].[CustomerTbl] ([CustId])
);



CREATE TABLE [dbo].[BikePaymentTbl] (
    [PaymentId]     INT             IDENTITY (1, 1) NOT NULL,
    [RentalId]      INT             NULL,
    [CustId]        INT             NULL,
    [Amount]        DECIMAL (10, 2) NULL,
    [PaymentDate]   DATE            NULL,
    [PaymentMethod] VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    FOREIGN KEY ([RentalId]) REFERENCES [dbo].[BikeRentalTbl] ([RentId]),
    FOREIGN KEY ([CustId]) REFERENCES [dbo].[BikeCustomerTbl] ([CustId])
);

INSERT INTO [dbo].[UserTbl] ([Uname], [Upass])
VALUES ('Admin', '123');
