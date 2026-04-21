# 🍔 Good Burger API

API REST desenvolvida em **ASP.NET Core (.NET 8)** para registrar pedidos da lanchonete **Good Hamburger** para o desafio proposta na etapa técnica.

O projeto foi construído com foco em **código limpo**, **princípios SOLID** e tentando uma **arquitetura limpa**, evitando complexidade desnecessária para o escopo do desafio. Também foi utilizado **SQLite** pela simplicidade de configuração e execução local, e o fluxo de desenvolvimento foi organizado com **commits convencionais**.

---

## 🚀 Tecnologias utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

---

## 🧱 Arquitetura do projeto

A aplicação foi organizada em camadas com separação clara de responsabilidades:

- `Domain`: entidades, enums e regras centrais do negócio
- `Application`: DTOs, interfaces, serviços e casos de uso
- `Infrastructure`: persistência de dados com EF Core e repositórios
- `API`: controllers, middleware, contratos HTTP e configuração da aplicação

### ✅ Decisões técnicas

- Uso de **SQLite** para manter a solução simples e fácil de executar
- Cardápio centralizado em um catálogo único
- Cálculo de desconto isolado em um serviço específico
- Tratamento global de exceções para retornar respostas claras
- CRUD completo para pedidos
- Estrutura preparada para crescimento sem exagerar na complexidade

---

## 📋 Cardápio

### 🥪 Sanduíches
- `x-burger` — X Burger — **R$ 5,00**
- `x-egg` — X Egg — **R$ 4,50**
- `x-bacon` — X Bacon — **R$ 7,00**

### 🍟 Acompanhamentos
- `fries` — Batata frita — **R$ 2,00**

### 🥤 Bebidas
- `soft-drink` — Refrigerante — **R$ 2,50**

---

## 💸 Regras de desconto

As regras de desconto são aplicadas conforme a combinação de itens no pedido:

- Sanduíche + batata + refrigerante → **20% de desconto**
- Sanduíche + refrigerante → **15% de desconto**
- Sanduíche + batata → **10% de desconto**

> O desconto é calculado sobre o **subtotal** do pedido.

---

## ✅ Regras de validação

Cada pedido pode conter no máximo:

- **1 sanduíche**
- **1 batata**
- **1 refrigerante**

A API também valida:

- pedido sem itens
- item duplicado
- item inexistente no cardápio
- pedido não encontrado

Quando ocorre erro, a API retorna uma resposta clara informando o problema.

---

## 🔗 Endpoints

### Cardápio
- `GET /api/menu`

### Pedidos
- `POST /api/orders`
- `GET /api/orders`
- `GET /api/orders/{id}`
- `PUT /api/orders/{id}`
- `DELETE /api/orders/{id}`

---

## 📦 Exemplo de criação de pedido
{
  "items": [
    "x-burger",
    "fries",
    "soft-drink"
  ]
}


---

## 📤 Exemplo de resposta
{
  "id": "guid",
  "items": [
    {
      "code": "x-burger",
      "name": "X Burger",
      "category": "Sanduíche",
      "price": 5.0
    },
    {
      "code": "fries",
      "name": "Batata frita",
      "category": "Acompanhamento",
      "price": 2.0
    },
    {
      "code": "soft-drink",
      "name": "Refrigerante",
      "category": "Bebida",
      "price": 2.5
    }
  ],
  "subtotal": 9.5,
  "discountPercentage": 0.2,
  "discount": 1.9,
  "total": 7.6,
  "createdAtUtc": "2026-04-21T00:00:00Z",
  "updatedAtUtc": null
}

---

## 🛠️ Como executar o projeto

### 1. Restaurar dependências
dotnet restore

### 2. Criar a migration
dotnet ef migrations add InitialCreate

### 3. Atualizar o banco de dados
dotnet ef database update

### 4. Executar a aplicação
dotnet run


### 5. Acessar o Swagger

Após subir a aplicação, abra a URL exibida no terminal e acesse o Swagger para testar os endpoints.

---

## 🚫 O que ficou de fora

- autenticação/autorização
- testes automatizados
- versionamento de API
- paginação na listagem
- frontend em blazor