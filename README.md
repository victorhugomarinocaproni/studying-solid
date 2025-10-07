# 🧩 Studying SOLID

## 🎯 Objetivo  
Este projeto foi desenvolvido como parte da **EFC 1 da disciplina de Padrões e Arquitetura de Software (2025)**, no 6º semestre do curso de Sistemas de Informação na PUC-CAMPINAS.  
O objetivo principal é **refatorar um sistema legado de gerenciamento de pedidos** aplicando os **princípios SOLID** e práticas de **Clean Code**, tornando-o mais **organizado, extensível e manutenível**.

---

## 🧠 Contexto  
O código original era um sistema monolítico escrito em Python que concentrava diversas responsabilidades em uma única classe (`Sis`).  
Entre suas funcionalidades estavam:
- Cadastro e atualização de pedidos  
- Processamento de pagamentos  
- Envio de notificações  
- Validação de estoque  
- Geração de relatórios  

A proposta da atividade foi **refatorar completamente o código**, separando responsabilidades e aplicando abstrações que respeitam os cinco princípios SOLID.

---

## 🏗️ Estrutura do Projeto  

A refatoração foi implementada em **C#**, com base nos conceitos estudados em sala, mantendo a lógica de negócio e modularizando as camadas.  

```
Solid/
│
├── Contracts/                # Interfaces que definem contratos de abstração (ISP, DIP)
├── Database/                 # Simulação da camada de persistência de dados
├── Entities/                 # Classes de domínio (Order, Customer, OrderItem etc.)
├── Exceptions/               # Exceções personalizadas do sistema
├── Mappers/OrderItemMappers/ # Classes de mapeamento entre entidades e modelos
├── Repositories/             # Implementações concretas de repositórios (SRP + DIP)
├── Services/                 # Serviços de negócio (Notification, Payment, Reporting etc.)
├── Strategies/               # Estratégias para cálculos e regras variáveis (Strategy Pattern)
│
├── Program.cs                # Ponto de entrada da aplicação
└── Solid.csproj              # Configuração do projeto
```

---

## ⚙️ Principais Refatorações e Aplicações SOLID  

| Princípio | Aplicação na Refatoração |
|------------|---------------------------|
| **SRP** (Responsabilidade Única) | Separação de responsabilidades em múltiplas classes (PaymentService, OrderService, NotificationService etc.) |
| **OCP** (Aberto/Fechado) | Estratégias de pagamento e desconto permitem extensão sem alterar código existente |
| **LSP** (Substituição de Liskov) | As classes concretas respeitam as abstrações e podem ser trocadas por suas interfaces |
| **ISP** (Segregação de Interfaces) | Interfaces específicas para cada serviço, evitando dependências desnecessárias |
| **DIP** (Inversão de Dependência) | Serviços dependem de interfaces, não de implementações concretas |

---

## 🧩 Design Patterns Utilizados  

- **Strategy Pattern** → para cálculo de descontos e métodos de pagamento  
- **Repository Pattern** → para abstrair acesso a dados  
- **Dependency Injection** → para inversão de dependências entre classes  
- **Factory Pattern** → para criação de estratégias de pagamento e notificação

---

## ✅ Funcionalidades  

- Criação e atualização de pedidos  
- Cálculo de descontos (normal, 10%, 20%, cliente VIP)  
- Processamento de pagamentos (cartão, PIX, boleto)  
- Envio de notificações (e-mail, SMS, etc.)  
- Geração de relatórios de vendas e clientes  
- Registro de pontos para clientes VIP  

---

## 🧹 Práticas de Clean Code  

- Métodos curtos e coesos  
- Nomes de classes e variáveis descritivos  
- Eliminação de duplicações  
- Clareza sem comentários redundantes  
- Estrutura modular e reutilizável  

---

## 🧾 Como Executar  

1. **Clone o repositório:**  
   ```bash
   git clone https://github.com/victorhugomarinocaproni/studying-solid.git
   cd studying-solid/Solid
   ```

2. **Compile e execute o projeto (via .NET CLI):**  
   ```bash
   dotnet build
   dotnet run
   ```

3. **Explore os resultados no console** — os fluxos de pedidos, pagamentos e notificações são exibidos conforme a execução.
