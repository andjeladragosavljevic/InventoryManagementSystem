# InventoryManagementSystem

**InventoryManagementSystem** is an upgraded project providing a comprehensive solution for managing inventory items, including users, items, item attributes, and user authentication.

## Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Usage/Examples](#usageexamples)

## Overview
InventoryManagementSystem is a web application developed using ASP.NET Core (.NET 8) and Entity Framework Core. The application allows users to register accounts, log in, and manage inventory items. The system also supports adding, updating, and deleting items and their attributes.

## Tech Stack

- **ASP.NET Core (.NET 8)** - for developing the web API
- **Entity Framework Core** - for database operations
- **MySQL** - as the database management system
- **JWT** - for authentication and authorization
- **C# 12** - Leveraging new language features for concise and efficient code

## Usage/Examples

### Authentication

#### Registration
- **Endpoint:** `POST /api/User/registration`
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
- **Endpoint:** `POST /api/User/login`
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
- **Endpoint:** `POST /api/Article`
- **Request Body:**
    ```json
    {
      "code": "12345",
      "name": "Sample Item",
      "measuringUnit": "pcs",
      "attributes": [
        {
          "attributeID": 1,
          "value": "Value1"
        },
        {
          "attributeID": 2,
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
- **Endpoint:** `DELETE /api/Article`
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
- **Endpoint:** `PUT /api/Article`
- **Request Body:**
    ```json
    {
      "id": 1,
      "code": "12345",
      "name": "Updated Item",
      "measuringUnit": "pcs",
      "attributes": [
        {
          "attributeID": 1,
          "value": "Updated Value1"
        }
      ]
    }
    ```
- **Response:** `200 OK`

#### Get all items
- **Endpoint:** `GET /api/Article`
- **Response:**
    ```json
    [
      {
        "id": 1,
        "code": "12345",
        "name": "Sample Item",
        "measuringUnit": "pcs",
        "attributes": [
          {
            "attributeID": 1,
            "value": "Value1"
          },
          {
            "attributeID": 2,
            "value": "Value2"
          }
        ]
      }
    ]
    ```

### Attributes

#### Add attribute
- **Endpoint:** `POST /api/Attribute`
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
      "attributeName": "AttributeName"
    }
    ```

#### Get all attributes
- **Endpoint:** `GET /api/Attribute`
- **Response:**
    ```json
    [
      {
        "id": 1,
        "attributeName": "AttributeName"
      }
    ]
    ```

### Users

#### Get all users
- **Endpoint:** `GET /api/User`
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


