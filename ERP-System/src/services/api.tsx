import axios from 'axios';

const api = axios.create({
  baseURL: 'https://your-api-endpoint.com/api', // Replace with your actual backend URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request Interceptor: Automatically attach Authorization token if available
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response Interceptor: Handle global errors like unauthorized access (401)
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;