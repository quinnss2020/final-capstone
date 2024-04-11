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

CREATE TABLE bids (
	id int IDENTITY(2000, 1) NOT NULL,
	unit_id int NOT NULL,
	bidder_id int NOT NULL DEFAULT 1,
	amount int NOT NULL,
	date_placed datetime NOT NULL,
	CONSTRAINT PK_bids PRIMARY KEY (id)
)

-- populate default data
-- password for these is "password"
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) 
VALUES ('Admin', 'Test','admin@storage.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', '567890', 1, 0);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) 
VALUES ('User', 'Test','user@storage.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', '123456', 1, 0);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) 
VALUES ('Jake', 'Norris','jaketaylornorris@gmail.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user', '234567', 1, 0);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) 
VALUES ('Quinn', 'Santucci','quinn.s.santucci@gmail.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user', '234567', 1, 0);
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed) 
VALUES ('Evil', 'Jake','eviljaketaylornorris@evil.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user', '234567', 1, 0);

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (101, 100, 100, 1, 'AA0000', 'Columbus', '5x5', 1, '2024-04-12 11:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (102, 120, 120, 2, 'BB0001', 'Cleveland', '10x10', 1, '2024-06-21 12:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (103, 234, 234, 3, 'CC0002', 'Cincinnati', '10x15', 1, '2024-05-13 11:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (104, 6000, 6000, 4, 'DD0003', 'Akron', '10x20', 1, '2024-07-17 11:08:10', GETDATE())

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (111, 40, 40, 5, 'EE0004', 'Columbus', '5x5', 1, '2024-04-12 13:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (112, 65, 65, 1, 'FF0005', 'Cleveland', '10x10', 1, '2024-06-22 12:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (113, 90, 90, 2, 'GG0006', 'Cincinnati', '10x15', 1, '2024-05-13 11:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (201, 120, 120, 3, 'HH0007', 'Akron', '10x20', 1, '2024-07-17 11:08:10', GETDATE())

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (202, 85, 85, 4, 'II0008', 'Columbus', '5x5', 1, '2024-04-12 18:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (358, 130, 130, 5, 'JJ0009', 'Cleveland', '10x10', 1, '2024-10-22 12:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (273, 220, 220, 1, 'KK0010', 'Cincinnati', '10x15', 1, '2024-11-13 11:08:10', GETDATE())
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created) 
VALUES (166, 20, 20, 2, 'LL0011', 'Akron', '10x20', 1, '2024-12-17 11:08:10', GETDATE())

INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1001, 1, 100, '2024-04-10 11:08:10')
INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1002, 3, 100, '2024-04-10 11:08:10')

ALTER TABLE units WITH CHECK ADD CONSTRAINT [FK_users_units]
FOREIGN KEY(highest_bidder) REFERENCES [users] (id)
ALTER TABLE bids WITH CHECK ADD CONSTRAINT [FK_bids_units]
FOREIGN KEY(unit_id) REFERENCES [units] (id)
ALTER TABLE bids WITH CHECK ADD CONSTRAINT [FK_bids_users]
FOREIGN KEY(bidder_id) REFERENCES [users] (id)

GO