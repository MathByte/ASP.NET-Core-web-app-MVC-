CREATE TABLE Address (
address_id nvarchar(450) NOT NULL, 
street nvarchar(255) NULL,
city nvarchar(255) NULL, 
postal_code nvarchar(255) NULL, 
province nvarchar(255) NULL, 
PRIMARY KEY (address_id));
GO
ALTER TABLE Address ADD CONSTRAINT FKAddress726313 FOREIGN KEY (address_id) REFERENCES dbo.AspNetUsers (Id);