# ManoelAPI - Microserviço de Empacotamento

Microserviço desenvolvido em .NET 8, responsável por receber pedidos e calcular a melhor forma de empacotar produtos em caixas. Possui autenticação via JWT, persistência em SQL Server e execução com Docker.

Pré-requisitos: é necessário ter o Docker instalado

Como executar: clone o repositório com `git clone https://github.com/carlosrodr1/ManoelAPI.git` e acesse a pasta com `cd manoelapi`. Depois, suba os containers com `docker compose up --build`. A aplicação estará disponível em `http://localhost:8080/swagger`.

Como testar: registre um usuário usando o endpoint `POST /api/registrar`, depois faça login em `POST /api/login` e copie o token JWT retornado. 

No Swagger, clique em "Authorize" e cole o token no formato `Bearer TOKEN`. Agora você pode testar o endpoint `POST /api/pedidos/embalar`.

Banco de Dados: usa SQL Server 2022, com as migrations aplicadas automaticamente no startup.

Testes unitários: podem ser executados com o comando `dotnet test` (utilizando xUnit).

👨‍💻 Desenvolvido por Carlos Rodrigues
