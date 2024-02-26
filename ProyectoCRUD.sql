/* Creación base de datos */
create database DBCRUD;

use DBCRUD;

/* Creación base de tablas */

create table Departamento(
IdDepartamento int primary key identity,
Nombre varchar(50)
);

create table Empleado(
IdEmpleado int primary key identity,
NombreCompleto varchar(50),
IdDepartamento int references Departamento (IdDepartamento),
Sueldo decimal (10,2),
FechaContrato date
);

/* Insertar Valores */

insert into Departamento(Nombre) values
('Administración'),
('Marketing'),
('Ventas'),
('Comercio');

select * from Departamento;

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Roxane',2,2500,GETDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Marcos',1,1800,GETDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Miriam',3,2600,GETDATE());

insert into Empleado
(NombreCompleto,IdDepartamento,Sueldo,FechaContrato)
values('Tomás',2,2500,GETDATE());

select * from Empleado;

/* Creación de funciones */

 create function fn_departamentos()
returns table
as 
return 
	select IdDepartamento,Nombre from Departamento;

 create function fn_empleados()
returns table
as 
return 
	select e.IdEmpleado,e.NombreCompleto,d.IdDepartamento,d.Nombre,
	e.Sueldo, convert(char(10),e.FechaContrato,103)[FechaContrato]
	from Empleado e
	inner join Departamento d on d.IdDepartamento = e.IdDepartamento;

 create function fn_empleado(@idEmpleado int)
returns table
as 
return 
	select e.IdEmpleado,e.NombreCompleto,d.IdDepartamento,d.Nombre,
	e.Sueldo, convert(char(10),e.FechaContrato,103)[FechaContrato]
	from Empleado e
	inner join Departamento d on d.IdDepartamento = e.IdDepartamento
	where e.IdEmpleado = @idEmpleado;

/*Procedimientos */

/* Crear Empleado */
create proc sp_CrearEmpleado (
@NombreCompleto varchar(50),
@IdDepartamento int,
@Sueldo decimal(10,2),
@FechaContrato varchar(10)
)
as
begin
	set dateformat dmy
	insert into Empleado(NombreCompleto,IdDepartamento,
	Sueldo,FechaContrato) values
	(@NombreCompleto, @IdDepartamento, @Sueldo,
	CONVERT(date,@FechaContrato))
end

/* Editar Empleado */
create proc sp_EditarEmpleado (
@IdEmpleado int,
@NombreCompleto varchar(50),
@IdDepartamento int,
@Sueldo decimal(10,2),
@FechaContrato varchar(10)
)
as
begin
	set dateformat dmy

	update Empleado set
	NombreCompleto = @NombreCompleto,
	IdDepartamento = @IdDepartamento,
	Sueldo = @Sueldo,
	FechaContrato = CONVERT(date,@FechaContrato)
	where IdEmpleado =@IdEmpleado

end;

/* Eliminar Empleado */
create proc sp_EliminarEmpleado (
@IdEmpleado int
)
as
begin
	delete from Empleado
	where IdEmpleado =@IdEmpleado

end



