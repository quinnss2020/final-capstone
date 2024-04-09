USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	id int IDENTITY(1,1) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email varchar(80) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	code varchar(6) NOT NULL,
	confirmed bit NOT NULL DEFAULT 0,
	CONSTRAINT PK_user PRIMARY KEY (id)
)

-- populate default data
-- password for these is "password"
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed) VALUES ('User', 'Test','user@storage.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', '123456', 1);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed) VALUES ('Admin', 'Test','admin@storage.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', '567890', 1);
--INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed) VALUES ('Jake', 'Norris','jake@jake.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user', 234567, 0);


GO