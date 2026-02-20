# Patrimonium Backend

## Visão Geral

O **Patrimonium** é um sistema institucional de gestão patrimonial,
financeira e contábil, construído sobre os princípios da **Clean
Architecture**, **DDD**, **imutabilidade lógica**, **compliance by
design** e **auditoria nativa**.

Não é CRUD.\
Não é app comum.\
É **infraestrutura institucional**.

------------------------------------------------------------------------

## Princípios Arquiteturais

-   Application só conversa com **interfaces**
-   Application **nunca depende** de Infrastructure
-   Dependências sempre apontam para dentro
-   Clean Architecture estrita
-   Domain isolado
-   Infrastructure como detalhe técnico
-   Persistência não contamina regra de negócio
-   DDD real (Entity, ValueObject, Aggregate, Domain Service)
-   Event-driven mindset
-   Imutabilidade lógica
-   Versionamento institucional

------------------------------------------------------------------------

## Estrutura de Camadas

    Patrimonium.Domain
    Patrimonium.Application
    Patrimonium.Infrastructure
    Patrimonium.API

### Domain

-   Entidades puras
-   ValueObjects
-   Enums
-   Regras de negócio
-   Invariantes
-   Exceções de domínio

### Application

-   UseCases
-   Interfaces de repositório
-   DTOs
-   Services
-   Orquestração de fluxo

### Infrastructure

-   EF Core
-   Repositórios concretos
-   Providers
-   Mappings
-   Integrações
-   Persistência
-   External services

### API

-   Controllers
-   Middlewares
-   Auth
-   Configuração
-   Injeção de dependência

------------------------------------------------------------------------

## Módulos Principais

### Patrimonial

-   Properties
-   Assets
-   Valuation
-   Snapshot patrimonial

### Financeiro

-   Transactions
-   Categories
-   Cashflow
-   Snapshot financeiro

### Operacional

-   Custos
-   Operações
-   Eventos
-   Snapshot operacional

------------------------------------------------------------------------

## Monthly Closing Engine

Sistema de fechamento mensal institucional:

-   Consolidação automática
-   Snapshot mensal
-   Histórico fechado
-   Versionamento
-   Imutabilidade lógica
-   Base contábil
-   Base fiscal
-   Base legal
-   Base de auditoria
-   Base de relatórios
-   Base de compliance
-   Base de exportação contábil
-   Base para BI
-   Base para IA

Entidades: - MonthlyClosing - MonthlySnapshot - FinancialSnapshot -
OperationalSnapshot - PatrimonialSnapshot - PropertySnapshot -
TransactionSnapshot

------------------------------------------------------------------------

## Compliance & Audit Engine

-   Trilhas de auditoria
-   Logs imutáveis
-   Event sourcing light
-   Cadeia de hash
-   Verificação de integridade
-   Provas criptográficas
-   Auditoria automática
-   Assinatura digital
-   Validação histórica
-   Exportação fiscal/legal

------------------------------------------------------------------------

## Institutional Layer (em construção)

-   Ledger (double-entry)
-   Journal
-   Plano de contas
-   Subledger
-   Consolidação
-   Holding
-   Multi-empresa
-   Multi-CNPJ
-   Fiscal
-   DRE
-   Balanço
-   Regulatório
-   Compliance bancário
-   Integração contábil
-   Base regulatória

------------------------------------------------------------------------

## Tecnologias

-   .NET
-   EF Core
-   PostgreSQL
-   Clean Architecture
-   DDD
-   CQRS ready
-   Event-driven ready
-   Modular Monolith ready
-   Microservices ready

------------------------------------------------------------------------

## Posicionamento

Patrimonium não é sistema de controle financeiro pessoal.\
Não é app doméstico.\
Não é MVP frágil.

É arquitetura institucional. É base de produto SaaS. É motor financeiro.
É infraestrutura patrimonial. É plataforma.

------------------------------------------------------------------------

## Filosofia

Tradicional onde importa.\
Moderno onde gera vantagem.\
Inovador onde cria mercado.\
Conservador onde há risco.

------------------------------------------------------------------------

## Status do Projeto

Em desenvolvimento ativo.\
Arquitetura em expansão institucional.\
Foco em robustez, escalabilidade e compliance.

------------------------------------------------------------------------

## Autor

Projeto desenvolvido com foco em arquitetura de alto nível, padrões
institucionais e design de sistemas financeiros complexos.
