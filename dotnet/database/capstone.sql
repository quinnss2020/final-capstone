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
	details varchar(max) NOT NULL,
	email_sent bit NOT NULL DEFAULT 0,
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

CREATE TABLE unit_photos (
	id int IDENTITY(1,1) NOT NULL, 
	unit_id int NOT NULL,
	img_URL varchar(max) NOT NULL,
	CONSTRAINT PK_unit_photos PRIMARY KEY (id)
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

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (101, 100, 100, 1, 'AA0000', 'Columbus', '5x5', 1, '2024-04-16 10:30:10', GETDATE(), 'Well-loved furniture assortment: A mishmash of worn sofas, chipped dining sets, and mismatched lamps, perfect for DIY enthusiasts looking to refurbish or repurpose these vintage pieces for a touch of character in any home.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (102, 120, 120, 1, 'BB0001', 'Cleveland', '10x10', 1, '2024-04-19 12:08:10', GETDATE(), 'Sports memorabilia mishmash: Faded jerseys, dusty trophies, and scuffed photographs from forgotten games, waiting for a new owner to appreciate the nostalgia and history behind each piece, despite their worn condition.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (103, 180, 180, 1, 'CC0002', 'Cincinnati', '10x15', 1, '2024-04-16 14:08:10', GETDATE(), 'Outdoor adventure odds and ends: From frayed hiking boots to weathered tents, this collection offers a glimpse into past expeditions, with gear that''s seen its fair share of rugged terrain and outdoor escapades.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (104, 60, 60, 1, 'DD0003', 'Akron', '10x20', 1, '2024-04-19 11:08:10', GETDATE(), 'Vintage book grab bag: Dog-eared classics, well-thumbed paperbacks, and musty volumes, each telling its own story of past readers and literary journeys, waiting to be rediscovered and cherished once again.')

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (111, 40, 40, 1, 'EE0004', 'Columbus', '5x5', 1, '2024-04-19 13:08:10', GETDATE(), 'Electronics graveyard: Outdated gadgets, cracked screens, and dusty devices from years gone by, a testament to the ever-changing landscape of technology and the memories held within each worn-out item.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (112, 65, 65, 1, 'FF0005', 'Cleveland', '10x10', 1, '2024-04-22 12:08:10', GETDATE(), ' Musical instrument medley: Instruments with missing strings, chipped finishes, and signs of wear, yet still holding the promise of melodic harmony and creative expression for those willing to give them a second chance.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (113, 90, 90, 1, 'GG0006', 'Cincinnati', '10x15', 1, '2024-04-17 15:08:10', GETDATE(), 'Fashion mishmash closet: Pre-loved garments, hand-me-downs, and thrifted treasures, offering a budget-friendly way to indulge in style and self-expression while giving new life to once-loved pieces.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (201, 120, 120, 3, 'HH0007', 'Akron', '10x20', 1, '2024-04-16 12:48:10', GETDATE(), 'Well-worn tool assortment: Rusty saws, dented hammers, and well-loved wrenches, a testament to the hard work and DIY projects of the past, ready to be reinvigorated by a new owner with a knack for restoration.')

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (202, 85, 85, 1, 'II0008', 'Columbus', '5x5', 1, '2024-04-18 18:08:10', GETDATE(), 'Kitchen odds and ends: Scratched cookware, outdated appliances, and half-used ingredients, a hodgepodge of culinary essentials perfect for those who appreciate the charm of well-loved kitchenware and budget-friendly cooking.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (358, 130, 130, 1, 'JJ0009', 'Cleveland', '10x10', 1, '2024-04-22 13:08:10', GETDATE(), 'Vintage toy collection: Play-worn toys, missing pieces, and well-loved games from childhoods long past, offering a trip down memory lane for those who cherish the simple joys of playtime and imagination.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (273, 220, 220, 1, 'KK0010', 'Cincinnati', '10x15', 1, '2024-04-18 16:38:10', GETDATE(), 'Artistic mishmash gallery: Paintings with faded colors, sculptures with chipped edges, and eclectic creations with stories to tell, offering a glimpse into the creative minds and artistic endeavors of the past.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (166, 20, 20, 1, 'LL0011', 'Akron', '10x20', 1, '2024-04-17 18:08:10', GETDATE(), 'Fitness gear grab bag: Tattered yoga mats, worn-out dumbbells, and cracked exercise machines, a testament to the dedication and sweat of past fitness enthusiasts, waiting to be revitalized by new energy and determination.')

INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (316, 30, 30, 1, 'BB0013', 'Cleveland', '10x10', 1, '2024-04-19 10:38:10', GETDATE(), 'Home office odds and ends: Scratched desks, wobbly chairs, and cluttered organizers, a mix of well-used essentials perfect for creating a functional workspace on a budget, with a touch of DIY charm.')
INSERT INTO units(local_id, start_bid, highest_bid, highest_bidder, order_number, city, size, active, expiration, created, details) 
VALUES (224, 50, 50, 1, 'CC0011', 'Columbus', '5x5', 1, '2024-04-17 10:08:10', GETDATE(), 'Vinyl record mishmash: Scratchy LPs, dusty covers, and worn-out sleeves, each holding a piece of musical history and the memories of past listeners, waiting to spin their tales once again.')

INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1001, 2, 110, '2024-04-10 11:08:10')
INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1002, 3, 130, '2024-04-10 11:08:10')
INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1001, 4, 120, '2024-04-10 11:08:10')
INSERT INTO bids(unit_id, bidder_id, amount, date_placed) 
VALUES (1002, 4, 140, '2024-04-10 11:08:10')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1000, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367334/360_F_420314895_Eu9FSsYr8rGdlKlsIo62WrewBfoSlBOp_qilr7c.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1000, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280373/y2gvkr4bvbkssugtm2dz.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1000, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278872/kw1jahxxyoxp7ouo30ih.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1000, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278906/dpgu1cgpvfsta6yv19dc.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1001, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367306/pxl_20220811_183329929_fed0md.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1001, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279174/ktigs8xebh3jlzpu5rc4.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1001, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279214/m1ro4ztnzvjwbcxddyzf.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1001, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279232/hzds5msfxtuqnvsajihd.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1002, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367265/SmallStorageUnit-3-e1469712645525_nfshi7.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1002, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279861/a8ziytgndhfla6s5rpzl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1002, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280005/efyhhlrzfajvrygzxttl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1002, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280020/vjsxavcfto5ppeyxxg9m.jpg')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1003, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367243/5_uxmc2z.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1003, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280345/zcax5raretqskhayvloy.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1003, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278872/kw1jahxxyoxp7ouo30ih.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1003, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278906/dpgu1cgpvfsta6yv19dc.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1004, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367227/packunit2-700x525_omhslx.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1004, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279129/vgtdzld2g2qiwpyh0pcg.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1004, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279214/m1ro4ztnzvjwbcxddyzf.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1004, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279232/hzds5msfxtuqnvsajihd.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1005, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367183/neat-organized-garage_gh3hff.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1005, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279901/tevceiczexl8o9tt1doe.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1005, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280005/efyhhlrzfajvrygzxttl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1005, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280020/vjsxavcfto5ppeyxxg9m.jpg')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1006, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278872/kw1jahxxyoxp7ouo30ih.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1006, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367183/neat-organized-garage_gh3hff.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1006, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280373/y2gvkr4bvbkssugtm2dz.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1006, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278906/dpgu1cgpvfsta6yv19dc.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1007, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279214/m1ro4ztnzvjwbcxddyzf.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1007, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279129/vgtdzld2g2qiwpyh0pcg.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1007, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279174/ktigs8xebh3jlzpu5rc4.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1007, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279232/hzds5msfxtuqnvsajihd.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1008, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280005/efyhhlrzfajvrygzxttl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1008, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279901/tevceiczexl8o9tt1doe.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1008, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279861/a8ziytgndhfla6s5rpzl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1008, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280020/vjsxavcfto5ppeyxxg9m.jpg')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1009, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278906/dpgu1cgpvfsta6yv19dc.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1009, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713367265/SmallStorageUnit-3-e1469712645525_nfshi7.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1009, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280373/y2gvkr4bvbkssugtm2dz.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1009, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278872/kw1jahxxyoxp7ouo30ih.jpg')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1010, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279232/hzds5msfxtuqnvsajihd.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1010, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279129/vgtdzld2g2qiwpyh0pcg.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1010, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279174/ktigs8xebh3jlzpu5rc4.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1010, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279214/m1ro4ztnzvjwbcxddyzf.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1011, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280020/vjsxavcfto5ppeyxxg9m.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1011, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279901/tevceiczexl8o9tt1doe.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1011, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279861/a8ziytgndhfla6s5rpzl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1011, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280005/efyhhlrzfajvrygzxttl.jpg')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1012, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279174/ktigs8xebh3jlzpu5rc4.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1012, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280373/y2gvkr4bvbkssugtm2dz.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1012, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278872/kw1jahxxyoxp7ouo30ih.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1012, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713278906/dpgu1cgpvfsta6yv19dc.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1013, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279129/vgtdzld2g2qiwpyh0pcg.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1013, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279174/ktigs8xebh3jlzpu5rc4.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1013, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279214/m1ro4ztnzvjwbcxddyzf.webp')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1013, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279232/hzds5msfxtuqnvsajihd.webp')

INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1014, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279901/tevceiczexl8o9tt1doe.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1014, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713279861/a8ziytgndhfla6s5rpzl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1014, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280005/efyhhlrzfajvrygzxttl.jpg')
INSERT INTO unit_photos(unit_id, img_URL)
VALUES(1014, 'https://res.cloudinary.com/dpicdmebf/image/upload/v1713280020/vjsxavcfto5ppeyxxg9m.jpg')



ALTER TABLE units WITH CHECK ADD CONSTRAINT [FK_users_units]
FOREIGN KEY(highest_bidder) REFERENCES [users] (id)
ALTER TABLE bids WITH CHECK ADD CONSTRAINT [FK_bids_units]
FOREIGN KEY(unit_id) REFERENCES [units] (id)
ALTER TABLE bids WITH CHECK ADD CONSTRAINT [FK_bids_users]
FOREIGN KEY(bidder_id) REFERENCES [users] (id)
ALTER TABLE unit_photos WITH CHECK ADD CONSTRAINT [FK_unit_photo]
FOREIGN KEY(unit_id) REFERENCES [units] (id)

GO