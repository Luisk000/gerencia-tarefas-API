# Visão Geral

Esta aplicação consiste em uma tela de gerenciamento de tarefas, onde cada tarefa possui título, descrição, prioridade, status atual e data de criação. As tarefas criadas podem ser editadas ou excluídas.

# Pré-requisitos

- PostgreSQL
https://www.postgresql.org/download/

- Node.js
https://nodejs.org/pt-br

- Angular
Após instalar o Node.js, abra o prompt de comando e digite:
```npm install -g @angular/cli```

- Visual Studio e/ou Visual Studio Code (ou outra IDE semelhante)
https://visualstudio.microsoft.com/pt-br/
https://code.visualstudio.com/Download

# Instruções para execução

## 1. Criação de tabela tarefas

- Crie um banco de dados no PostgreSQL chamado homologacao:

```
CREATE DATABASE homologacao;
```

- Então selecione o banco de dados homologacao e execute este script:

```
CREATE TYPE prioridade_tipo AS ENUM ('baixa', 'media', 'alta');
CREATE TYPE status_tipo AS ENUM ('pendente', 'em_andamento', 'concluido');

CREATE TABLE tarefas(
	id SERIAL PRIMARY KEY,
	titulo VARCHAR(50) NOT NULL,
	descricao TEXT NOT NULL,
	prioridade prioridade_tipo NOT NULL,
	status status_tipo DEFAULT 'pendente' NOT NULL,
	data_criacao TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL
);
```

## 2. Execução do frontend

- Abra frontend/GerenciaTarefas.Web em uma IDE, de preferência o Visual Studio Code
- No terminal, execute ```npm install``` para instalar todas as dependências
- Então execute ```ng serve -o``` para abrir o frontend em um servidor local

## 3. Execução do backend

- Abra o projeto backend/GerenciaTarefas.API, de preferência com Visual Studio 
- No terminal, execute ```dotnet restore``` para restaurar os pacotes NuGet
- No terminal, execute ```dotnet run``` para abrir o backend em um servidor local, ou clique no botão Executar (IIS Express)


