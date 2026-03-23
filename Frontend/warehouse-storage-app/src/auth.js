const AUTH_STORAGE_KEY = 'warehouseAuth';

export const getStoredSession = () => {
  const storedSession = localStorage.getItem(AUTH_STORAGE_KEY)
    ?? sessionStorage.getItem(AUTH_STORAGE_KEY);

  if (!storedSession) {
    return null;
  }

  try {
    return JSON.parse(storedSession);
  } catch {
    localStorage.removeItem(AUTH_STORAGE_KEY);
    sessionStorage.removeItem(AUTH_STORAGE_KEY);
    return null;
  }
};

export const persistSession = (session, remember) => {
  const primaryStorage = remember ? localStorage : sessionStorage;
  const alternateStorage = remember ? sessionStorage : localStorage;

  alternateStorage.removeItem(AUTH_STORAGE_KEY);
  primaryStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(session));
};

export const clearSession = () => {
  localStorage.removeItem(AUTH_STORAGE_KEY);
  sessionStorage.removeItem(AUTH_STORAGE_KEY);
};

export const authFetch = async (input, init = {}) => {
  const session = getStoredSession();
  const headers = new Headers(init.headers ?? {});

  if (session?.token) {
    headers.set('Authorization', `Bearer ${session.token}`);
  }

  return fetch(input, {
    ...init,
    headers,
  });
};
