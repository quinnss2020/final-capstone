BEGIN TRANSACTION;

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
	details varchar(max) NOT NULL,
	CONSTRAINT PK_units PRIMARY KEY (id)
)

CREATE TABLE bids (
	id int IDENTITY(2000, 1) NOT NULL,
	unit_id int NOT NULL,
	bidder_id int NOT NULL DEFAULT 1,
	amount int NOT NULL,
	date_placed datetime NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_bids PRIMARY KEY (id)
)

--USER COUNT: 5
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

-- UNIT COUNT: 4
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (202, 85, 85, 4, 'II0008', 'Columbus', '5x5', 1, '2024-04-18 18:08:10', '2024-04-13 12:00:00', 'Kitchen odds and ends: Scratched cookware, outdated appliances, and half-used ingredients, a hodgepodge of culinary essentials perfect for those who appreciate the charm of well-loved kitchenware and budget-friendly cooking.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (358, 130, 130, 5, 'JJ0009', 'Cleveland', '10x10', 1, '2024-04-22 12:08:10', GETDATE(), 'Vintage toy collection: Play-worn toys, missing pieces, and well-loved games from childhoods long past, offering a trip down memory lane for those who cherish the simple joys of playtime and imagination.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (273, 220, 220, 1, 'KK0010', 'Cincinnati', '10x15', 1, '2024-04-13 16:38:10', GETDATE(), 'Artistic mishmash gallery: Paintings with faded colors, sculptures with chipped edges, and eclectic creations with stories to tell, offering a glimpse into the creative minds and artistic endeavors of the past.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (166, 20, 20, 2, 'LL0011', 'Akron', '10x20', 1, '2024-04-13 18:08:10', GETDATE(), 'Fitness gear grab bag: Tattered yoga mats, worn-out dumbbells, and cracked exercise machines, a testament to the dedication and sweat of past fitness enthusiasts, waiting to be revitalized by new energy and determination.')


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

COMMIT;
