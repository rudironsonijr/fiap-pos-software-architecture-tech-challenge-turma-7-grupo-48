# Lanchonete API (Turma 7 - Grupo 48)

Bem-vindo à API da Lanchonete! A API foi desenvolvida em .NET Core 8, utiliza Docker Compose para containerização e SQL Server como banco de dados. A documentação da API está disponível no Swagger.

## Tecnologias Utilizadas

- .NET Core 8
- Docker
- SQL Server
- Swagger

## Funcionalidades

- Criar, editar e Produtos
- Buscar Produtos por Categoria
- Cadastro do Cliente
- Identificação do Cliente via CPF
- Fake checkout, apenas enviar os produtos escolhidos para a fila. O checkout é a finalização do pedido.
- Listar Pedidos.

## Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:

- [Docker](https://www.docker.com/get-started)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [SQL Server](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

## Como Rodar a Aplicação

### 1. Clone este repositório

```bash
git clone https://github.com/rudironsonijr/fiap-pos-software-architecture-tech-challenge-turma-7-grupo-48.git
cd lanchonete-api
```
## Documentação do Event Storm e Swagger
- [Link para acesso do Event Storm](https://miro.com/welcomeonboard/VDJKZ2pRT0wzZWYwRDhKdWZjSGc0emxZOVVDMmFSTHg4VERsVTA3S2pRZTdZSlJ5ZVVjMXFlOGpvZVJtNGZTVnwzNDU4NzY0NTg1NTg2OTYzNjIxfDI=?share_link_id=452022055535)
- Após subir os containers, a API estará disponível em http://localhost:5000. Você pode acessar a documentação do Swagger em http://localhost:5000/swagger.
