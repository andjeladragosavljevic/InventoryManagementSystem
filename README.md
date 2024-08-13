# InventoryManagementSystem

**InventoryManagementSystem** is a project that provides a comprehensive solution for managing inventory items, including users, items, item attributes, and user authentication.

## Contents

- [Overview](#overview)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Controllers](#controllers)
- [Database](#database)

## Overview
InventoryManagementSystem is a web application developed using ASP.NET Core and Entity Framework Core. The application allows users to register accounts, log in, and manage inventory items. The system also supports adding, updating, and deleting items and their attributes.

## Tech Stack

- **ASP.NET Core** - for developing the web API
- **Entity Framework Core** - for database operations
- **MySQL** - as the database management system
- **JWT** - for authentication and authorization


## Usage/Examples

### Authentication

#### Registration
- **Endpoint:** `POST /api/Korisnik/registration`
- **Request Body:**
    ```json
    {
      "firstName": "John",
      "lastName": "Doe",
      "phoneNumber": "123456789",
      "email": "john.doe@example.com",
      "password": "password123",
      "repeatedPassword": "password123"
    }
    ```
- **Response:**
    ```json
    {
      "id": 1,
      "firstName": "John",
      "lastName": "Doe",
      "phoneNumber": "123456789",
      "email": "john.doe@example.com",
      "password": "hashedPassword",
      "passwordSalt": "salt"
    }
    ```

#### Login
- **Endpoint:** `POST /api/Korisnik/login`
- **Request Body:**
    ```json
    {
      "email": "john.doe@example.com",
      "password": "password123"
    }
    ```
- **Response:**
    ```json
    {
      "token": "jwtToken"
    }
    ```

### Item

#### Add item
- **Endpoint:** `POST /api/Artikl/add`
- **Request Body:**
    ```json
    {
      "code": "12345",
      "name": "Sample Item",
      "measuringUnit": "pcs",
      "atributs": [
        {
          "atributID": 1,
          "value": "Value1"
        },
        {
          "atributID": 2,
          "value": "Value2"
        }
      ]
    }
    ```
- **Response:**
    ```json
    {
      "id": 1,
      "code": "12345",
      "name": "Sample Item",
      "measuringUnit": "pcs"
    }
    ```

#### Delete item
- **Endpoint:** `DELETE /api/Artikl/delete`
- **Request Parameter:**
    - `id` (int): The ID of the item to be deleted.
- **Response:**
    ```json
    {
      "id": 1,
      "code": "12345",
      "name": "Sample Item",
      "measuringUnit": "pcs"
    }
    ```

#### Edit item
- **Endpoint:** `PUT /api/Artikl/edit`
- **Request Body:**
    ```json
    {
      "id": 1,
      "code": "12345",
      "name": "Updated Item",
      "measuringUnit": "pcs",
      "atributs": [
        {
          "atributID": 1,
          "value": "Updated Value1"
        }
      ]
    }
    ```
- **Response:** `200 OK`

#### Get all items
- **Endpoint:** `GET /api/Artikl/get`
- **Response:**
    ```json
    [
      {
        "id": 1,
        "code": "12345",
        "name": "Sample Item",
        "measuringUnit": "pcs",
        "atributs": [
          {
            "atributID": 1,
            "value": "Value1"
          },
          {
            "atributID": 2,
            "value": "Value2"
          }
        ]
      }
    ]
    ```

### Attributes

#### Add attribute
- **Endpoint:** `POST /api/Atribut/add`
- **Request Body:**
    ```json
    {
      "name": "AttributeName"
    }
    ```
- **Response:**
    ```json
    {
      "id": 1,
      "attributName": "AttributeName"
    }
    ```

#### Get all attributes
- **Endpoint:** `GET /api/Atribut/get`
- **Response:**
    ```json
    [
      {
        "id": 1,
        "attributName": "AttributeName"
      }
    ]
    ```

### Users

#### Get all users
- **Endpoint:** `GET /api/Korisnik/get`
- **Response:**
    ```json
    [
      {
        "id": 1,
        "firstName": "John",
        "lastName": "Doe",
        "phoneNumber": "123456789",
        "email": "john.doe@example.com"
      }
    ]
    ```






