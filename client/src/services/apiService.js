import api from './api';

export const authService = {
  async register(data) {
    const response = await api.post('/auth/register', data);
    return response.data;
  },

  async login(email, password) {
    const response = await api.post('/auth/login', { email, password });
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify(response.data.user));
    }
    return response.data;
  },

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  getCurrentUser() {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  },

  isAuthenticated() {
    return !!localStorage.getItem('token');
  },
};

export const projectService = {
  async getAll() {
    const response = await api.get('/projects');
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/projects/${id}`);
    return response.data;
  },

  async create(data) {
    const response = await api.post('/projects', data);
    return response.data;
  },

  async update(id, data) {
    const response = await api.put(`/projects/${id}`, data);
    return response.data;
  },

  async delete(id) {
    const response = await api.delete(`/projects/${id}`);
    return response.data;
  },
};

export default api;
