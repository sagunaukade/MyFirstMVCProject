create database Assignment

create table UserTable(
UserId int primary key IDENTITY,
Email varchar(50),
FirstName varchar(50),
LastName varchar(50),
DOB date,
Gender varchar(50),
Education varchar(50),
Address varchar(50),
Password varchar(50),
ConfirmPassword varchar(50));

drop table usertable
select * from UserTable


---stored proc
Create procedure UserRegister(
@Email varchar(50),
@FirstName varchar(50),
@LastName varchar(50),
@DOB date,
@Gender varchar(50),
@Education varchar(50),
@Address varchar(50),
@Password varchar(255),
@ConfirmPassword varchar(50))
As
Begin
insert into UserTable(Email,FirstName,LastName,DOB,Gender,Education,Address,Password,ConfirmPassword) values(@Email,@FirstName,@LastName,@DOB,@Gender,@Education,@Address,@Password,@ConfirmPassword);
end

--for login
create procedure UserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from UserTable
where Email = @Email and Password = @Password
End;

--todolist
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


create procedure AddItem
(
@Date date ,
@Title varchar(50),
@Description varchar(50),
@PriorityId int
)
as
BEGIN
If Exists (select * from TODOLISTTYPE where PriorityId = @PriorityId)
begin
Insert into TODOLISTTABLE 
values(@Date, @Title, @Description, @PriorityId);
end
Else
begin
select 2
end
End;

create procedure GetList
as
BEGIN
	select * from TODOLISTTABLE;
End;

--procedure to updatebook
alter procedure UpdateTask
(
@Date date ,
@Title varchar(50),
@Description varchar(50),
@PriorityId int
)
as
BEGIN
Update ToDoListTable set Date = @Date, 
Title = @Title,
Description= @Description

where PriorityId = @PriorityId;
End;

---Procedure to deleteTask
create procedure DeleteTask
(
@ToDoListId int
)
as
BEGIN
Delete ToDoListTable 
where ToDoListId = @ToDoListId;
End;