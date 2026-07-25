import { NavLink } from 'react-router-dom';
import type { NavItem } from '@merlain/module-sdk';

interface NavProps {
  items: readonly NavItem[];
}

export default function Nav({ items }: NavProps) {
  return (
    <nav className="nav">
      <p className="nav__heading">Modules</p>

      <NavLink
        to="/"
        end
        className={({ isActive }) => (isActive ? 'nav__item nav__item--active' : 'nav__item')}
      >
        <span aria-hidden="true">🏠</span>
        <span>Dashboard</span>
      </NavLink>

      {items.map((item) => (
        <NavLink
          key={item.id}
          to={item.path}
          className={({ isActive }) => (isActive ? 'nav__item nav__item--active' : 'nav__item')}
        >
          <span aria-hidden="true">{item.icon}</span>
          <span>{item.title}</span>
        </NavLink>
      ))}
    </nav>
  );
}
