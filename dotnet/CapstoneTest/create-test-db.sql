IF DB_ID('final_capstone_testing') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone_testing SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone_testing;
END

CREATE DATABASE final_capstone_testing;

