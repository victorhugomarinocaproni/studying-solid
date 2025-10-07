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

## 🚀 Ponto de Partida 
O sistema monolítico pode ser encontrado logo abaixo:
```bash
import sqlite3
import json
from datetime import datetime

class Sis:
    def __init__(self):
        self.db = sqlite3.connect('loja.db')
        self.c = self.db.cursor()

        # Cria tabela
        self.c.execute('''CREATE TABLE IF NOT EXISTS ped (
            id INTEGER PRIMARY KEY,
            cli TEXT,
            itens TEXT,
            tot REAL,
            st TEXT,
            dt TEXT,
            tp TEXT
        )''')
        self.db.commit()

    def add_ped(self, n, its, t):
        # Adiciona pedido
        dt = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        tot = 0

        # Calcula total
        for i in its:
            if i['tipo'] == 'normal':
                tot += i['p'] * i['q']
            elif i['tipo'] == 'desc10':
                tot += i['p'] * i['q'] * 0.9  # 10% desconto
            elif i['tipo'] == 'desc20':
                tot += i['p'] * i['q'] * 0.8  # 20% desconto

        # Valida cliente VIP
        if t == 'vip':
            tot = tot * 0.95  # 5% desconto vip

        its_str = json.dumps(its)
        self.c.execute(
            "INSERT INTO ped (cli, itens, tot, st, dt, tp) VALUES (?, ?, ?, ?, ?, ?)",
            (n, its_str, tot, 'pendente', dt, t)
        )
        self.db.commit()

        # Envia email (simulado)
        print(f"Email enviado para {n}: Pedido recebido!")
        return self.c.lastrowid

    def get_ped(self, id):
        # Busca pedido
        self.c.execute("SELECT * FROM ped WHERE id = ?", (id,))
        r = self.c.fetchone()
        if r:
            return {
                'id': r[0],
                'cli': r[1],
                'itens': json.loads(r[2]),
                'tot': r[3],
                'st': r[4],
                'dt': r[5],
                'tp': r[6]
            }
        return None

    def upd_st(self, id, s):
        # Atualiza status
        p = self.get_ped(id)
        if p:
            self.c.execute("UPDATE ped SET st = ? WHERE id = ?", (s, id))
            self.db.commit()

            # Envia notificações
            if s == 'aprovado':
                print(f"Email enviado para {p['cli']}: Pedido aprovado!")
                print(f"SMS enviado para {p['cli']}: Pedido aprovado!")
            elif s == 'enviado':
                print(f"Email enviado para {p['cli']}: Pedido enviado!")
            elif s == 'entregue':
                print(f"Email enviado para {p['cli']}: Pedido entregue!")

            # Registra pontos
            if p['tp'] == 'vip':
                pts = int(p['tot'] * 2)  # vip ganha 2x pontos
                print(f"Cliente VIP ganhou {pts} pontos!")
            else:
                pts = int(p['tot'])
                print(f"Cliente ganhou {pts} pontos!")

    def calc_tot_cli(self, n):
        # Calcula total gasto pelo cliente
        self.c.execute("SELECT * FROM ped WHERE cli = ?", (n,))
        rs = self.c.fetchall()
        t = 0
        for r in rs:
            t += r[3]
        return t

    def gerar_rel(self, tipo):
        # Gera relatório
        if tipo == 'vendas':
            self.c.execute("SELECT * FROM ped")
            rs = self.c.fetchall()
            print("=== RELATÓRIO DE VENDAS ===")
            tot_g = 0
            for r in rs:
                print(f"Pedido #{r[0]} - Cliente: {r[1]} - Total: R$ {r[3]:.2f} - Status: {r[4]}")
                tot_g += r[3]
            print(f"Total Geral: R$ {tot_g:.2f}")

            # Salva em arquivo
            with open('rel_vendas.txt', 'w') as f:
                f.write(f"Total de vendas: {tot_g}")

        elif tipo == 'clientes':
            self.c.execute("SELECT DISTINCT cli, tp FROM ped")
            rs = self.c.fetchall()
            print("=== RELATÓRIO DE CLIENTES ===")
            for r in rs:
                n = r[0]
                tp = r[1]
                tot = self.calc_tot_cli(n)
                print(f"Cliente: {n} ({tp}) - Total gasto: R$ {tot:.2f}")

            # Salva em arquivo
            with open('rel_clientes.txt', 'w') as f:
                for r in rs:
                    f.write(f"{r[0]},{r[1]}\n")

    def proc_pag(self, id, m, vl):
        # Processa pagamento
        p = self.get_ped(id)
        if not p:
            return False

        # Valida valor
        if vl < p['tot']:
            print("Valor insuficiente!")
            return False

        # Processa de acordo com método
        if m == 'cartao':
            print("Processando pagamento com cartão...")
            print("Cartão validado!")
            self.upd_st(id, 'aprovado')
            return True
        elif m == 'pix':
            print("Gerando QR Code PIX...")
            print("PIX recebido!")
            self.upd_st(id, 'aprovado')
            return True
        elif m == 'boleto':
            print("Gerando boleto...")
            print("Boleto gerado!")
            # boleto não aprova automaticamente
            return True
        else:
            print("Método de pagamento inválido!")
            return False

    def validar_estoque(self, its):
        # Valida estoque (simplificado)
        est = {'produto1': 100, 'produto2': 50, 'produto3': 75}
        for i in its:
            if i['nome'] not in est:
                print(f"Produto {i['nome']} não encontrado!")
                return False
            if est[i['nome']] < i['q']:
                print(f"Estoque insuficiente para {i['nome']}!")
                return False
        return True

    def close(self):
        self.db.close()


class PedEspecial(Sis):
    def add_ped(self, n, its, t):
        # Pedido especial tem taxa extra
        dt = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        tot = 0
        for i in its:
            if i['tipo'] == 'normal':
                tot += i['p'] * i['q']
            elif i['tipo'] == 'desc10':
                tot += i['p'] * i['q'] * 0.9
            elif i['tipo'] == 'desc20':
                tot += i['p'] * i['q'] * 0.8
        # Taxa especial
        tot = tot * 1.15  # 15% taxa
        its_str = json.dumps(its)
        self.c.execute(
            "INSERT INTO ped (cli, itens, tot, st, dt, tp) VALUES (?, ?, ?, ?, ?, ?)",
            (n, its_str, tot, 'pendente', dt, t)
        )
        self.db.commit()
        print(f"Email especial enviado para {n}: Pedido especial recebido!")
        return self.c.lastrowid


def main():
    s = Sis()

    # Exemplo de uso
    its1 = [
        {'nome': 'produto1', 'p': 100, 'q': 2, 'tipo': 'normal'},
        {'nome': 'produto2', 'p': 50, 'q': 1, 'tipo': 'desc10'}
    ]

    if s.validar_estoque(its1):
        id1 = s.add_ped('João Silva', its1, 'normal')
        print(f"Pedido {id1} criado!")

        # Processa pagamento
        s.proc_pag(id1, 'cartao', 250)

        # Atualiza para enviado
        s.upd_st(id1, 'enviado')

        # Atualiza para entregue
        s.upd_st(id1, 'entregue')

    # Pedido VIP
    its2 = [
        {'nome': 'produto3', 'p': 200, 'q': 1, 'tipo': 'desc20'}
    ]

    if s.validar_estoque(its2):
        id2 = s.add_ped('Maria Santos', its2, 'vip')
        s.proc_pag(id2, 'pix', 160)

    # Gera relatórios
    s.gerar_rel('vendas')
    print()
    s.gerar_rel('clientes')

    s.close()


if __name__ == '__main__':
    main()
```


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
