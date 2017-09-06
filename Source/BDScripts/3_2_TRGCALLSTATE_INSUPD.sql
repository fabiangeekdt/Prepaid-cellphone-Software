-- =================================================================================
-- Author:		Fabian Andres Moreno chacon
-- Create date: Sept 02, 2017
-- Description:	Trigger creation for update the call's State when the phone call 
--				end with 0 and 2 States: accepted and on hold.
-- =================================================================================
-- ============================= CHANGES ===========================================
-- Author:		
-- Create date: 
-- Description:	
-- =================================================================================
USE [CELLPHONE_COMPANY]
GO

CREATE TRIGGER [dbo].[TRGCALLSTATE_INSUPD] ON [dbo].[CALL]
	FOR INSERT, UPDATE
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
		AND [Phone_Number] = @PhoneNumber
		AND [Call_State] IN (0,2);
