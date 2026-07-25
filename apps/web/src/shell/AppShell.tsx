import { Suspense } from 'react';
import { Route, Routes } from 'react-router-dom';
import Nav from './Nav';
import { registry } from './moduleRegistry';
import { buildModuleRoutes } from './routes';
import DashboardPage from './pages/DashboardPage';
import NotFoundPage from './pages/NotFoundPage';

export default function AppShell() {
  return (
    <div className="shell">
      <aside className="shell__sidebar">
        <div>
          <div className="shell__brand">MerlAIn</div>
          <div className="shell__tagline">Your Dungeon Master assistant</div>
        </div>

        <Nav items={registry.navItems} />

        <footer className="shell__footer">
          {registry.modules.length} module{registry.modules.length === 1 ? '' : 's'} registered
        </footer>
      </aside>

      <main className="shell__main">
        <Suspense fallback={<p className="placeholder">Summoning module…</p>}>
          <Routes>
            <Route path="/" element={<DashboardPage />} />
            {buildModuleRoutes(registry)}
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </Suspense>
      </main>
    </div>
  );
}
