const dateFormatter = new Intl.DateTimeFormat('en-CA', {
  weekday: 'short',
  month: 'short',
  day: 'numeric',
})

const formatDate = (value) => {
  const date = new Date(value)
  return Number.isNaN(date.getTime()) ? 'Upcoming forecast' : dateFormatter.format(date)
}

export default function WeatherCard({ day }) {
  return (
    <article className="weather-card">
      <div className="weather-card-top">
        <div className="weather-location">
          <p className="weather-date">{formatDate(day.date)}</p>
          <p className="weather-description">{day.description}</p>
        </div>
        <img
          className="weather-icon"
          src={`https://openweathermap.org/img/wn/${day.icon}@2x.png`}
          alt={day.description}
        />
      </div>

      <div className="temperature-row">
        <span className="temperature">{day.temp ?? '—'}°C</span>
        <span className="feels-like">Feels like {day.feelsLike ?? '—'}°C</span>
      </div>

      <div className="metrics-grid">
        <div className="metric">
          <span className="metric-label">Humidity</span>
          <span className="metric-value">{day.humidity ?? '—'}%</span>
        </div>
        <div className="metric">
          <span className="metric-label">Wind</span>
          <span className="metric-value">{day.windSpeed ?? '—'} km/h</span>
        </div>
      </div>
    </article>
  )
}
