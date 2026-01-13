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

insert into usuarios(usu_usuario,usu_password,usu_rol) values('2601@gmail.com','admin123','administrador')
go