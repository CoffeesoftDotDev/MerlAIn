import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AppShell from './shell/AppShell';
import SignInPage from './shell/pages/SignInPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Sign-in lives outside the shell: no navigation before there is a session. */}
        <Route path="/sign-in" element={<SignInPage />} />
        <Route path="*" element={<AppShell />} />
      </Routes>
    </BrowserRouter>
  );
}
