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
	agreed bit NOT NULL DEFAULT 0,
	CONSTRAINT PK_user PRIMARY KEY (id)
)

CREATE TABLE units (
	id int IDENTITY(1000, 1) NOT NULL,
	local_id int NOT NULL,
	start_bid int NOT NULL,
	highest_bid int NOT NULL DEFAULT 0, 
	highest_bidder int NOT NULL DEFAULT 1,
	order_number varchar(6) NOT NULL, 
	city varchar(255) NOT NULL, 
	size varchar(5) NOT NULL, 
	active bit NOT NULL, 
	expiration smalldatetime NOT NULL, 
	created datetime NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_units PRIMARY KEY (id)
)

-- populate default data
-- password for these is "password"
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) VALUES ('Admin', 'Test','admin@storage.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', '567890', 1, 0);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) VALUES ('User', 'Test','user@storage.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', '123456', 1, 0);
--INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed) VALUES ('Jake', 'Norris','jake@jake.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user', 234567, 0);

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) VALUES (101, 100, 100, 1, 'AA0000', 'Columbus', '5x5', 1, '2024-05-10 11:08:10', GETDATE())

ALTER TABLE units WITH CHECK ADD CONSTRAINT [FK_users_units]
FOREIGN KEY(highest_bidder) REFERENCES [users] (id)

GO