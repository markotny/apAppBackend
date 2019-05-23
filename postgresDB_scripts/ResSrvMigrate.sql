CREATE DATABASE "TrueHomeDB";

\c "TrueHomeDB"

CREATE TABLE "role" (
	ID_Role       SERIAL,
	RoleName      varchar (30) NOT NULL,
	CONSTRAINT pk_role PRIMARY KEY (ID_Role)
);

CREATE TABLE "user" (
	ID_User       text NOT NULL,
	Login         varchar (100) NOT NULL,
	Email         varchar (100) NOT NULL,
	Rate 		  numeric (2,1),
	isBlocked	  boolean NOT NULL,
	IDRole        INTEGER,
	CONSTRAINT pk_user PRIMARY KEY(ID_User),
	CONSTRAINT fk_role FOREIGN KEY(IDRole) REFERENCES "role"(ID_Role) ON DELETE RESTRICT
);

CREATE TABLE "apartment" (
	ID_Ap         	SERIAL,
	Name           	varchar (100) NOT NULL,
	City            varchar (100) NOT NULL,
	Street          varchar (100) NOT NULL,
	ApartmentNumber varchar (20)  NOT NULL,
	ImgThumb        varchar (200),
	ImgList         varchar (200)[],
	Rate            numeric (2,1),
	Lat             numeric (9,7) NOT NULL,
	Long            numeric (10,7)NOT NULL,
	Description 	text,
	IDUser          text NOT NULL,
	CONSTRAINT pk_apartment PRIMARY KEY(ID_Ap),
	CONSTRAINT fk_user FOREIGN KEY(IDUser) REFERENCES "user"(ID_User) ON DELETE RESTRICT
);

CREATE TABLE "personaldata" (
	ID_PData	SERIAL,
	FirstName	varchar(50) NOT NULL,
	LastName	varchar(100) NOT NULL,
	BirthDate	Date,
	IDUser		text NOT NULL,
	CONSTRAINT pk_personaldata PRIMARY KEY(ID_PData),
	CONSTRAINT fk_user FOREIGN KEY(IDUser) REFERENCES "user"(ID_User) ON DELETE RESTRICT
);

CREATE TABLE "dump" (
	ID_PData	SERIAL
);