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

CREATE OR ALTER PROCEDURE SelectTarefaById @Id int
AS
BEGIN
SELECT * FROM Tarefas WHERE Id = @Id
END
GO

EXEC SelectTarefaById 2;

CREATE OR ALTER PROCEDURE SelectTarefasConcluidas
AS
BEGIN
SELECT * FROM Tarefas WHERE IsCompleta = 1
END
GO

EXEC SelectTarefaConcluidas