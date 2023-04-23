USE [JellyFishDB]
GO


INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           )
     VALUES
           ('1','Administrator','ADMINISTRATOR')
         
GO

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           )
     VALUES
           ('2','JobSeeker','JOBSEEKER')
         
GO
INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           )
     VALUES
           ('3','Employer','EMPLOYER')
         
GO




INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[profileImage])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f1',
             'e@e.com',
           null,
           null,
           null,
           'E@E.COM',
           'e@e.com',
          'E@E.COM',
           1,
           'AQAAAAIAAYagAAAAEKxXRawSxYYuaT8sqv7W5QJw+UEY60cJ3faWTmmOG81LC7f1SirDkQp3ATxkrH7yRg==',
		   'O2VWIKZF7XBXYLJ4DZ3DLNM6VOKVQKGT',
          'bc61d48d-4f02-4d9e-a69f-5fa10ee07ac8',
           null,
           0,
           0,
            null,
           1,
           0,
            null)
GO


INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f1',
           3)
GO


























INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[profileImage])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f2',
             'a1@a1.com',
           null,
           null,
           null,
           'a1@a1.COM',
           'a1@a1.com',
          'A1@A1.COM',
           1,
           'AQAAAAIAAYagAAAAEKxXRawSxYYuaT8sqv7W5QJw+UEY60cJ3faWTmmOG81LC7f1SirDkQp3ATxkrH7yRg==',
		   'O2VWIKZF7XBXYLJ4DZ3DLNM6VOKVQKGT',
          'bc61d48d-4f02-4d9e-a69f-5fa10ee07ac8',
           null,
           0,
           0,
            null,
           1,
           0,
            null)
GO


INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f2',
           2)
GO





INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[profileImage])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f3',
             'a2@a2.com',
           null,
           null,
           null,
           'a2@a2.com',
           'a2@a2.com',
          'A2@A2.com',
           1,
           'AQAAAAIAAYagAAAAEKxXRawSxYYuaT8sqv7W5QJw+UEY60cJ3faWTmmOG81LC7f1SirDkQp3ATxkrH7yRg==',
		   'O2VWIKZF7XBXYLJ4DZ3DLNM6VOKVQKGT',
          'bc61d48d-4f02-4d9e-a69f-5fa10ee07ac8',
           null,
           0,
           0,
            null,
           1,
           0,
            null)
GO


INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f3',
           2)
GO




INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[profileImage])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f4',
             'a3@a3.com',
           null,
           null,
           null,
           'a3@a3.com',
           'a3@a3.com',
          'A3@A3.com',
           1,
           'AQAAAAIAAYagAAAAEKxXRawSxYYuaT8sqv7W5QJw+UEY60cJ3faWTmmOG81LC7f1SirDkQp3ATxkrH7yRg==',
		   'O2VWIKZF7XBXYLJ4DZ3DLNM6VOKVQKGT',
          'bc61d48d-4f02-4d9e-a69f-5fa10ee07ac8',
           null,
           0,
           0,
            null,
           1,
           0,
            null)
GO


INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ('a76b2d8b-93e5-4f5e-9c4f-ae2bca2a08f4',
           2)
GO



