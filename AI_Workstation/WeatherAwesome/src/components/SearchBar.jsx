import { useEffect, useState } from 'react'

const clampDays = (value) => Math.min(5, Math.max(1, Number(value) || 1))

export default function SearchBar({ initialCity, initialDays, onSearch, disabled }) {
  const [city, setCity] = useState(initialCity)
  const [days, setDays] = useState(initialDays)

  useEffect(() => {
    setCity(initialCity)
  }, [initialCity])

  useEffect(() => {
    setDays(initialDays)
  }, [initialDays])

  const handleSubmit = (event) => {
    event.preventDefault()
    onSearch({ city: city.trim() || 'Toronto', days: clampDays(days) })
  }

  return (
    <section className="search-panel">
      <form className="search-form" onSubmit={handleSubmit}>
        <div className="field-group">
          <label className="field-label" htmlFor="city-input">
            City
          </label>
          <input
            id="city-input"
            className="input-field"
            type="text"
            value={city}
            onChange={(event) => setCity(event.target.value)}
            placeholder="Search city in Ontario..."
            autoComplete="off"
          />
        </div>

        <div className="field-group">
          <label className="field-label" htmlFor="days-input">
            Days
          </label>
          <input
            id="days-input"
            className="input-field"
            type="number"
            value={days}
            onChange={(event) => setDays(clampDays(event.target.value))}
            min="1"
            max="5"
          />
        </div>

        <button className="search-button" type="submit" disabled={disabled}>
          {disabled ? 'Refreshing…' : 'Search forecast'}
        </button>
      </form>
    </section>
  )
}
