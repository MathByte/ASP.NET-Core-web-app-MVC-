insert into level ([Level_name]) VALUES ('Junior');
insert into level ([Level_name]) VALUES ('Mid-Level');
insert into level ([Level_name]) VALUES ('Senior');


Insert into Category(name) VALUES ('Fullstack');
Insert into Category(name) VALUES ('FrontEnd');
Insert into Category(name) VALUES ('BackEnd');

Insert into JobType(name) VALUES ('fulltime');
Insert into JobType(name) VALUES ('partime');
Insert into JobType(name) VALUES ('contract');

-- Insert a new job
INSERT INTO Job  ([title]
           ,[salary]
           ,[createdDate]
           ,[location]
           ,[isActive]
           ,[isOpen]
           ,[category_id]
           ,[job_type_id]
           ,[level_id]
           ,[employer_id]
           ,[description])
select 'Software Engineer', 80000.00, GETDATE(),'Location 1', 0, 0, 1, 1, 1, ee.employer_id, 'We are looking for a skilled software engineer to join our team.' from Employer ee;

INSERT INTO Job  ([title]
           ,[salary]
           ,[createdDate]
           ,[location]
           ,[isActive]
           ,[isOpen]
           ,[category_id]
           ,[job_type_id]
           ,[level_id]
           ,[employer_id]
           ,[description])
select 'JR Software Engineer', 60000.00, GETDATE(),'Location 2', 0, 0, 2, 2, 2, ee.employer_id, 'We are seeking a talented marketing manager to lead our marketing efforts.' from Employer ee;

INSERT INTO Job  ([title]
           ,[salary]
           ,[createdDate]
           ,[location]
           ,[isActive]
           ,[isOpen]
           ,[category_id]
           ,[job_type_id]
           ,[level_id]
           ,[employer_id]
           ,[description])
select 'Software Engineer', 50000.00, GETDATE(),'Location 3', 0, 0, 3, 3, 3, ee.employer_id, 'We need an energetic sales representative to grow our customer base.' from Employer ee;




INSERT INTO Job  ([title]
           ,[salary]
           ,[createdDate]
           ,[location]
           ,[isActive]
           ,[isOpen]
           ,[category_id]
           ,[job_type_id]
           ,[level_id]
           ,[employer_id]
           ,[description])
select 'AI Dev', 100000.00, GETDATE(),'Location 3', 0, 0, 3, 3, 3, ee.employer_id, 'AI discription.' from Employer ee;














