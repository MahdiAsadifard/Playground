import WeatherCard from './WeatherCard'

function StatusPanel({ variant, title, message, showSpinner = false }) {
  return (
    <div className={`status-panel ${variant}`}>
      <div>
        {showSpinner ? <div className="spinner" aria-hidden="true" /> : null}
        <h3 className="status-title">{title}</h3>
        <p>{message}</p>
      </div>
    </div>
  )
}

export default function WeatherGrid({ data, loading, error }) {
  if (loading) {
    return (
      <StatusPanel
        variant="loading"
        title="Loading forecast"
        message="We’re gathering the latest conditions for your selected city."
        showSpinner
      />
    )
  }

  if (error) {
    return (
      <StatusPanel
        variant="error"
        title="Unable to load weather data"
        message={error}
      />
    )
  }

  if (!data?.length) {
    return (
      <StatusPanel
        variant="empty"
        title="No results"
        message="Try a different city name or reduce the number of days."
      />
    )
  }

  return (
    <section className="weather-grid">
      {data.map((day) => (
        <WeatherCard key={day.id} day={day} />
      ))}
    </section>
  )
}
