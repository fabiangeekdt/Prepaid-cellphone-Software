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

---- [CALL_STATE] Inserts
INSERT INTO [dbo].[CALL_STATE] ([State_Id],[State_Description]) VALUES(0, 'Accepted');
INSERT INTO [dbo].[CALL_STATE] ([State_Id],[State_Description]) VALUES(1, 'Rejected');
INSERT INTO [dbo].[CALL_STATE] ([State_Id],[State_Description]) VALUES(2, 'On Hold');
INSERT INTO [dbo].[CALL_STATE] ([State_Id],[State_Description]) VALUES(3, 'UnReachable');
INSERT INTO [dbo].[CALL_STATE] ([State_Id],[State_Description]) VALUES(4, 'Ended');
---- [RECHARGE_STATE] Inserts
INSERT INTO [dbo].[RECHARGE_STATE] ([State_Id],[State_Description]) VALUES (0,'Accepted');
INSERT INTO [dbo].[RECHARGE_STATE] ([State_Id],[State_Description]) VALUES (1,'Pending');
INSERT INTO [dbo].[RECHARGE_STATE] ([State_Id],[State_Description]) VALUES (2,'Rejected');
---- [PROMOTION_VALUETYPE] Inserts
INSERT INTO [dbo].[PROMOTION_VALUETYPE] ([Type],[Description]) VALUES ('%','Percentaje bonus value.');
INSERT INTO [dbo].[PROMOTION_VALUETYPE] ([Type],[Description]) VALUES ('$','Money bonus value.');
INSERT INTO [dbo].[PROMOTION_VALUETYPE] ([Type],[Description]) VALUES ('Min','Minutes bonus value.');
---- [PROMOTION] inserts
INSERT INTO [dbo].[PROMOTION] ([Description],[Value],[Value_Type]) VALUES ('Recharged sum is greater than the average sums of all previous recharges, then a 10% bonus is awarded on the recharged sum',10,1);
INSERT INTO [dbo].[PROMOTION] ([Description],[Value],[Value_Type]) VALUES ('Bonus derived from the average of the minimun recharges during the past week',5,1);
---- [PRICE] inserts
INSERT INTO [dbo].[PRICE] ([Price],[Description]) VALUES ( 166.6,'Minute price');
INSERT INTO [dbo].[PRICE] ([Price],[Description]) VALUES ( 2.81,'Second price');
---- [MINIMUN_RECHARGE] inserts
INSERT INTO [dbo].[MINIMUN_RECHARGE] ([Id],[Period],[MinimunValue]) VALUES (1,'Daily',5000);
INSERT INTO [dbo].[MINIMUN_RECHARGE] ([Id],[Period],[MinimunValue]) VALUES (2,'Weekly',1000);
INSERT INTO [dbo].[MINIMUN_RECHARGE] ([Id],[Period],[MinimunValue]) VALUES (3,'BiMonthly',10000);
INSERT INTO [dbo].[MINIMUN_RECHARGE] ([Id],[Period],[MinimunValue]) VALUES (4,'Monthly',20000);