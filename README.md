#  ManoelAPI - Microserviço de Empacotamento

Microserviço desenvolvido em **.NET 8**, responsável por receber pedidos e calcular a melhor forma de empacotar produtos em caixas.  
Conta com **autenticação JWT**, persistência em **SQL Server** e execução via **Docker**.

##  Pré-requisitos

- [Docker](https://www.docker.com/) instalado na máquina.

##  Como executar

```bash
git clone https://github.com/carlosrodr1/ManoelAPI.git
cd manoelapi
docker compose up --build
```

Acesse o Swagger em:  
 [http://localhost:8080/swagger](http://localhost:8080/swagger)

##  Como testar

1. Registre um usuário:  
   `POST /api/registrar`

2. Faça login:  
   `POST /api/login`  
    Copie o token JWT retornado

3. Autorize no Swagger:  
   Clique em **Authorize** e cole o token no formato:  
   ```
   Bearer SEU_TOKEN
   ```

4. Teste o endpoint de empacotamento:  
   `POST /api/pedidos/embalar`

##  Banco de Dados

- Utiliza **SQL Server 2022**
- Migrations aplicadas automaticamente na inicialização da aplicação

##  Testes

Execute os testes unitários com:

```bash
dotnet test
```

> Os testes utilizam **xUnit**.

##  Desenvolvido por

**Carlos Rodrigues**  
[GitHub @carlosrodr1](https://github.com/carlosrodr1)
