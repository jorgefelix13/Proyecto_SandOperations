create database SandOperations
go

use SandOperations
go

create table usuarios(
usu_id int identity primary key,
usu_usuario varchar(50) not null unique, --unique evita que se creen dos usuarios iguales por error
usu_password varchar(255) not null,
usu_rol varchar(50) not null,
usu_activo bit default 1
)
go

select * from usuario

insert into usuarios(usu_usuario,usu_password,usu_rol) values('2602@gmail.com','admin123','administrador')
go

create table productos(
pro_id int identity primary key,
pro_codigo varchar(50) not null unique, --varchar para el codigo de barras
pro_nombre varchar(150) not null,
pro_pCompra decimal(10,2) not null,
pro_stock int not null default 0, --default 0 para que inicie en 0 el inventario
pro_descripcion varchar(250) null
)
go
select * from productos
insert into productos(pro_codigo, pro_nombre, pro_pCompra, pro_stock, pro_descripcion)
values('0001', 'pala', 150.50, 50, 'pala para tierra blanda')

update productos set pro_codigo = '0004', pro_nombre = 'arena', pro_pCompra = 150, pro_stock = 50, pro_descripcion = 'arena blanca' where pro_id = 4

create table entradas(
ent_id int identity primary key,
ent_proId int foreign key references productos(pro_id),
ent_cantidad int not null,
ent_precioUnitario decimal(10,2) not null,
ent_total decimal(10,2) not null,
ent_fecha datetime default getdate(),
ent_destino varchar(50)
)
go

create table salidas(
sal_id int identity primary key,
sal_proId int foreign key references productos(pro_id),
sal_cantidad int not null,
sal_fecha datetime default getdate(),
sal_destino varchar(50) null,
sal_usuario varchar(50) not null
)