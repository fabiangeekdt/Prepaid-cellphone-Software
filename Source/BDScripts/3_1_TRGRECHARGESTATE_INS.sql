-- =================================================================================
-- Author:		Fabian Andres Moreno chacon
-- Create date: Sept 02, 2017
-- Description:	Trigger creation for update the Recharge State 
-- =================================================================================
-- ============================= CHANGES ===========================================
-- Author:		
-- Create date: 
-- Description:	
-- =================================================================================
USE [CELLPHONE_COMPANY]
GO

CREATE TRIGGER [dbo].[TRGRECHARGESTATE_INS] ON [dbo].[RECHARGE]
	FOR INSERT
	AS
		DECLARE @RechargeCode	[bigint]
		DECLARE @CustomerId		[int]
		DECLARE @PhoneNumber	[nvarchar] (50)

		SELECT @RechargeCode = i.Recharge_Code, 
		@CustomerId = i.Customer_Id,  
		@PhoneNumber = i.Phone_Number
		FROM INSERTED i;

		UPDATE [dbo].[RECHARGE] SET [Recharge_State] = 0 
		WHERE [Recharge_Code] = @RechargeCode 
		AND [Customer_Id] = @CustomerId
		AND [Phone_Number] = @PhoneNumber;
