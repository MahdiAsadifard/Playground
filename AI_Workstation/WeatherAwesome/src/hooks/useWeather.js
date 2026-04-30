import { useEffect, useState } from 'react'

const API_BASE_URL = 'http://localhost:3333/api/weather'

const clampDays = (days) => Math.min(5, Math.max(1, Number(days) || 1))

const formatNumber = (value) => {
  const numericValue = Number(value)
  return Number.isFinite(numericValue) ? Math.round(numericValue) : null
}

const toTitleCase = (value = '') =>
  value ? value.charAt(0).toUpperCase() + value.slice(1) : ''

function normalizeDay(day, index) {
  if (!day || typeof day !== 'object') {
    return null
  }

  const sourceWind = day.wind?.speed
  const flatWind =
    day.windKph ??
    day.wind_kph ??
    day.windSpeedKph ??
    day.windSpeed ??
    day.wind_speed

  const windSpeed =
    flatWind != null
      ? Number(flatWind)
      : Number.isFinite(Number(sourceWind))
        ? Number(sourceWind) * 3.6
        : null

  return {
    id: day.id ?? day.dt ?? day.date ?? `${index}-${day.description ?? 'forecast'}`,
    date:
      day.date ??
      day.datetime ??
      day.dt_txt ??
      day.timestamp ??
      (day.dt ? day.dt * 1000 : null),
    icon: day.icon ?? day.weather?.[0]?.icon ?? '01d',
    temp:
      formatNumber(day.temp ?? day.temperature ?? day.temperatureCelsius ?? day.main?.temp ?? day.dayTemp),
    feelsLike: formatNumber(
      day.feelsLike ?? day.feels_like ?? day.temperatureFeelsLike ?? day.main?.feels_like,
    ),
    description: toTitleCase(
      day.description ?? day.weather?.[0]?.description ?? 'Unavailable',
    ),
    humidity: formatNumber(day.humidity ?? day.main?.humidity),
    windSpeed: formatNumber(windSpeed),
  }
}

function normalizeWeatherResponse(response) {
  const payload = response?.data ?? response ?? {}

  // Backend returns { city: string, country: string, forecasts: [...] }
  const location =
    typeof payload.location === 'object' && payload.location !== null
      ? payload.location
      : {}
  const list =
    payload.forecasts ?? payload.days ?? payload.forecast ?? payload.list ?? payload.data ?? []

  const normalizedDays = Array.isArray(list)
    ? list.map(normalizeDay).filter(Boolean)
    : []

  const cityName =
    location.name ??
    (typeof payload.city === 'string' ? payload.city : null) ??
    payload.cityName ??
    payload.name ??
    ''

  const countryName =
    location.country ??
    (typeof payload.country === 'string' ? payload.country : null) ??
    location.countryCode ??
    ''

  return {
    location: {
      name: cityName,
      country: countryName,
    },
    days: normalizedDays,
  }
}

export default function useWeather(city, days) {
  const [data, setData] = useState({ location: { name: '', country: '' }, days: [] })
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    const controller = new AbortController()

    async function fetchWeather() {
      setLoading(true)
      setError('')

      try {
        const query = new URLSearchParams({
          city: city?.trim() || 'Toronto',
          days: String(clampDays(days)),
        })

        const response = await fetch(`${API_BASE_URL}?${query.toString()}`, {
          signal: controller.signal,
        })

        if (!response.ok) {
          throw new Error('Unable to fetch the weather forecast right now.')
        }

        const result = await response.json()
        setData(normalizeWeatherResponse(result))
      } catch (fetchError) {
        if (fetchError.name === 'AbortError') {
          return
        }

        setError(fetchError.message || 'Something went wrong while loading weather data.')
        setData({ location: { name: '', country: '' }, days: [] })
      } finally {
        if (!controller.signal.aborted) {
          setLoading(false)
        }
      }
    }

    fetchWeather()

    return () => controller.abort()
  }, [city, days])

  return { data, loading, error }
}
