/* Creación base de datos*/
create database DBCRUD;

use DBCRUD;

create table Departamento(
IdDepartamento int AUTO_INCREMENT primary key,
Nombre varchar(50)
);

create table Empleado(
IdEmpleado int AUTO_INCREMENT primary key,
NombreCompleto varchar(50),
IdDepartamento int references Departamento (IdDepartamento),
Sueldo decimal (10,2),
FechaContrato date
);

insert into Departamento(Nombre) values
('Administración'),
('Marketing'),
('Ventas'),
('Comercio');

select * from Departamento;

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Roxane',2,2500,CURDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Marcos',1,1800,CURDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Miriam',3,2600,CURDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Tomás',2,2500,CURDATE());

select * from Empleado;



