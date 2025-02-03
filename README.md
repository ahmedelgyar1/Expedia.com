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

## Project Structure
expedia.com/
├── Data/
│ ├── hotels.txt # Contains 100+ hotels data
│ └── flights.txt # Contains flight data
├── Classes/
│ ├── Hotel.cs # Hotel class and room management
│ ├── Flight.cs # Flight class and ticket management
│ ├── User.cs # User model and authentication
│ ├── Payment.cs # Payment processing logic
│ ├── Admin.cs # Admin functionalities
│ ├── Booking.cs # Base booking class
│ └── ... # Other helper classes
├── Interfaces/
│ ├── IUser.cs # User interface
│ └── IUserManager.cs # User management interface
└── Program.cs # Main entry point

