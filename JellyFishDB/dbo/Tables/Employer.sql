CREATE TABLE Employer 
(employer_id nvarchar(450) NOT NULL, 
title nvarchar(255) NOT NULL,
company_id int NOT NULL,
PRIMARY KEY (employer_id),
CONSTRAINT FKEmployer37240 FOREIGN KEY (employer_id) REFERENCES dbo.AspNetUsers (Id),
CONSTRAINT fk_company FOREIGN KEY (company_id) REFERENCES Company (company_id),
);


