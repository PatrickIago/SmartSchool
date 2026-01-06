# 🎓 SmartSchool .NET + Angular

O **SmartSchool** é um sistema de gestão escolar robusto, projetado para administrar de forma eficiente o ecossistema educacional. A aplicação permite que administradores e professores controlem listagens de alunos, visualizem detalhes de disciplinas e acompanhem métricas importantes por meio de um dashboard intuitivo.

---

## 🚀 Tecnologias e Versões

### 🎨 Frontend
* **Angular 12** – Framework principal para construção de uma SPA performática.
* **Bootstrap 4.5.2** – Responsividade e componentes visuais modernos.
* **Reactive Forms** – Validações avançadas e gerenciamento de estado.
* **ngx-bootstrap** – Modais reativos e componentes de UI dinâmicos.
* **ngx-toastr** – Notificações em tempo real.
* **ngx-spinner** – Indicadores visuais de carregamento.

### ⚙️ Backend & Infraestrutura
* **C# com .NET 6** – Web API responsável pela regra de negócio.
* **SQL Server** – Banco de dados relacional para persistência segura.
* **Docker** – Containerização completa do ambiente para facilitar o deploy.

---

## 🐳 Como Executar com Docker

Para subir o ambiente completo (**SQL Server + API .NET 6**):

### Pré-requisitos
* [Docker Desktop](https://www.docker.com/products/docker-desktop) instalado e em execução.

### Passos
No diretório raiz da solução, execute:
```bash
docker-compose up -d
