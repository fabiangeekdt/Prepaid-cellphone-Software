-- =================================================================================
-- Author:		Fabian Andres Moreno chacon
-- Create date: Sept 02, 2017
-- Description:	Table and relations creation
-- =================================================================================
-- ============================= CHANGES ===========================================
-- Author:		
-- Create date: 
-- Description:	
-- =================================================================================

USE [CELLPHONE_COMPANY]
GO

----
CREATE TABLE [dbo].[MINIMUN_RECHARGE](
	[Id] [int] NOT NULL,
	[Period] [varchar](15) NOT NULL,
	[MinimunValue] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Minimun_Recharge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[PRICE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_PRICE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[RECHARGE_STATE](
	[State_Id] [int] NOT NULL,
	[State_Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Recharge_State] PRIMARY KEY CLUSTERED 
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[CALL_STATE](
	[State_Id] [int] NOT NULL,
	[State_Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CALL_STATE] PRIMARY KEY CLUSTERED 
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[PROMOTION_VALUETYPE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](5) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_PROMOTION_VALUETYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[PROMOTION](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Value] [int] NOT NULL,
	[Value_Type] [int] NOT NULL,
 CONSTRAINT [PK_PROMOTION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PROMOTION]  WITH CHECK ADD  CONSTRAINT [FK_PROMOTION_PROMOTION_VALUETYPE] FOREIGN KEY([Value_Type])
REFERENCES [dbo].[PROMOTION_VALUETYPE] ([Id])
GO

ALTER TABLE [dbo].[PROMOTION] CHECK CONSTRAINT [FK_PROMOTION_PROMOTION_VALUETYPE]
GO

----
CREATE TABLE [dbo].[CUSTOMER](
	[Id_Type] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Second_Name] [nvarchar](50) NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Phone_Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----
CREATE TABLE [dbo].[CUSTOMER_BONUS](
	[Bonus_Code] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
	[Promotion_Id] [int] NOT NULL,
	[Activation_day] [datetime] NOT NULL,
 CONSTRAINT [PK_CUSTOMER_BONUS] PRIMARY KEY CLUSTERED 
(
	[Bonus_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CUSTOMER_BONUS]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMER_BONUS_CUSTOMER] FOREIGN KEY([Customer_Id], [Phone_Number])
REFERENCES [dbo].[CUSTOMER] ([Id], [Phone_Number])
GO

ALTER TABLE [dbo].[CUSTOMER_BONUS] CHECK CONSTRAINT [FK_CUSTOMER_BONUS_CUSTOMER]
GO

ALTER TABLE [dbo].[CUSTOMER_BONUS]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMER_BONUS_PROMOTION] FOREIGN KEY([Promotion_Id])
REFERENCES [dbo].[PROMOTION] ([Id])
GO

ALTER TABLE [dbo].[CUSTOMER_BONUS] CHECK CONSTRAINT [FK_CUSTOMER_BONUS_PROMOTION]
GO

----
CREATE TABLE [dbo].[CUSTOMER_PHONE](
	[Internal_Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
	[Phone_state] [tinyint] NOT NULL,
	[Minute_Balance] [decimal](10, 2) NOT NULL,
	[Minutes_Use] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_CUSTOMER_PHONE] PRIMARY KEY CLUSTERED 
(
	[Internal_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CUSTOMER_PHONE]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMERPHONE] FOREIGN KEY([Customer_Id], [Phone_Number])
REFERENCES [dbo].[CUSTOMER] ([Id], [Phone_Number])
GO

ALTER TABLE [dbo].[CUSTOMER_PHONE] CHECK CONSTRAINT [FK_CUSTOMERPHONE]
GO

----
CREATE TABLE [dbo].[RECHARGE](
	[Recharge_Code] [bigint] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
	[Recharge_value] [decimal](7, 0) NOT NULL,
	[Recharge_Date] [datetime] NOT NULL,
	[Recharge_State] [int] NOT NULL,
 CONSTRAINT [PK_RECHARGE] PRIMARY KEY CLUSTERED 
(
	[Recharge_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RECHARGE]  WITH CHECK ADD  CONSTRAINT [FK_RECHARGE_CUSTOMER] FOREIGN KEY([Customer_Id], [Phone_Number])
REFERENCES [dbo].[CUSTOMER] ([Id], [Phone_Number])
GO

ALTER TABLE [dbo].[RECHARGE] CHECK CONSTRAINT [FK_RECHARGE_CUSTOMER]
GO

ALTER TABLE [dbo].[RECHARGE]  WITH CHECK ADD  CONSTRAINT [FK_RECHARGE_RECHARGE_STATE] FOREIGN KEY([Recharge_State])
REFERENCES [dbo].[RECHARGE_STATE] ([State_Id])
GO

ALTER TABLE [dbo].[RECHARGE] CHECK CONSTRAINT [FK_RECHARGE_RECHARGE_STATE]
GO

----

CREATE TABLE [dbo].[CALL](
	[PhoneCall_Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_id] [int] NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
	[Destination_Number] [nvarchar](50) NOT NULL,
	[Initial_DateTime] [datetime] NOT NULL,
	[Final_DateTime] [datetime] NOT NULL,
	[Call_Duration] [decimal](5, 2) NOT NULL,
	[Call_Cost] [decimal](10, 2) NOT NULL,
	[Call_State] [int] NOT NULL,
 CONSTRAINT [PK_CALL_HISTORY] PRIMARY KEY CLUSTERED 
(
	[PhoneCall_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CALL]  WITH CHECK ADD  CONSTRAINT [FK_CALL_HISTORY_CALL_STATE] FOREIGN KEY([Call_State])
REFERENCES [dbo].[CALL_STATE] ([State_Id])
GO

ALTER TABLE [dbo].[CALL] CHECK CONSTRAINT [FK_CALL_HISTORY_CALL_STATE]
GO

ALTER TABLE [dbo].[CALL]  WITH CHECK ADD  CONSTRAINT [FK_CALL_HISTORY_CUSTOMER] FOREIGN KEY([Customer_id], [Phone_Number])
REFERENCES [dbo].[CUSTOMER] ([Id], [Phone_Number])
GO

ALTER TABLE [dbo].[CALL] CHECK CONSTRAINT [FK_CALL_HISTORY_CUSTOMER]
GO