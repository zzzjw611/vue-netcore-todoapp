// src/services/todoService.js
import axios from 'axios';

// Create an Axios instance pre-configured to talk to our Todo API.
const api = axios.create({
  baseURL: 'http://localhost:5000/api/todo'
});

// set the “provider mode” header.
export function setProviderMode(mode) {
  api.defaults.headers['X-Provider-Mode'] = mode;
}
// CRUD operations for todos
export function getTodos(search = '') {
  return api.get('', { params: { search } })
    .then(res => res.data);
}

export function addTodo(todo) {
  return api.post('', todo)
    .then(res => res.data);
}

export function updateTodo(id, todo) {
  return api.put(`/${id}`, todo)
    .then(res => res.data);
}

export function deleteTodo(id) {
  return api.delete(`/${id}`);
}
