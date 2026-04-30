import { useTheme } from '../context/ThemeContext'

export default function Header() {
  const { theme, toggleTheme } = useTheme()

  return (
    <header className="header">
      <div className="brand">
        <div className="brand-icon" aria-hidden="true">
          ⛅
        </div>
        <div className="brand-copy">
          <p>Professional Ontario forecast dashboard</p>
          <h1>Weather Awesome</h1>
        </div>
      </div>
      <button
        type="button"
        className="theme-toggle"
        onClick={toggleTheme}
        aria-label={`Switch to ${theme === 'light' ? 'dark' : 'light'} mode`}
      >
        <span className="theme-icon" aria-hidden="true">
          {theme === 'light' ? '🌙' : '☀️'}
        </span>
        <span>{theme === 'light' ? 'Dark mode' : 'Light mode'}</span>
      </button>
    </header>
  )
}
