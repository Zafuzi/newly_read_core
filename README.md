# Newly Read Core
Newly Read Core is a Dotnet Core implementation of Newly Read http://newlyread.com

## Features
- Uses [newsapi.org](newsapi.org) to gather sources and articles
- Uses Razor and SQLite to store and render articles

## Getting Started
This project requires Microsoft's .NET Core to run. You can follow instructions to install it here: [https://www.microsoft.com/net/core](https://www.microsoft.com/net/core)
- Download or clone this repo into an empty directory
- Open a terminal or command window and navigate to the downloaded folder
- Run: dotnet restore
- Run: dotnet run
- Open your browser and navigate to localhost:5000
- Enjoy

## Todo
- Implement a simpler solution for user authentication.
- Shorten articles url's by storing a guid code in the DB. Consider using a seperate DB file for this purpose
- Add exception handlers for failed JSON Reader. Unsure what line of code is causing this error, but it is most likely something involving writing to or reading from the DB
- Rebuild the ReadAPI to more closely integrate with the DB.
  - Add measures for dealing with missing images.
  - Rewrite JSON storage to HTML storage to make conversion easier.
  - Add filters for authentication access to important DB information for security.
- Consider using filters to add premium features for authenticated users.
  - Some features may include saving articles, flagging content, and moderating posts.
- Consider adding user created content.
  - Not exactly sure if I want to implement something like this. My main fear is fake news or spammed articles, as well as secruity issues and increase complexity.