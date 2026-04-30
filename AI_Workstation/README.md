# Weather Forecast — Ontario, Canada

A full-stack weather forecast application that displays current and upcoming weather for cities in Ontario, Canada. The app features a .NET back-end API that fetches data from OpenWeatherMap and a React front-end with a professional UI, dark/light mode, and city search.

---

## Project Hierarchy & Technology Stack

```
AI_Workstation/
├── WeatherWebService/          # Back-End API
│   ├── Controllers/
│   │   └── WeatherController.cs
│   ├── Models/
│   │   ├── WeatherResponse.cs
│   │   └── DayForecast.cs
│   ├── Services/
│   │   ├── IWeatherService.cs
│   │   └── WeatherService.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Program.cs
│   └── appsettings.json
│
├── WeatherAwesome/             # Front-End App
│   ├── src/
│   │   ├── components/
│   │   │   ├── Header.jsx
│   │   │   ├── SearchBar.jsx
│   │   │   ├── WeatherCard.jsx
│   │   │   └── WeatherGrid.jsx
│   │   ├── context/
│   │   │   └── ThemeContext.jsx
│   │   ├── hooks/
│   │   │   └── useWeather.js
│   │   ├── App.jsx
│   │   ├── App.css
│   │   ├── index.css
│   │   └── main.jsx
│   ├── vite.config.js
│   └── package.json
│
└── README.md
```

| Layer | Technology | Port |
|-------|-----------|------|
| Back-End | .NET 10, ASP.NET Core Web API (Controller-based) | 3333 |
| Front-End | React, Vite 5 | 4444 |
| Weather Data | OpenWeatherMap API (5-day forecast) | — |

---

## Prerequisites

### Software Requirements

| Tool | Version | Download |
|------|---------|----------|
| .NET SDK | 8.0+ (project targets .NET 10) | https://dotnet.microsoft.com/download |
| Node.js | 18.0+ | https://nodejs.org |
| npm | 9.0+ (comes with Node.js) | — |

### OpenWeatherMap API Key

This project requires a **free** API key from OpenWeatherMap:

1. Go to https://openweathermap.org/api and create an account
2. Navigate to **API Keys** in your profile
3. Copy your API key
4. Open `WeatherWebService/appsettings.json` and replace the placeholder:

```json
{
  "OpenWeatherMap": {
    "ApiKey": "YOUR_API_KEY_HERE"   ← paste your key here
  }
}
```

> **Note:** New API keys may take up to 2 hours to activate on OpenWeatherMap's side.

---

## How to Run Locally

### 1. Start the Back-End (port 3333)

```bash
cd AI_Workstation/WeatherWebService
dotnet run
```

The API will be available at `http://localhost:3333/api/weather`

**Test it:**
```bash
curl "http://localhost:3333/api/weather?city=Toronto&days=3"
```

### 2. Start the Front-End (port 4444)

In a separate terminal:

```bash
cd AI_Workstation/WeatherAwesome
npm install        # only needed first time
npm run dev
```

Open your browser at `http://localhost:4444`

### Usage

- The app loads with **Toronto, 5-day forecast** by default
- Use the search box to look up any Ontario city (e.g., Ottawa, Hamilton, London)
- Adjust the number of days (1–5) using the days input
- Toggle between light and dark mode using the theme button in the header
