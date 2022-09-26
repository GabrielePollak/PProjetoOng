CREATE DATABASE Ong

USE Ong;

CREATE TABLE Pet(
NomePet varchar(50),
EspeciePet varchar(50) not null,
RacaPet varchar(20) not null,
SexoPet char(1),
Disponivel char(1),
NChip int identity not null,

constraint pk_pet primary key(NChip)
);

create table Adotante(
nome varchar(50) not null,
cpf varchar(11) not null,
sexo char(1) not null,
telefone varchar(11) not null,
endereco varchar(100),
ativa char(1) not null,
dataNascimento date not null

constraint pk_pessoa primary key(cpf)
);

create table Endereco(
cpf varchar(11) not null,
logradouro varchar(50) not null,
numero int not null,
bairro varchar(20) not null,
complemento varchar(20) not null,
cep varchar(8) not null,
cidade varchar(20) not null,
uf varchar(3) not null

foreign key (cpf) references Adotante(cpf)
);

create table regAdocao(
cpf varchar(11) not null,
nChipPet int not null,

foreign key (cpf) references Adotante(cpf),
foreign key (nChipPet) references pet(nChipPet),
constraint pk_regAdocao primary key(cpf,nChipPet)
);

delete from regAdocao where nChipPet = 1



