import { useEffect } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import { clearSession } from '../auth.js';

function LogoutPage() {
  const navigate = useNavigate();

  useEffect(() => {
    clearSession();
    navigate('/login', { replace: true });
  }, [navigate]);

  return <Navigate replace to="/login" />;
}

export default LogoutPage;
