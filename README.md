# Project Tenjin

Project Tenjin is a wannabe social media platform designed for developers, inspired by the Japanese god of learning and scholarship. Our goal is to create a space where programmers and designers can connect, share their skills, discuss tools and frameworks, and find job opportunities.

This project is built with an ASP.NET Core Web API backend, an Angular frontend, SQL Server for data storage, Entity Framework as an ORM, and xUnit, Mock and FluentAssertions for unit testing.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Contributing](#contributing)
- [Roadmap](#roadmap)
- [License](#license)

## Features

- User authentication and registration
- Profile creation and editing
- Post creation
- User follow
- Feed

## Getting Started

### Prerequisites

Before you start, make sure you have the following software installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/en/download/)
- [Angular CLI](https://cli.angular.io/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repo:

```bash
git clone https://github.com/vekt0R-HUB/Project-Tenjin.git
```

2. Change directories to the repo folder and restore dependencies:

```bash
cd Project-Tenjin
dotnet restore
```

3. Update the connection string in `appsettings.json` to point to your SQL Server instance.

4. Change directories to the `SocialMediaRepositories` and apply the database migrations:

```bash
cd SocialMediaRepositories
dotnet ef database update
```

5. Change directories to the `SocialMediaUi` folder and install dependencies:

```bash
cd ../SocialMediaUi
npm install
```

## Usage

1. Start the backend server from the `SocialMediaRepositories` folder:

```bash
dotnet run
```

2. Start the frontend server from the `frontend` folder:

```bash
ng serve
```

3. Open your browser and navigate to [http://localhost:4200](http://localhost:4200) to access the application.

## Running Tests

To run the unit tests for the backend, change directories to the `SocialMediaApiTesting` folder and run:

```bash
dotnet test
```

## Contributing

We appreciate any contributions to Project Tenjin! To contribute, please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a pull request

## Roadmap

- Commenting on posts
- Liking and sharing posts
- Real-time chat and messaging
- Job posting and searching
- Skill, tool and framework tagging
- Notifications
- Implementing user groups and communities
- Enhancing real-time chat with group chats and voice/video calls
- Integrating with popular developer tools and platforms (GitHub, GitLab, Bitbucket, etc.)
- Adding a built-in code editor and collaboration features
- Gamification: badges, points, and leveling system
- Advanced search and filtering options for job postings

## License

This project currently doesn't have a license so use it as you like !!
