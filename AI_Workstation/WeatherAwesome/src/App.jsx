import { useState } from 'react'
import './App.css'
import Header from './components/Header'
import SearchBar from './components/SearchBar'
import WeatherGrid from './components/WeatherGrid'
import { ThemeProvider } from './context/ThemeContext'
import useWeather from './hooks/useWeather'

function WeatherApp() {
  const [city, setCity] = useState('Toronto')
  const [state, setState] = useState('ON')
  const [country, setCountry] = useState('CA')
  const [days, setDays] = useState(5)
  const [query, setQuery] = useState({ city: 'Toronto', days: 5, state: 'ON', country: 'CA' })
  const { data, loading, error } = useWeather(query.city, query.days, query.state, query.country)

  const handleSearch = ({ city: nextCity, days: nextDays, state: nextState, country: nextCountry }) => {
    setCity(nextCity)
    setDays(nextDays)
    setState(nextState || '')
    setCountry(nextCountry || '')
    setQuery({ city: nextCity, days: nextDays, state: nextState, country: nextCountry })
  }

  const locationName = data?.location?.name || query.city
  const countryName = data?.location?.country || ''

  return (
    <div className="app-shell">
      <div className="app-background" aria-hidden="true" />
      <main className="app-container">
        <Header />
        <SearchBar
          initialCity={city}
          initialDays={days}
          initialState={state}
          initialCountry={country}
          onSearch={handleSearch}
          disabled={loading}
        />
        <section className="results-panel">
          <div className="results-copy">
            <p className="eyebrow">Forecast overview</p>
            <h2>
              {locationName}
              {countryName ? `, ${countryName}` : ''}
            </h2>
            <p className="results-meta">
              Viewing {query.days}-day outlook with live backend data.
            </p>
          </div>
          <WeatherGrid data={data?.days} loading={loading} error={error} />
        </section>
      </main>
    </div>
  )
}

function App() {
  return (
    <ThemeProvider>
      <WeatherApp />
    </ThemeProvider>
  )
}

export default App
