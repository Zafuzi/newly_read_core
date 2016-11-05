# Newly Read Core
Newly Read Core is a Dotnet Core implementation of Newly Read http://newlyread.com

![Newly Read](https://github.com/Zafuzi/newly_read_core/raw/master/readme_images/langind_page.png)

## Features
- Uses [newsapi.org](newsapi.org) to gather sources and articles
- Uses Razor and SQLite to store and render articles

## Getting Started
This project requires Microsoft's .NET Core to run. You can follow instructions to install it here: [https://www.microsoft.com/net/core](https://www.microsoft.com/net/core)
- Download or clone this repo into an empty directory
- Open a terminal or command window and navigate to the downloaded folder
- Run: dotnet run
- Open your browser and navigate to localhost:5000
- Enjoy

## Todo
- URGENT
  - [ ] Find replacement for flex on older browsers and Apple devices. (Also Edge)
  - [ ] Add accounts using [StormPath](https://stormpath.com/)

- FEATURES  
  - [ ] Create a landing page
  - [ ] Implement a 'dark' mode
  - [ ] Redo the header navigation
  - [ ] Fix the size to be easier to click on mobile

- PERSONAL
  - [ ] Edit deploy.sh to not need so many passwords
  - [ ] Comment and Documentation
    - Any ideas where to start?