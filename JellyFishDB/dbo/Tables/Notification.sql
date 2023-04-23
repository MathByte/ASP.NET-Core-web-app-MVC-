CREATE TABLE [dbo].[Notification]
(
	[NotiId] INT NOT NULL PRIMARY KEY, 
    [FromUserId] NVARCHAR(450) NULL, 
    [ToUserId] NVARCHAR(450) NULL, 
    [NotiHeader] VARCHAR(MAX) NULL, 
    [NotiBody] VARCHAR(MAX) NULL, 
    [IsRead] BIT NULL, 
    [Url] VARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL
)
