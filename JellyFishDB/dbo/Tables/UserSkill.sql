CREATE TABLE UserSkill (user_skill_id int IDENTITY NOT NULL, skill_id int NOT NULL, user_id nvarchar(450) NOT NULL, PRIMARY KEY (user_skill_id));
GO
ALTER TABLE UserSkill ADD CONSTRAINT FKUserSkill942971 FOREIGN KEY (skill_id) REFERENCES Skill (skill_id);
GO
ALTER TABLE UserSkill ADD CONSTRAINT FKUserSkill459199 FOREIGN KEY (user_id) REFERENCES dbo.AspNetUsers (Id);