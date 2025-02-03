# Hotel & Flight Booking System

A comprehensive console application for booking hotels and flights, built with C#. The system supports user registration, authentication, hotel and flight bookings, payment processing, and admin functionalities.

## Features

- **User Management**: Sign up, sign in, and manage user profiles.
- **Hotel Booking**: Search, filter, and book hotels with various amenities.
- **Flight Booking**: Book one-way or round-trip flights with seat class options.
- **Payment Processing**: Secure payment simulation with card validation.
- **Admin Dashboard**: Add/remove hotels, view users, and generate revenue reports.
- **Data Persistence**: Hotels and flights data stored in `.txt` files.

## Technologies Used

- **C#**: Core programming language.
- **.NET Framework**: For building the console application.
- **File I/O**: Reading/Writing data to `.txt` files.

## Key Principles Implemented
- **Object-Oriented Design**: Utilizes encapsulation, inheritance, and polymorphism to create a modular and reusable codebase.
- **Scalability**: The system is designed to handle increasing amounts of data and users without performance degradation.
- **Separation of Concerns**: Divides functionality into models, views, and controllers for better maintainability.
- **Automated Testing**: Includes unit and integration tests to ensure reliability and prevent regressions.
- **Error Handling & Logging**: Implements structured error handling and logging mechanisms for debugging and monitoring.


### Usage
- **User Registration/Sign In**
  - **New User**: Choose Sign Up and provide your email, name, and password (min 10 characters).
  - **Existing User**: Choose Sign In and enter your credentials.

- **Booking a Hotel**
  - From the homepage, select Stays.
  - Enter your destination, check-in/check-out dates, and number of rooms.
  - Filter hotels by price, rating, or amenities.
  - Select a hotel and room, then confirm payment.

- **Booking a Flight**
  - From the homepage, select Flights.
  - Choose One-Way or Round-Trip.
  - Enter departure/arrival locations, dates, and seat class.
  - Select a flight and ticket, then confirm payment.

- **Admin Features**
  - **Add/Remove Hotels**: Manage the list of available hotels.
  - **View Users**: See all registered users.
  - **Revenue Report**: Generate total revenue from bookings.

## 🗄 Data Storage
Data is stored in `.txt` files with the following format:

- **Hotels:**
  ```
  HotelName:Name,Country:Country,City:City,AvailableRooms:10,PricePerRoom:100,Rating:4.5,Amenities:WiFi|Pool,Rooms:Room1|Amenities|Price|Booked;Room2|...
  ```
- **Flights:**
  ```
  FlightId:1,Airline:EgyptAir,DepartureLocation:Cairo,ArrivalLocation:Alexandria,...
  ```
