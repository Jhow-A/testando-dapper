create table Tarefas(
	Id int primary key,
	Descricao varchar(255),
	IsCompleta bit
);

insert into Tarefas 
values 
(1, 'Aprender Dapper', 0),
(2, 'Reservar hotel para viagem', 1)

select * from Tarefas;