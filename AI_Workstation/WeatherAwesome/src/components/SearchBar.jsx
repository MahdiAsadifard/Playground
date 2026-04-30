import { useEffect, useState } from 'react'

const clampDays = (value) => Math.max(1, Number(value) || 1)

export default function SearchBar({ initialCity, initialDays, initialState, initialCountry, onSearch, disabled }) {
  const [city, setCity] = useState(initialCity)
  const [state, setState] = useState(initialState || '')
  const [country, setCountry] = useState(initialCountry || '')
  const [days, setDays] = useState(initialDays)

  useEffect(() => {
    setCity(initialCity)
  }, [initialCity])

  useEffect(() => {
    setDays(initialDays)
  }, [initialDays])

  useEffect(() => {
    setState(initialState || '')
  }, [initialState])

  useEffect(() => {
    setCountry(initialCountry || '')
  }, [initialCountry])

  const handleSubmit = (event) => {
    event.preventDefault()
    onSearch({
      city: city.trim() || 'Toronto',
      days: clampDays(days),
      state: state.trim() || null,
      country: country.trim() || null,
    })
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
            placeholder="e.g. Toronto"
            autoComplete="off"
          />
        </div>

        <div className="field-group">
          <label className="field-label" htmlFor="state-input">
            State/Province
          </label>
          <input
            id="state-input"
            className="input-field"
            type="text"
            value={state}
            onChange={(event) => setState(event.target.value)}
            placeholder="e.g. ON (optional)"
            autoComplete="off"
          />
        </div>

        <div className="field-group">
          <label className="field-label" htmlFor="country-input">
            Country
          </label>
          <input
            id="country-input"
            className="input-field"
            type="text"
            value={country}
            onChange={(event) => setCountry(event.target.value)}
            placeholder="e.g. CA (optional)"
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
          />
        </div>

        <button className="search-button" type="submit" disabled={disabled}>
          {disabled ? 'Refreshing…' : 'Search forecast'}
        </button>
      </form>
    </section>
  )
}
