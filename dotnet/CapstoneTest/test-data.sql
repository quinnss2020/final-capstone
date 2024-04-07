BEGIN TRANSACTION;

--create tables
CREATE TABLE users (
	id int IDENTITY(1,1) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email varchar(80) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (id)
)

CREATE TABLE temp_users (
	temp_id int IDENTITY(1,1) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email varchar(80) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	confirm_code int NOT NULL,
	CONSTRAINT PK_temp_users PRIMARY KEY (temp_id)
)

--populate default data
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role) VALUES ('User', 'Test','user@storage.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role) VALUES ('Admin', 'Test','admin@storage.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO temp_users (first_name, last_name, email, password_hash, salt, user_role, confirm_code) VALUES ('Temp', 'User', 'temp@storage.com', '14KFNJ+kvxguyeFJjg6O/Gzqs3w=', 'xnV8JjKT95w=', 'user', 123456);
INSERT INTO temp_users (first_name, last_name, email, password_hash, salt, user_role, confirm_code) VALUES ('Temp', 'Admin', 'test@storage.com', 'tjFdz9WVDM0UUY9+2cTmbl6CLL4=', 'wv5YsMG9HtU=', 'admin', 567890);

COMMIT;
