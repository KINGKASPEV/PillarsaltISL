# PillarsaltISL


# Scratch Card Management System


## Overview


This project is a scratch card management system implemented using ASP.NET Core. It allows users to:


- Generate scratch cards
- Purchase scratch cards
- Use scratch cards by validating a PIN


The system tracks the state of each scratch card with flags for whether a card is purchased or used, ensuring that a card cannot be purchased or used more than once.


## Features


- Generate Cards: Create a specified number of scratch cards with unique serial numbers and PINs.
- Purchase Cards: Mark a scratch card as purchased. Ensures that a card cannot be purchased more than once.
- Use Cards: Validate a scratch card using its PIN. Marks the card as used if the PIN is correct and the card has been purchased.
- Error Handling: Properly handles scenarios such as attempting to purchase a used card, using a card with an incorrect PIN, and other edge cases.


## API Endpoints


- Generate Cards
  - POST `/api/scratchcard/generate?count={count}`
  - Creates the specified number of scratch cards.


- Purchase Card
  - POST `/api/scratchcard/purchase?serialNumber={serialNumber}`
  - Marks the card with the given serial number as purchased.


- Use Card
  - POST `/api/scratchcard/use?serialNumber={serialNumber}&pin={pin}`
  - Validates and marks the card with the given serial number as used if the PIN is correct.


## Installation For those Visual Studio Users


1. Clone the repository:
   ```bash
   git clone https://github.com/KINGKASPEV/PillarsaltISL.git


2. Change your connection strings in the appsettings.Json


3. Update-Database


3. Run the program


## Installation For those VSCode Users


1. Clone the repository:
   ```bash
   git clone https://github.com/KINGKASPEV/PillarsaltISL.git


2. cd CRUD


3. dotnet restore


4. dotnet build


5. dotnet run



