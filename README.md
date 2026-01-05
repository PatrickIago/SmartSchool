🎓 SmartSchoolApp

O SmartSchool é um sistema de gestão escolar robusto, projetado para administrar de forma eficiente o ecossistema educacional.
A aplicação permite que administradores e professores controlem listagens de alunos, visualizem detalhes de disciplinas e acompanhem métricas importantes por meio de um dashboard intuitivo.

🚀 Tecnologias e Versões
🎨 Frontend

Angular 12 – Framework principal para construção de uma SPA (Single Page Application) performática

Bootstrap 4.5.2 – Responsividade e componentes visuais modernos

Reactive Forms – Validações avançadas e gerenciamento de estado dos formulários

ngx-bootstrap – Modais reativos e componentes de UI dinâmicos

ngx-toastr – Notificações em tempo real

ngx-spinner – Indicadores visuais de carregamento

⚙️ Backend & Infraestrutura

C# com .NET 6 – Web API responsável pela regra de negócio

SQL Server – Banco de dados relacional para persistência segura

Docker – Containerização completa do ambiente para facilitar desenvolvimento e deploy

🐳 Como Executar com Docker

Para subir o ambiente completo (SQL Server + API .NET 6):

Pré-requisitos

Docker Desktop instalado e em execução

Passos

No diretório raiz da solução, execute:

docker-compose up -d


O Docker irá provisionar automaticamente todos os containers necessários.

💻 Como Executar o Frontend (Angular)
Pré-requisitos

Node.js: 14.0.0

npm: 8.19.4

Angular CLI instalado globalmente

Instalação

Entre na pasta do projeto:

cd SmartSchoolApp


Instale as dependências:

npm install


Inicie a aplicação:

ng serve


Acesse no navegador:
👉 http://localhost:4200/
