# 💳 FCG Payments Microservice

Microsserviço responsável pelo **processamento e gerenciamento de pagamentos** da plataforma **FIAP Cloud Games (FCG)**, desenvolvido como parte do **Tech Challenge – Fase 3**.

Este serviço foi separado seguindo a proposta de **arquitetura em microsserviços**, mantendo baixo acoplamento com os módulos de **Usuários** e **Jogos**, e preparado para futura integração via **API Gateway** e **Serverless**.

---

## 🎯 Responsabilidade do microsserviço

O microsserviço de **Pagamentos** é responsável por:

- Criar pagamentos (checkout)
- Consultar o status de um pagamento
- Aprovar pagamentos
- Rejeitar pagamentos
- Persistir o histórico de transações

> ⚠️ A validação de usuários e jogos é responsabilidade de outros microsserviços (Users e Games).

---

## 🧱 Arquitetura adotada

O projeto segue uma **arquitetura em camadas**, alinhada aos demais microsserviços do grupo e aos princípios de **Clean Architecture**:

- **Domain** → regras de negócio e entidades
- **Application** → casos de uso e serviços
- **Infra** → persistência e acesso a dados
- **Web** → API (Minimal API + Swagger)
- **Common** → contratos e respostas compartilhadas
- **Tests** → estrutura de testes

---

## 📁 Estrutura do projeto

```text
Fcg.Payment
├─ README.md
├─ .gitignore
│
├─ src
│  ├─ Fcg.Payment.Application
│  │  ├─ Requests
│  │  │  └─ CheckoutRequest.cs
│  │  ├─ Responses
│  │  │  ├─ CheckoutResponse.cs
│  │  │  └─ PaymentResponse.cs
│  │  └─ Services
│  │     └─ PaymentService.cs
│  │
│  ├─ Fcg.Payment.Common
│  │  └─ Responses
│  │     ├─ IResponse.cs
│  │     └─ Response.cs
│  │
│  ├─ Fcg.Payment.Domain
│  │  ├─ Entities
│  │  │  └─ PaymentTransaction.cs
│  │  ├─ Enums
│  │  │  └─ PaymentStatus.cs
│  │  └─ Repositories
│  │     └─ IPaymentRepository.cs
│  │
│  ├─ Fcg.Payment.Infra
│  │  ├─ Data
│  │  │  └─ PaymentDbContext.cs
│  │  ├─ Migrations
│  │  │  └─ (migrations geradas pelo EF Core)
│  │  └─ Repositories
│  │     └─ PaymentRepository.cs
│  │
│  └─ Fcg.Payment.Web
│     ├─ appsettings.json
│     ├─ Fcg.Payment.Web.http
│     └─ Program.cs
│        └─ Minimal API + Swagger + endpoints
│
└─ tests
   └─ Fcg.Payment.Tests