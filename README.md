# Projeto TryBets

## Descrição

O TryBets é um sistema de API para um site de apostas esportivas, estruturado em microserviços. O projeto possui quatro entidades principais: Users (Usuários), Teams (Times), Matches (Partidas) e Bets (Apostas).

Originalmente monolítico, o sistema foi dividido em microserviços independentes, cada um responsável por uma funcionalidade específica, facilitando a manutenção e escalabilidade.

## Tecnologias Utilizadas

- .NET 6.0
- Entity Framework Core 7.0
- SQL Server (Azure SQL Edge para container)
- JWT para autenticação e autorização
- Docker e Docker Compose para orquestração dos microserviços
- Swagger para documentação da API

## Arquitetura

O projeto é composto pelos seguintes microserviços:

- **TryBets.Users**: Gerencia usuários, autenticação e autorização via JWT.
- **TryBets.Matches**: Gerencia as partidas e os times.
- **TryBets.Bets**: Gerencia as apostas feitas pelos usuários.
- **TryBets.Odds**: Gerencia as odds (cotações) das partidas.

Cada microserviço possui seu próprio contexto de banco de dados, modelos, repositórios e controladores.

![Arquitetura de Microserviços](img/arq-micro.png)

## Diagrama de Entidade-Relacionamento (DER)

![DER do Banco de Dados](img/trybets-der.png)

## Instalação e Execução

### Pré-requisitos

- Docker e Docker Compose instalados

### Passos para rodar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/rita-moura/40-Project-TryBets.git
cd 40-Project-TryBets
```

2. Suba os contêineres Docker:

```bash
docker-compose -f docker-compose.microservices.yml up -d --build
```

3. A API estará disponível nos seguintes endereços:
    - **TryBets.Users**: `http://localhost:5501`
    - **TryBets.Matches**: `http://localhost:5502`
    - **TryBets.Bets**: `http://localhost:5503`
    - **TryBets.Odds**: `http://localhost:5504`

## Endpoints da API

### TryBets.Users

| Método | Rota | Descrição |
| --- | --- | --- |
| `POST` | `/user/signup` | Registra um novo usuário. |
| `POST` | `/user/login` | Autentica um usuário e retorna um token JWT. |

### TryBets.Matches

| Método | Rota | Descrição |
| --- | --- | --- |
| `GET` | `/team` | Retorna todos os times. |
| `GET` | `/match/{MatchFinished}` | Retorna as partidas finalizadas (`true`) ou não finalizadas (`false`). |

### TryBets.Bets

| Método | Rota | Descrição |
| --- | --- | --- |
| `POST` | `/bet` | Cria uma nova aposta (requer autenticação). |
| `GET` | `/bet/{BetId}` | Retorna uma aposta específica (requer autenticação). |

### TryBets.Odds

| Método | Rota | Descrição |
| --- | --- | --- |
| `PATCH` | `/odd/{MatchId}/{TeamId}/{BetValue}` | Atualiza o valor da odd de um time em uma partida. |

## Como Contribuir

Contribuições são bem-vindas! Para contribuir com o projeto, siga os seguintes passos:

1.  Faça um fork do projeto.
2.  Crie uma nova branch (`git checkout -b feature/nova-feature`).
3.  Faça commit de suas alterações (`git commit -m 'Adiciona nova feature'`).
4.  Faça push para a branch (`git push origin feature/nova-feature`).
5.  Abra um Pull Request.