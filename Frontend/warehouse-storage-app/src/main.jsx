import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { getStoredSession } from './auth.js';
import './css/index.css';
import './css/home.css';
import MenuLayout from './MenuLayout.jsx';
import ProductsPage from './pages/ProductsPage.jsx';
import TransitsPage from './pages/TransitsPage.jsx';
import AdminPage from './pages/AdminPage.jsx';
import LogoutPage from './pages/LogoutPage.jsx';
import WarehousePage from './pages/WarehousePage.jsx';
import App from './App.jsx';

function ProtectedRoute({ children }) {
  if (!getStoredSession()) {
    return <Navigate replace to="/login" />;
  }

  return children;
}

function GuestRoute({ children }) {
  if (getStoredSession()) {
    return <Navigate replace to="/products" />;
  }

  return children;
}

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate replace to={getStoredSession() ? '/products' : '/login'} />} />
        <Route
          path="/login"
          element={(
            <GuestRoute>
              <App />
            </GuestRoute>
          )}
        />
        <Route
          element={(
            <ProtectedRoute>
              <MenuLayout />
            </ProtectedRoute>
          )}
        >
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/warehouses" element={<WarehousePage />} />
          <Route path="/transits" element={<TransitsPage />} />
          <Route path="/employees" element={<AdminPage />} />
          <Route path="/logout" element={<LogoutPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
