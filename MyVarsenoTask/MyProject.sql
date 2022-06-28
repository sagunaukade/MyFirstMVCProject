create database MyProject

create table UserTable(
UserId int primary key IDENTITY,
Email varchar(50),
FirstName varchar(50),
LastName varchar(50),
DOB date,
Gender char(1),
Education varchar(50),
Address varchar(50),
Password varchar(50),
ConfirmPassword varchar(50));


select * from UserTable


create Table TODOLISTTYPE
(
	PriorityId INT IDENTITY(1,1) PRIMARY KEY,
	PriorityName varchar(50)
);

insert into TODOLISTTYPE values('Low'),('High'),('Medium');

create Table TODOLISTTABLE
(
ToDoListId INT IDENTITY(1,1) PRIMARY KEY,
Date date, 
Title varchar(50),
Description varchar(50),
PriorityId int 
FOREIGN KEY (PriorityId) REFERENCES TODOLISTTYPE(PriorityId),
);

select * from TODOLISTTABLE