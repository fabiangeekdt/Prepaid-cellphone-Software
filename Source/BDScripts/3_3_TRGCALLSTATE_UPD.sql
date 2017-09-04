-- =================================================================================
-- Author:		Fabian Andres Moreno chacon
-- Create date: 
-- Description:	
-- =================================================================================
-- ============================= CHANGES ===========================================
-- Author:		
-- Create date: 
-- Description:	
-- =================================================================================
USE [CELLPHONE_COMPANY]
GO

CREATE TRIGGER [dbo].[TRGCALLSTATE_UPD] ON [dbo].[CALL]
	FOR UPDATE
	AS
		DECLARE @PhoneCallID	[bigint]
		DECLARE @CustomerId		[int]
		DECLARE @PhoneNumber	[nvarchar] (50)

		SELECT @PhoneCallID = i.PhoneCall_Id, 
		@CustomerId = i.Customer_Id,  
		@PhoneNumber = i.Phone_Number
		FROM INSERTED i;

		UPDATE [dbo].[CALL] SET [Call_State] = 4 
		WHERE [PhoneCall_Id] = @PhoneCallID 
		AND [Customer_Id] = @CustomerId
		AND [Phone_Number] = @PhoneNumber;
